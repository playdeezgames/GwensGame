#+build !js
package gwen

import "core:fmt"
import "core:strings"
import "core:testing"

// --- A fake platform -------------------------------------------------------------------------

@(private = "file")
fake_slots: [SAVE_SLOTS + 1]string
@(private = "file")
fake_slot_store: [SAVE_SLOTS + 1][1024]u8
@(private = "file")
fake_sfx: [Sfx]int
@(private = "file")
fake_fullscreen: int
@(private = "file")
fake_volume: int

@(private = "file")
fake_reset :: proc() {
	fake_slots = {}
	fake_sfx = {}
	fake_fullscreen = 0
	fake_volume = -1
}

@(private = "file")
fake_platform :: proc() -> Platform {
	return Platform {
		slot_read = proc(slot: int, buf: []u8) -> int {
			s := fake_slots[slot]
			if len(s) == 0 {
				return -1
			}
			return copy(buf, s)
		},
		slot_write = proc(slot: int, text: string) {
			n := copy(fake_slot_store[slot][:], text)
			fake_slots[slot] = string(fake_slot_store[slot][:n])
		},
		volume_write = proc(v: int) {fake_volume = v},
		toggle_fullscreen = proc() {fake_fullscreen += 1},
		play_sfx = proc(s: Sfx, v: int) {fake_sfx[s] += 1},
	}
}

@(private = "file")
new_app :: proc(a: ^App, seed: u64 = 1) {
	fake_reset()
	app_init(a, fake_platform(), seed, 5)
}

@(private = "file")
type :: proc(a: ^App, text: string) {
	for c in text {
		app_key(a, i32(c))
	}
	app_key(a, i32(Key.Enter))
}

// The text of the last n log lines, joined by newlines.
@(private = "file")
tail :: proc(a: ^App, n: int) -> string {
	b := strings.builder_make(context.temp_allocator)
	t := &a.term
	for i in max(t.count - n, 0) ..< t.count {
		l := &t.lines[i]
		strings.write_string(&b, string(l.text[:l.n]))
		strings.write_byte(&b, '\n')
	}
	return strings.to_string(b)
}

@(private = "file")
log_has :: proc(a: ^App, text: string) -> bool {
	t := &a.term
	for i in 0 ..< t.count {
		l := &t.lines[i]
		if strings.contains(string(l.text[:l.n]), text) {
			return true
		}
	}
	return false
}

@(private = "file")
grid_row :: proc(g: ^Grid, row: int) -> string {
	b := strings.builder_make(context.temp_allocator)
	for c in 0 ..< g.cols {
		ch := g.cells[row][c].ch
		strings.write_byte(&b, ch == 0 ? ' ' : ch)
	}
	return strings.trim_right(strings.to_string(b), " ")
}

@(private = "file")
grid_has :: proc(g: ^Grid, text: string) -> bool {
	for r in 0 ..< g.rows {
		if strings.contains(grid_row(g, r), text) {
			return true
		}
	}
	return false
}

// --- Dice ------------------------------------------------------------------------------------

@(test)
rng_is_deterministic :: proc(t: ^testing.T) {
	a, b: Rng
	rng_seed(&a, 42)
	rng_seed(&b, 42)
	for _ in 0 ..< 100 {
		testing.expect_value(t, rng_next(&a), rng_next(&b))
	}
}

@(test)
local_area_stays_in_range :: proc(t: ^testing.T) {
	r: Rng
	rng_seed(&r, 3)
	g: Game
	game_init(&g, &r)
	saw_shortcut, saw_none, saw_no_forage := false, false, false
	for _ in 0 ..< 5000 {
		refresh_local_area(&g, &r)
		testing.expect(t, g.foraging_difficulty >= -49 && g.foraging_difficulty <= 49)
		testing.expect(t, g.foraging_abundance >= 0 && g.foraging_abundance <= 4)
		testing.expect(t, g.shortcut >= -10 && g.shortcut <= 10)
		saw_shortcut ||= g.shortcut != 0
		saw_none ||= g.shortcut == 0
		saw_no_forage ||= g.foraging_abundance == 0
	}
	testing.expect(t, saw_shortcut && saw_none && saw_no_forage)
}

// --- Rules -----------------------------------------------------------------------------------

