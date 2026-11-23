package gwen

// The scrolling text terminal the original drew into (S0W80's FrameBuffer), kept as a log of
// logical lines so it can be re-wrapped for the portrait layout. Each byte has its own ink.

LOG_CAP :: 600
LINE_CAP :: 400
INPUT_CAP :: 12

// The 16 colours of the original (CGA order); the page owns the RGB values.
Ink :: enum u8 {
	Black,
	Blue,
	Green,
	Cyan,
	Red,
	Magenta,
	Brown,
	Gray,
	Dark_Gray,
	Light_Blue,
	Light_Green,
	Light_Cyan,
	Light_Red,
	Light_Magenta,
	Yellow,
	White,
}

// Which layouts show a line (the title art only fits the wide one).
Only :: enum u8 {
	Any,
	Wide,
	Narrow,
}

Line :: struct {
	text:   [LINE_CAP]u8,
	ink:    [LINE_CAP]Ink,
	n:      int,
	choice: int, // the menu number this line chooses, or -1
	menu:   int, // which printing of a menu it belongs to (only the current one is tappable)
	only:   Only,
	center: bool,
}

Term :: struct {
	lines:   [LOG_CAP]Line,
	count:   int,
	fg:      Ink,
	open:    bool, // the last line is still being written
	menu:    int,
	prompt:  bool, // a "> " prompt is waiting for input
	input:   [INPUT_CAP]u8,
	input_n: int,
	scroll:  int, // rows scrolled back from the bottom
}

term_reset :: proc(t: ^Term) {
	t^ = {}
	t.fg = .Gray
}

@(private = "file")
new_line :: proc(t: ^Term) -> ^Line {
	if t.count == LOG_CAP {
		// Drop the oldest half; nobody scrolls back that far.
		half := LOG_CAP / 2
		copy(t.lines[:], t.lines[half:])
		t.count -= half
	}
	l := &t.lines[t.count]
	l^ = {}
	l.choice = -1
	t.count += 1
	t.open = true
	return l
}

@(private = "file")
current_line :: proc(t: ^Term) -> ^Line {
	if t.open && t.count > 0 {
		return &t.lines[t.count - 1]
	}
	return new_line(t)
}

// Write: text at the cursor in ink (or the current colour), which becomes the current colour.
term_write :: proc(t: ^Term, text: string, ink: Maybe(Ink) = nil) {
	if c, ok := ink.?; ok {
		t.fg = c
	}
	l := current_line(t)
	buf: [LINE_CAP]u8
	n := cp437_encode(buf[:], text)
	for i in 0 ..< n {
		if l.n < LINE_CAP {
			l.text[l.n] = buf[i]
			l.ink[l.n] = t.fg
			l.n += 1
		}
	}
}

// WriteLine: write the text, then end the line (an empty call makes a blank line).
term_line :: proc(t: ^Term, text: string = "", ink: Maybe(Ink) = nil) {
	term_write(t, text, ink)
	t.open = false
}

// A menu line that a tap can choose.
term_choice :: proc(t: ^Term, choice: int, text: string, ink: Maybe(Ink) = nil) {
	term_write(t, text, ink)
	l := &t.lines[t.count - 1]
	l.choice = choice
	l.menu = t.menu
	t.open = false
}

// A line shown only in one layout, optionally centred.
term_special :: proc(t: ^Term, text: string, ink: Ink, only: Only, center := false) {
	term_line(t, text, ink)
	l := &t.lines[t.count - 1]
	l.only = only
	l.center = center
}

// ShowPrompt: a blank line, then "> " in cyan, waiting for a number.
term_prompt :: proc(t: ^Term) {
	term_line(t)
	term_write(t, "> ", Ink.Cyan)
	t.prompt = true
	t.input_n = 0
}

term_type :: proc(t: ^Term, ch: u8) {
	if t.prompt && t.input_n < INPUT_CAP && ch >= 32 && ch < 127 {
		t.input[t.input_n] = ch
		t.input_n += 1
	}
}

term_backspace :: proc(t: ^Term) {
	if t.prompt && t.input_n > 0 {
		t.input_n -= 1
	}
}

// Enter: the typed text joins the prompt line, which ends. Returns what was typed.
// Every earlier menu stops being tappable.
term_submit :: proc(t: ^Term, out: []u8) -> int {
	n := min(t.input_n, len(out))
	copy(out, t.input[:n])
	term_line(t, string(t.input[:t.input_n]))
	t.prompt = false
	t.input_n = 0
	t.menu += 1
	t.scroll = 0
	return n
}

// --- Drawing ---------------------------------------------------------------------------------

