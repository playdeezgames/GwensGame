package gwen

import "core:fmt"

// The application states (GP.Application/States), each a Run that prints and a HandleChoice
// for the number typed at its prompt. States that only print go straight on to another.

State :: enum {
	Main_Menu,
	Embark,
	Load,
	Options,
	Volume,
	Neutral,
	Game_Menu,
	Confirm_Abandon,
	Save,
	Keep_Going,
	Win,
	Dead,
	Change_Pace,
	Eat_Snax,
	Forage,
	Take_Shortcut,
}

go_to :: proc(a: ^App, s: State) {
	a.state = s
	run(a)
}

@(private = "file")
SAVE_BUF_CAP :: 1024

// The 66-column title art. Only the wide layout shows it; the narrow one starts at the title line.
@(private = "file")
TITLE_ART := [?]string {
	"                          ___                 _",
	"                         / __|_ __ _____ _ _ ( )___",
	"                        | (_ \\ V  V / -_) ' \\|/(_-<",
	"              ___        \\___|\\_/\\_/\\___|_||_| /__/_   _",
	"             | _ \\___ _ _ ___ __ _ _ _(_)_ _  __ _| |_(_)___ _ _",
	"             |  _/ -_) '_/ -_) _` | '_| | ' \\/ _` |  _| / _ \\ ' \\",
	"             |_| \\___|_| \\___\\__, |_| |_|_||_\\__,_|\\__|_\\___/_||_|",
	"                             |___/",
}

splash_screen :: proc(a: ^App) {
	t := &a.term
	for line in TITLE_ART {
		term_special(t, line, .Light_Red, .Wide)
	}
	term_special(t, "", .Light_Red, .Narrow)
	term_special(t, "Gwen's Peregrination", .White, .Any, true)
	term_special(t, "A Production of TheGrumpyGameDev", .Dark_Gray, .Any, true)
	term_special(t, "Font: DINOBYTE by mby (CC BY 4.0)", .Dark_Gray, .Any, true)
}