@(test)
a_new_game_matches_the_original :: proc(t: ^testing.T) {
	r: Rng
	rng_seed(&r, 9)
	g: Game
	game_init(&g, &r)
	testing.expect_value(t, g.distance_remaining, 2000)
	testing.expect_value(t, g.health, 100)
	testing.expect_value(t, g.satiety, 100)
	testing.expect_value(t, g.pace, 3)
	testing.expect_value(t, g.snax, 10)
	testing.expect_value(t, g.foraging_skill, 50)
}

@(test)
hunger_spills_into_health :: proc(t: ^testing.T) {
	g := Game{health = 100, satiety = 3}
	apply_hunger(&g, 2)
	testing.expect_value(t, g.satiety, 1)
	testing.expect_value(t, g.health, 100)
	apply_hunger(&g, 5)
	testing.expect_value(t, g.satiety, 0)
	testing.expect_value(t, g.health, 96)
	apply_hunger(&g, 500)
	testing.expect_value(t, g.health, 0)
	testing.expect(t, is_dead(&g))
}

@(test)
keep_going_walks_the_pace :: proc(t: ^testing.T) {
	r: Rng
	rng_seed(&r, 1)
	for pace in MINIMUM_PACE ..= MAXIMUM_PACE {
		g: Game
		game_init(&g, &r)
		set_pace(&g, pace)
		keep_going(&g, &r)
		testing.expect_value(t, g.distance_remaining, 2000 - pace)
		testing.expect_value(t, g.satiety, 100 - pace)
	}
	g := Game{distance_remaining = 2, health = 100, satiety = 100, pace = 5}
	keep_going(&g, &r)
	testing.expect_value(t, g.distance_remaining, 0)
	testing.expect(t, has_won(&g))
}

@(test)
pace_is_clamped :: proc(t: ^testing.T) {
	g: Game
	set_pace(&g, 0)
	testing.expect_value(t, g.pace, 1)
	set_pace(&g, 9)
	testing.expect_value(t, g.pace, 5)
}

@(test)
shortcuts_change_the_distance_and_cost_one_hunger :: proc(t: ^testing.T) {
	r: Rng
	rng_seed(&r, 1)
	g := Game{distance_remaining = 100, health = 100, satiety = 50, shortcut = -7}
	testing.expect(t, take_shortcut(&g, &r))
	testing.expect_value(t, g.distance_remaining, 93)
	testing.expect_value(t, g.satiety, 49)
	g.shortcut = 4
	take_shortcut(&g, &r)
	testing.expect_value(t, g.distance_remaining, 97)
	g.shortcut = 0
	testing.expect(t, !take_shortcut(&g, &r))
	testing.expect_value(t, g.satiety, 48)
}

@(test)
forage_uses_up_the_spot :: proc(t: ^testing.T) {
	r: Rng
	rng_seed(&r, 5)
	// The easiest spot (+49, skill 50): every roll of 1d100 succeeds.
	g := Game{health = 100, satiety = 100, foraging_skill = 50, foraging_difficulty = 49, foraging_abundance = 2}
	testing.expect(t, forage(&g, &r))
	testing.expect(t, forage(&g, &r))
	testing.expect_value(t, g.snax, 2)
	testing.expect_value(t, g.satiety, 98)
	testing.expect(t, !can_forage(&g))
	testing.expect(t, !forage(&g, &r))
	testing.expect_value(t, g.satiety, 98) // nothing to forage costs nothing
}

@(test)
forage_labels_tell_the_truth :: proc(t: ^testing.T) {
	r: Rng
	rng_seed(&r, 11)
	rate :: proc(r: ^Rng, difficulty: int) -> int {
		wins := 0
		for _ in 0 ..< 4000 {
			g := Game{health = 100, satiety = 100, foraging_skill = 50, foraging_difficulty = difficulty, foraging_abundance = 1}
			if forage(&g, r) {
				wins += 1
			}
		}
		return wins * 100 / 4000
	}
	easy := rate(&r, 40)
	hard := rate(&r, -40)
	testing.expectf(t, easy > 80 && hard < 20, "easy %d%%, hard %d%%", easy, hard)
	testing.expect_value(t, forage_difficulty_name(40), "Very Easy")
	testing.expect_value(t, forage_difficulty_name(-40), "Very Difficult")
	testing.expect_value(t, forage_difficulty_name(0), "Moderate")
	testing.expect_value(t, forage_chance(&Game{foraging_skill = 50, foraging_difficulty = 40}), 90)
}

