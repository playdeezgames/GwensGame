package gwen

// Special key codes passed from the page; anything >= 32 is a printable character code.
Key :: enum i32 {
	None      = 0,
	Enter     = 5,
	Escape    = 6,
	Backspace = 7,
	Page_Up   = 8,
	Page_Down = 9,
}

SAVE_SLOTS :: 5

Sfx :: enum i32 {
	Player_Hit  = 0,
	Player_Dead = 1,
}

// What the app needs from the page. The web build fills it from JS imports; tests use fakes.
Platform :: struct {
	slot_read:         proc(slot: int, buf: []u8) -> int, // byte length, or -1 for an empty slot
	slot_write:        proc(slot: int, text: string),
	volume_write:      proc(volume: int),
	toggle_fullscreen: proc(),
	play_sfx:          proc(sfx: Sfx, volume: int),
}

App :: struct {
	platform:     Platform,
	rng:          Rng,
	term:         Term,
	state:        State,
	options_exit: State, // where Options' Go Back returns to
	game:         Game,
	in_game:      bool,
	volume:       int, // 0 to 10 (tenths)
	cols, rows:   int,
}

app_init :: proc(a: ^App, platform: Platform, seed: u64, volume: int) {
	a^ = {}
	a.platform = platform
	rng_seed(&a.rng, seed)
	a.volume = clamp(volume, 0, 10)
	a.cols = LANDSCAPE_COLS
	a.rows = LANDSCAPE_ROWS
	term_reset(&a.term)
	splash_screen(a)
	go_to(a, .Main_Menu)
}

app_set_layout :: proc(a: ^App, portrait: bool) {
	a.cols = portrait ? PORTRAIT_COLS : LANDSCAPE_COLS
	a.rows = portrait ? PORTRAIT_ROWS : LANDSCAPE_ROWS
}

app_key :: proc(a: ^App, key: i32) {
	t := &a.term
	switch Key(key) {
	case .Page_Up:
		term_scroll(t, a.rows / 2)
		return
	case .Page_Down:
		term_scroll(t, -a.rows / 2)
		return
	case .Backspace:
		term_backspace(t)
	case .Enter:
		if t.prompt {
			execute(a)
		}
	case .None, .Escape:
	case:
		if key >= 32 && key < 127 {
			term_type(t, u8(key))
		}
	}
	t.scroll = 0
}

// A tap on a menu line chooses it, as if its number had been typed.
app_tap :: proc(a: ^App, grid: ^Grid, col, row: int) {
	if row < 0 || row >= MAX_ROWS || !a.term.prompt {
		return
	}
	choice := grid.row_choice[row]
	if choice < 0 {
		return
	}
	t := &a.term
	t.input_n = 0
	buf: [8]u8
	n := 0
	if choice >= 10 {
		buf[n] = u8('0' + choice / 10)
		n += 1
	}
	buf[n] = u8('0' + choice % 10)
	n += 1
	for i in 0 ..< n {
		term_type(t, buf[i])
	}
	execute(a)
}

app_draw :: proc(a: ^App, g: ^Grid) {
	term_draw(&a.term, g, a.cols, a.rows)
}

// Engine.Execute and BaseState.Handle: one whole number, or "Please enter a valid number!".
@(private = "file")
execute :: proc(a: ^App) {
	buf: [INPUT_CAP]u8
	n := term_submit(&a.term, buf[:])
	text := string(buf[:n])
	// Fix: surrounding spaces are ignored (the original split on spaces and refused "1 ").
	for len(text) > 0 && text[0] == ' ' {
		text = text[1:]
	}
	for len(text) > 0 && text[len(text) - 1] == ' ' {
		text = text[:len(text) - 1]
	}
	choice, ok := parse_choice(text)
	if !ok || !handle_choice(a, choice) {
		show_error(a, "Please enter a valid number!")
	}
}

parse_choice :: proc(text: string) -> (int, bool) {
	if len(text) == 0 || len(text) > 6 {
		return 0, false
	}
	start := 0
	negative := false
	if text[0] == '-' || text[0] == '+' {
		negative = text[0] == '-'
		start = 1
		if len(text) == 1 {
			return 0, false
		}
	}
	v := 0
	for i in start ..< len(text) {
		c := text[i]
		if c < '0' || c > '9' {
			return 0, false
		}
		v = v * 10 + int(c - '0')
	}
	return negative ? -v : v, true
}

show_error :: proc(a: ^App, message: string) {
	term_line(&a.term, "", Ink.Red)
	term_line(&a.term, message)
	run(a)
}