run :: proc(a: ^App) {
	t := &a.term
	g := &a.game
	switch a.state {
	case .Main_Menu:
		term_line(t, "", Ink.Brown)
		term_line(t, "Main Menu:")
		term_choice(t, 1, "1. Embark!", Ink.Gray)
		term_choice(t, 2, "2. Load...")
		term_choice(t, 3, "3. Options...")
		term_prompt(t)

	case .Embark:
		term_line(t, "", Ink.Light_Cyan)
		term_line(t, "Prolog:")
		term_line(
			t,
			"You are Gwen. You seek to get to yer destination, which is quite far away. You will get hungry along the way, and you start out with a few snax, but you'll need to find more if you hope to make it. You will face various challenges and choices along yer way. Good luck!",
			Ink.Gray,
		)
		game_init(g, &a.rng)
		a.in_game = true
		go_to(a, .Neutral)

	case .Load:
		term_line(t, "", Ink.Brown)
		term_line(t, "Load From:")
		buf: [SAVE_BUF_CAP]u8
		for slot in 1 ..= SAVE_SLOTS {
			if a.platform.slot_read(slot, buf[:]) >= 0 {
				term_choice(t, slot, fmt.tprintf("%d. Slot %d", slot, slot), Ink.Gray)
			}
		}
		term_choice(t, 0, "0. Never Mind", Ink.Gray)
		term_prompt(t) // Fix: the original printed no prompt here

	case .Options:
		term_line(t, "", Ink.Brown)
		term_line(t, "Options:")
		term_choice(t, 1, "1. Toggle Fullscreen", Ink.Gray)
		term_choice(t, 2, "2. Set Volume...")
		term_choice(t, 0, "0. Go Back")
		term_prompt(t)

	case .Volume:
		term_line(t, "", Ink.Brown)
		term_line(t, fmt.tprintf("Volume(currently %d%%):", a.volume * 10))
		for n in 0 ..= 10 {
			term_choice(t, n + 1, fmt.tprintf("%d. %d%%", n + 1, n * 10), Ink.Gray)
		}
		term_choice(t, 0, "0. Go Back")
		term_prompt(t)

	case .Neutral:
		if has_won(g) {
			go_to(a, .Win)
			return
		}
		if is_dead(g) {
			a.platform.play_sfx(.Player_Dead, a.volume)
			go_to(a, .Dead)
			return
		}
		term_line(t, "", Ink.Brown)
		term_line(t, "Current Status:")
		term_line(t, fmt.tprintf("You have %d miles left to go!", g.distance_remaining), Ink.Gray)
		term_write(t, "Yer health: ", Ink.Gray)
		term_line(t, fmt.tprintf("%d/%d", g.health, MAXIMUM_HEALTH), statistic_ink(g.health, MAXIMUM_HEALTH))
		term_write(t, "Yer satiety: ", Ink.Gray)
		term_line(t, fmt.tprintf("%d/%d", g.satiety, MAXIMUM_SATIETY), statistic_ink(g.satiety, MAXIMUM_SATIETY))
		term_write(t, "Yer pace: ", Ink.Gray)
		term_line(t, pace_name(g.pace), pace_ink(g.pace))
		if can_forage(g) {
			term_write(t, "Foraging: ", Ink.Gray)
			term_line(t, forage_difficulty_name(g.foraging_difficulty), forage_difficulty_ink(g.foraging_difficulty))
		}
		if g.snax > 0 {
			term_line(t, fmt.tprintf("You have %d snax.", g.snax), Ink.Gray)
		}
		if has_shortcut(g) {
			term_line(t, "There is a potential shortcut here!")
		}
		choices := neutral_choices(g)
		for c, i in choices.list[:choices.n] {
			term_choice(t, i + 1, fmt.tprintf("%d. %s", i + 1, neutral_choice_names[c]), Ink.Gray)
		}
		term_choice(t, 0, "0. Game Menu", Ink.Gray)
		term_prompt(t)

	case .Game_Menu:
		term_line(t, "", Ink.Brown)
		term_line(t, "Game Menu:")
		term_choice(t, 1, "1. Save", Ink.Gray)
		term_choice(t, 2, "2. Abandon Game")
		term_choice(t, 3, "3. Options...")
		term_choice(t, 0, "0. Resume Game")
		term_prompt(t) // Fix: the original printed no prompt here

	case .Confirm_Abandon:
		term_line(t, "", Ink.Light_Red)
		term_line(t, "Are you sure you want to abandon the game?")
		term_choice(t, 1, "1. Yes", Ink.Gray)
		term_choice(t, 0, "0. No")
		term_prompt(t)

	case .Save:
		term_line(t, "", Ink.Brown)
		term_line(t, "Save To Slot:")
		buf: [SAVE_BUF_CAP]u8
		for slot in 1 ..= SAVE_SLOTS {
			term_write(t, fmt.tprintf("%d. Slot %d", slot, slot), Ink.Gray)
			if a.platform.slot_read(slot, buf[:]) >= 0 {
				term_write(t, " (will overwrite)", Ink.Red)
			}
			term_line(t)
			l := &t.lines[t.count - 1]
			l.choice = slot
			l.menu = t.menu
		}
		term_choice(t, 0, "0. Go Back", Ink.Gray)
		term_prompt(t)

	case .Keep_Going:
		term_line(t, "", Ink.Gray)
		term_line(t, "You keep going!")
		keep_going(g, &a.rng)
		go_to(a, .Neutral)

	case .Win:
		term_line(t, "", Ink.Light_Green)
		term_line(t, "You made it! I knew you could do it! Congratulations!")
		a.in_game = false
		go_to(a, .Main_Menu)

	case .Dead:
		term_line(t, "", Ink.Light_Red)
		term_line(t, "Yer dead!")
		a.in_game = false
		go_to(a, .Main_Menu)

	case .Change_Pace:
		term_line(t, "")
		term_write(t, "Current Pace: ", Ink.Gray)
		term_line(t, pace_name(g.pace), pace_ink(g.pace))
		term_line(t, "Change Pace:", Ink.Brown)
		for p in MINIMUM_PACE ..= MAXIMUM_PACE {
			term_write(t, fmt.tprintf("%d. ", p), Ink.Gray)
			term_choice(t, p, pace_name(p), pace_ink(p))
		}
		term_choice(t, 0, "0. Go Back", Ink.Gray)
		term_prompt(t)

	case .Eat_Snax:
		term_line(t, "", Ink.Gray)
		term_line(t, "You eat snax.")
		eat_snax(g)
		go_to(a, .Neutral)

	case .Forage:
		term_line(t, "")
		if forage(g, &a.rng) {
			term_line(t, "You successfully forage!", Ink.Green)
		} else {
			term_line(t, "You don't find anything!", Ink.Red)
		}
		go_to(a, .Neutral)

	case .Take_Shortcut:
		term_line(t, "")
		term_line(t, "You take the potential shortcut!", Ink.Gray)
		if g.shortcut > 0 {
			term_line(t, fmt.tprintf("It made yer journey %d mile(s) longer!", g.shortcut), Ink.Red)
		} else {
			term_line(t, fmt.tprintf("It made yer journey %d mile(s) shorter!", -g.shortcut), Ink.Green)
		}
		take_shortcut(g, &a.rng)
		go_to(a, .Neutral)
	}
}