@(test)
snax_fill_up_to_the_maximum :: proc(t: ^testing.T) {
	g := Game{satiety = 95, snax = 1}
	testing.expect(t, eat_snax(&g))
	testing.expect_value(t, g.satiety, 100)
	testing.expect_value(t, g.snax, 0)
	testing.expect(t, !eat_snax(&g))
}

@(test)
statistic_colours_match_the_original :: proc(t: ^testing.T) {
	testing.expect_value(t, statistic_ink(100, 100), Ink.Green)
	testing.expect_value(t, statistic_ink(76, 100), Ink.Green)
	testing.expect_value(t, statistic_ink(75, 100), Ink.Yellow)
	testing.expect_value(t, statistic_ink(50, 100), Ink.Brown)
	testing.expect_value(t, statistic_ink(25, 100), Ink.Red)
	testing.expect_value(t, statistic_ink(0, 100), Ink.Red)
}

// --- Saves -----------------------------------------------------------------------------------

@(test)
saves_round_trip :: proc(t: ^testing.T) {
	g := Game{1234, 77, 12, 4, 9, 50, -21, 3, -6}
	text, ok := save_encode(&g)
	testing.expect(t, ok)
	copy_buf: [512]u8
	n := copy(copy_buf[:], text)
	back: Game
	testing.expect(t, save_decode(&back, string(copy_buf[:n])))
	testing.expect_value(t, back, g)
}

@(test)
bad_saves_are_refused :: proc(t: ^testing.T) {
	g := Game{1234, 77, 12, 4, 9, 50, -21, 3, -6}
	bad := [?]string {
		"",
		"not json",
		"{}",
		`{"game":"gwens-peregrination","version":2,"distance_remaining":5,"health":5,"satiety":5,"pace":3,"snax":1,"foraging_skill":50,"foraging_difficulty":0,"foraging_abundance":0,"shortcut":0}`,
		`{"game":"other","version":1,"distance_remaining":5,"health":5,"satiety":5,"pace":3,"snax":1,"foraging_skill":50,"foraging_difficulty":0,"foraging_abundance":0,"shortcut":0}`,
		`{"game":"gwens-peregrination","version":1,"distance_remaining":5,"health":5,"satiety":5,"pace":7,"snax":1,"foraging_skill":50,"foraging_difficulty":0,"foraging_abundance":0,"shortcut":0}`,
		`{"game":"gwens-peregrination","version":1,"distance_remaining":5,"health":0,"satiety":5,"pace":3,"snax":1,"foraging_skill":50,"foraging_difficulty":0,"foraging_abundance":0,"shortcut":0}`,
		`{"game":"gwens-peregrination","version":1,"distance_remaining":99999999999,"health":5,"satiety":5,"pace":3,"snax":1,"foraging_skill":50,"foraging_difficulty":0,"foraging_abundance":0,"shortcut":0}`,
		`{game:'gwens-peregrination',version:1,distance_remaining:5,health:5,satiety:5,pace:3,snax:1,foraging_skill:50,foraging_difficulty:0,foraging_abundance:0,shortcut:0,}`,
	}
	for s in bad {
		before := g
		testing.expectf(t, !save_decode(&g, s), "accepted %q", s)
		testing.expect_value(t, g, before)
	}
}

// --- Screens ---------------------------------------------------------------------------------

@(test)
the_title_and_main_menu_show :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	g: Grid
	app_draw(&a, &g)
	testing.expect(t, grid_has(&g, "A Production of TheGrumpyGameDev"))
	testing.expect(t, grid_has(&g, "|___/"))
	testing.expect(t, grid_has(&g, "1. Embark!"))
	testing.expect(t, grid_has(&g, "> _"))
	app_set_layout(&a, true)
	app_draw(&a, &g)
	testing.expect(t, !grid_has(&g, "|___/"))
	testing.expect(t, grid_has(&g, "Gwen's Peregrination"))
	testing.expect(t, grid_has(&g, "3. Options..."))
}