LANDSCAPE_COLS :: 80
LANDSCAPE_ROWS :: 30
PORTRAIT_COLS :: 40
PORTRAIT_ROWS :: 60
MAX_COLS :: 80
MAX_ROWS :: 60

// One grid cell: a code page 437 glyph and its ink (palette index).
Cell :: struct {
	ch:  u8,
	ink: u8,
}

Grid :: struct {
	cols, rows: int,
	cells:      [MAX_ROWS][MAX_COLS]Cell,
	// For taps: the menu choice drawn on each row, or -1.
	row_choice: [MAX_ROWS]int,
}

// Enough rows to scroll back through the whole log at the narrow width.
@(private = "file")
ROW_CAP :: 4096

@(private = "file")
Row :: struct {
	line:       int, // index into the log, or -1 for the prompt
	start, end: int,
	col:        int,
}

@(private = "file")
shown :: proc(l: ^Line, wide: bool) -> bool {
	switch l.only {
	case .Any:
		return true
	case .Wide:
		return wide
	case .Narrow:
		return !wide
	}
	return true
}

// Splits one line into rows of at most cols glyphs, in reading order. A centred line loses
// its padding and each row is centred.
@(private = "file")
wrap_rows :: proc(text: []u8, cols: int, center: bool, rows: ^[dynamic; LINE_CAP]Row, line: int) {
	s := 0
	n := len(text)
	if center {
		for s < n && text[s] == ' ' {
			s += 1
		}
		for n > s && text[n - 1] == ' ' {
			n -= 1
		}
	}
	if s >= n {
		append(rows, Row{line, s, s, 0})
		return
	}
	for s < n {
		end, next := wrap_line(text[:n], s, cols)
		col := center ? max((cols - (end - s)) / 2, 0) : 0
		append(rows, Row{line, s, end, col})
		s = next
	}
}

// Draws the bottom of the log (or a scrolled-back part of it), with the prompt and cursor.
term_draw :: proc(t: ^Term, g: ^Grid, cols, rows: int) {
	g.cols = cols
	g.rows = rows
	g.cells = {}
	for i in 0 ..< MAX_ROWS {
		g.row_choice[i] = -1
	}
	wide := cols >= LANDSCAPE_COLS

	// The prompt line is drawn with the typed text and a cursor appended.
	prompt_line: Line
	if t.prompt && t.count > 0 {
		prompt_line = t.lines[t.count - 1]
		for i in 0 ..< t.input_n {
			if prompt_line.n < LINE_CAP {
				prompt_line.text[prompt_line.n] = t.input[i]
				prompt_line.ink[prompt_line.n] = .Cyan
				prompt_line.n += 1
			}
		}
		if prompt_line.n < LINE_CAP {
			prompt_line.text[prompt_line.n] = '_'
			prompt_line.ink[prompt_line.n] = .Gray
			prompt_line.n += 1
		}
	}

	// Gather rows from the newest line back until there are enough to fill the screen.
	need := rows + t.scroll
	collected: [dynamic; LINE_CAP]Row
	@(static) stack: [dynamic; ROW_CAP]Row // static: too big for the wasm stack
	clear(&stack)
	total := 0
	i := t.count - 1
	for i >= 0 && total < need {
		l := &t.lines[i]
		if t.prompt && i == t.count - 1 {
			l = &prompt_line
		}
		if shown(l, wide) {
			clear(&collected)
			wrap_rows(l.text[:l.n], cols, l.center, &collected, i)
			for k := len(collected) - 1; k >= 0; k -= 1 {
				if len(stack) < cap(stack) {
					append(&stack, collected[k])
				}
			}
			total = len(stack)
		}
		i -= 1
	}
	// stack holds rows newest first. Keep the scroll in range.
	t.scroll = clamp(t.scroll, 0, max(len(stack) - rows, 0))
	first := t.scroll // newest row shown
	visible := min(len(stack) - first, rows)
	for r in 0 ..< visible {
		row := stack[first + visible - 1 - r]
		l := &t.lines[row.line]
		if t.prompt && row.line == t.count - 1 {
			l = &prompt_line
		}
		for k in row.start ..< row.end {
			c := row.col + (k - row.start)
			if c < cols {
				g.cells[r][c] = Cell{l.text[k], u8(l.ink[k])}
			}
		}
		if l.choice >= 0 && l.menu == t.menu && t.prompt {
			g.row_choice[r] = l.choice
		}
	}
}

// Page Up and Page Down (and the mouse wheel) scroll back through the log.
term_scroll :: proc(t: ^Term, delta: int) {
	t.scroll = max(t.scroll + delta, 0)
}