// Returns false for a number the screen does not offer ("Please enter a valid number!").
handle_choice :: proc(a: ^App, choice: int) -> bool {
	g := &a.game
	switch a.state {
	case .Main_Menu:
		switch choice {
		case 1:
			go_to(a, .Embark)
		case 2:
			go_to(a, .Load)
		case 3:
			a.options_exit = .Main_Menu
			go_to(a, .Options)
		case:
			return false
		}

	case .Load:
		if choice == 0 {
			go_to(a, .Main_Menu)
			return true
		}
		if choice < 1 || choice > SAVE_SLOTS {
			return false
		}
		buf: [SAVE_BUF_CAP]u8
		n := a.platform.slot_read(choice, buf[:])
		if n < 0 {
			return false
		}
		loaded: Game
		term_line(&a.term)
		if save_decode(&loaded, string(buf[:n])) {
			a.game = loaded
			a.in_game = true
			term_line(&a.term, fmt.tprintf("Loaded game from Slot %d!", choice), Ink.Light_Green)
			go_to(a, .Neutral)
		} else {
			term_line(&a.term, fmt.tprintf("Failed to load game from Slot %d!", choice), Ink.Red)
			run(a) // Fix: the original left no menu or prompt after a failed load
		}

	case .Options:
		switch choice {
		case 0:
			go_to(a, a.options_exit)
		case 1:
			a.platform.toggle_fullscreen()
			run(a)
		case 2:
			go_to(a, .Volume)
		case:
			return false
		}

	case .Volume:
		switch choice {
		case 0:
			go_to(a, .Options)
		case 1 ..= 11:
			a.volume = choice - 1
			a.platform.volume_write(a.volume)
			a.platform.play_sfx(.Player_Hit, a.volume)
			run(a)
		case:
			return false
		}

	case .Neutral:
		if choice == 0 {
			go_to(a, .Game_Menu)
			return true
		}
		choices := neutral_choices(g)
		if choice < 1 || choice > choices.n {
			return false
		}
		switch choices.list[choice - 1] {
		case .Keep_Going:
			go_to(a, .Keep_Going)
		case .Change_Pace:
			go_to(a, .Change_Pace)
		case .Eat_Snax:
			go_to(a, .Eat_Snax)
		case .Forage:
			go_to(a, .Forage)
		case .Take_Shortcut:
			go_to(a, .Take_Shortcut)
		}

	case .Game_Menu:
		switch choice {
		case 0:
			go_to(a, .Neutral)
		case 1:
			go_to(a, .Save)
		case 2:
			go_to(a, .Confirm_Abandon)
		case 3:
			a.options_exit = .Game_Menu
			go_to(a, .Options)
		case:
			return false
		}

	case .Confirm_Abandon:
		switch choice {
		case 0:
			go_to(a, .Game_Menu)
		case 1:
			a.in_game = false
			go_to(a, .Main_Menu)
		case:
			return false
		}

	case .Save:
		switch choice {
		case 0:
			go_to(a, .Game_Menu)
		case 1 ..= SAVE_SLOTS:
			text, ok := save_encode(g)
			term_line(&a.term)
			if ok {
				a.platform.slot_write(choice, text)
				term_line(&a.term, fmt.tprintf("Game Saved to Slot %d!", choice), Ink.Light_Green)
			}
			go_to(a, .Game_Menu)
		case:
			return false
		}

	case .Change_Pace:
		switch choice {
		case 0:
			go_to(a, .Neutral)
		case MINIMUM_PACE ..= MAXIMUM_PACE:
			set_pace(g, choice)
			go_to(a, .Neutral)
		case:
			return false
		}

	case .Embark, .Keep_Going, .Win, .Dead, .Eat_Snax, .Forage, .Take_Shortcut:
		return false
	}
	return true
}