@(test)
a_bad_number_reprints_the_menu :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	type(&a, "7")
	testing.expect(t, strings.contains(tail(&a, 8), "Please enter a valid number!"))
	testing.expect_value(t, a.state, State.Main_Menu)
	type(&a, "abc")
	type(&a, "")
	testing.expect_value(t, a.state, State.Main_Menu)
	type(&a, " 1 ") // spaces are ignored
	testing.expect_value(t, a.state, State.Neutral)
}

@(test)
embarking_starts_the_walk :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	type(&a, "1")
	testing.expect(t, a.in_game)
	testing.expect(t, log_has(&a, "Prolog:"))
	testing.expect(t, log_has(&a, "choices along yer way. Good luck!")) // the whole paragraph fits a line
	testing.expect(t, log_has(&a, "You have 2000 miles left to go!"))
	testing.expect(t, log_has(&a, "1. Keep Going!"))
	testing.expect(t, log_has(&a, "0. Game Menu"))
	type(&a, "1")
	testing.expect(t, log_has(&a, "You keep going!"))
	testing.expect(t, log_has(&a, "You have 1997 miles left to go!"))
}

@(test)
pace_changes_through_the_menu :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	type(&a, "1")
	type(&a, "2")
	testing.expect(t, log_has(&a, "Current Pace: Medium"))
	type(&a, "5")
	testing.expect_value(t, a.game.pace, 5)
	testing.expect(t, log_has(&a, "Yer pace: Very Fast"))
	type(&a, "1")
	testing.expect_value(t, a.game.distance_remaining, 1995)
}

@(test)
taps_choose_menu_lines :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	g: Grid
	app_draw(&a, &g)
	row := -1
	for r in 0 ..< g.rows {
		if grid_row(&g, r) == "2. Load..." {
			row = r
		}
	}
	testing.expect(t, row >= 0)
	app_tap(&a, &g, 0, row)
	testing.expect_value(t, a.state, State.Load)
	// The old main menu's lines are no longer tappable.
	app_draw(&a, &g)
	for r in 0 ..< g.rows {
		if grid_row(&g, r) == "1. Embark!" {
			testing.expect_value(t, g.row_choice[r], -1)
		}
	}
}

@(test)
saving_and_loading_through_the_menus :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	type(&a, "1") // embark
	type(&a, "1") // keep going
	saved := a.game
	type(&a, "0") // game menu
	type(&a, "1") // save
	type(&a, "3") // slot 3
	testing.expect(t, log_has(&a, "Game Saved to Slot 3!"))
	testing.expect_value(t, a.state, State.Game_Menu)
	type(&a, "2")
	type(&a, "1") // abandon
	testing.expect(t, !a.in_game)
	testing.expect_value(t, a.state, State.Main_Menu)
	type(&a, "2") // load
	testing.expect(t, strings.contains(tail(&a, 6), "3. Slot 3"))
	testing.expect(t, !strings.contains(tail(&a, 6), "1. Slot 1"))
	type(&a, "1") // an empty slot is not a valid number
	testing.expect(t, strings.contains(tail(&a, 8), "Please enter a valid number!"))
	type(&a, "3")
	testing.expect(t, log_has(&a, "Loaded game from Slot 3!"))
	testing.expect_value(t, a.game, saved)
	testing.expect_value(t, a.state, State.Neutral)
	// The save menu now warns about slot 3.
	type(&a, "0")
	type(&a, "1")
	testing.expect(t, log_has(&a, "3. Slot 3 (will overwrite)"))
}

@(test)
a_corrupt_slot_fails_to_load :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	a.platform.slot_write(2, "garbage")
	type(&a, "2")
	type(&a, "2")
	testing.expect(t, log_has(&a, "Failed to load game from Slot 2!"))
	testing.expect_value(t, a.state, State.Load)
	testing.expect(t, a.term.prompt)
}

@(test)
options_toggle_fullscreen_and_set_volume :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	type(&a, "3")
	type(&a, "1")
	testing.expect_value(t, fake_fullscreen, 1)
	type(&a, "2")
	testing.expect(t, log_has(&a, "Volume(currently 50%):"))
	type(&a, "11")
	testing.expect_value(t, a.volume, 10)
	testing.expect_value(t, fake_volume, 10)
	testing.expect_value(t, fake_sfx[.Player_Hit], 1)
	testing.expect(t, log_has(&a, "Volume(currently 100%):"))
	type(&a, "0")
	type(&a, "0")
	testing.expect_value(t, a.state, State.Main_Menu)
	// From the game menu, Go Back returns there.
	type(&a, "1")
	type(&a, "0")
	type(&a, "3")
	type(&a, "0")
	testing.expect_value(t, a.state, State.Game_Menu)
}

@(test)
starving_ends_at_the_main_menu :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	type(&a, "1")
	a.game.health = 1
	a.game.satiety = 0
	type(&a, "1")
	testing.expect(t, log_has(&a, "Yer dead!"))
	testing.expect_value(t, fake_sfx[.Player_Dead], 1)
	testing.expect(t, !a.in_game)
	testing.expect_value(t, a.state, State.Main_Menu)
}

@(test)
arriving_wins :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	type(&a, "1")
	a.game.distance_remaining = 1
	type(&a, "1")
	testing.expect(t, log_has(&a, "You made it! I knew you could do it! Congratulations!"))
	testing.expect_value(t, a.state, State.Main_Menu)
}

@(test)
every_screen_draws_in_both_layouts :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	g: Grid
	for s in State {
		for portrait in ([2]bool{false, true}) {
			new_app(&a)
			type(&a, "1")
			app_set_layout(&a, portrait)
			if s != .Dead && s != .Win {
				go_to(&a, s)
			}
			app_draw(&a, &g)
			testing.expect(t, g.cols == (portrait ? PORTRAIT_COLS : LANDSCAPE_COLS))
			// The prompt is always on screen, on the last row that has text.
			last := -1
			for r in 0 ..< g.rows {
				if len(grid_row(&g, r)) > 0 {
					last = r
				}
			}
			testing.expectf(t, last >= 0 && strings.has_prefix(grid_row(&g, last), "> "), "%v portrait=%v: %q", s, portrait, grid_row(&g, last))
		}
	}
}

@(test)
scrolling_back_and_forward :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	type(&a, "1")
	for _ in 0 ..< 12 {
		type(&a, "1")
	}
	g: Grid
	app_draw(&a, &g)
	testing.expect(t, !grid_has(&g, "Prolog:"))
	for _ in 0 ..< 100 {
		app_key(&a, i32(Key.Page_Up))
	}
	app_draw(&a, &g)
	testing.expect(t, grid_has(&g, "Prolog:"))
	app_key(&a, '1') // typing jumps back to the bottom
	app_draw(&a, &g)
	testing.expect(t, grid_has(&g, "> 1_"))
}

@(test)
the_log_never_overflows :: proc(t: ^testing.T) {
	a: App
	new_app(&a)
	type(&a, "1")
	for _ in 0 ..< 1200 {
		type(&a, "2")
		type(&a, "3")
	}
	testing.expect(t, a.term.count <= LOG_CAP)
	g: Grid
	app_draw(&a, &g)
	testing.expect(t, grid_has(&g, "> _"))
}

// A simple player: eat when satiety is low, forage good spots, take shortcuts that save
// miles, walk at the given pace. Not a test of balance, a check the walk can be finished.
@(test)
a_sensible_walk_can_be_won :: proc(t: ^testing.T) {
	wins := 0
	RUNS :: 200
	for seed in 1 ..= RUNS {
		r: Rng
		rng_seed(&r, u64(seed))
		g: Game
		game_init(&g, &r)
		set_pace(&g, 5)
		for !has_won(&g) && !is_dead(&g) {
			if g.satiety <= MAXIMUM_SATIETY - SNAX_SATIETY_BENEFIT && g.snax > 0 {
				eat_snax(&g)
			} else if g.shortcut < 0 {
				take_shortcut(&g, &r)
			} else if can_forage(&g) && forage_chance(&g) >= 40 {
				forage(&g, &r)
			} else {
				keep_going(&g, &r)
			}
		}
		if has_won(&g) {
			wins += 1
		}
	}
	fmt.printfln("sensible bot: %d of %d walks won", wins, RUNS)
	testing.expect(t, wins > 0)
}