Neutral_Choice :: enum {
	Keep_Going,
	Change_Pace,
	Eat_Snax,
	Forage,
	Take_Shortcut,
}

neutral_choice_names := [Neutral_Choice]string {
	.Keep_Going    = "Keep Going!",
	.Change_Pace   = "Change Pace",
	.Eat_Snax      = "Eat Snax",
	.Forage        = "Forage",
	.Take_Shortcut = "Take Shortcut",
}

Neutral_Choices :: struct {
	list: [len(Neutral_Choice)]Neutral_Choice,
	n:    int,
}

// Unavailable actions are left out and the rest numbered from 1, as in the original.
neutral_choices :: proc(g: ^Game) -> (c: Neutral_Choices) {
	add :: proc(c: ^Neutral_Choices, x: Neutral_Choice) {
		c.list[c.n] = x
		c.n += 1
	}
	add(&c, .Keep_Going)
	add(&c, .Change_Pace)
	if g.snax > 0 {add(&c, .Eat_Snax)}
	if can_forage(g) {add(&c, .Forage)}
	if has_shortcut(g) {add(&c, .Take_Shortcut)}
	return
}

// GetStatisticColor: quarters of the maximum, rounded up.
statistic_ink :: proc(value, maximum: int) -> Ink {
	q := (value * 4 + maximum - 1) / maximum
	switch {
	case q >= 4:
		return .Green
	case q == 3:
		return .Yellow
	case q == 2:
		return .Brown
	}
	return .Red
}

@(private = "file")
pace_names := [MAXIMUM_PACE + 1]string{"", "Very Slow", "Slow", "Medium", "Fast", "Very Fast"}
@(private = "file")
pace_inks := [MAXIMUM_PACE + 1]Ink{.Gray, .Blue, .Cyan, .Green, .Yellow, .Red}

pace_name :: proc(pace: int) -> string {return pace_names[clamp(pace, MINIMUM_PACE, MAXIMUM_PACE)]}
pace_ink :: proc(pace: int) -> Ink {return pace_inks[clamp(pace, MINIMUM_PACE, MAXIMUM_PACE)]}

// Fix: the original's lowest band read "Very Difficulty".
forage_difficulty_name :: proc(d: int) -> string {
	switch {
	case d <= -30:
		return "Very Difficult"
	case d <= -15:
		return "Difficult"
	case d <= 14:
		return "Moderate"
	case d <= 29:
		return "Easy"
	}
	return "Very Easy"
}

forage_difficulty_ink :: proc(d: int) -> Ink {
	switch {
	case d <= -30:
		return .Red
	case d <= -15:
		return .Brown
	case d <= 14:
		return .Yellow
	case d <= 29:
		return .Green
	}
	return .Light_Green
}
