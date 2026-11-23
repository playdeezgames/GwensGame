#+build !js
package gwen

import "core:os"
import "core:strings"
import "core:testing"

// Screenshots for the itch.io page: run with -define:SHOTS=true (tools/make_shots.sh). Each
// grid is written as text, one row per line of "char,ink" pairs, for tools/make_shots.py.
SHOTS :: #config(SHOTS, false)

@(private = "file")
dump :: proc(a: ^App, name: string) {
	g: Grid
	app_draw(a, &g)
	b := strings.builder_make(context.temp_allocator)
	strings.write_int(&b, g.cols)
	strings.write_byte(&b, ' ')
	strings.write_int(&b, g.rows)
	strings.write_byte(&b, '\n')
	for r in 0 ..< g.rows {
		for c in 0 ..< g.cols {
			if c > 0 {strings.write_byte(&b, ' ')}
			strings.write_int(&b, int(g.cells[r][c].ch))
			strings.write_byte(&b, ',')
			strings.write_int(&b, int(g.cells[r][c].ink))
		}
		strings.write_byte(&b, '\n')
	}
	os.make_directory("build/shots")
	_ = os.write_entire_file(strings.concatenate({"build/shots/", name, ".txt"}, context.temp_allocator), transmute([]u8)strings.to_string(b))
}

@(private = "file")
press :: proc(a: ^App, text: string) {
	for c in text {app_key(a, i32(c))}
	app_key(a, i32(Key.Enter))
}

@(private = "file")
noop_platform :: proc() -> Platform {
	return Platform {
		slot_read = proc(slot: int, buf: []u8) -> int {return -1},
		slot_write = proc(slot: int, text: string) {},
		volume_write = proc(v: int) {},
		toggle_fullscreen = proc() {},
		play_sfx = proc(s: Sfx, v: int) {},
	}
}

@(test)
make_shots :: proc(t: ^testing.T) {
	when !SHOTS {
		return
	}
	a: App
	app_init(&a, noop_platform(), 20231106, 5)
	dump(&a, "01-title")
	press(&a, "1")
	for _ in 0 ..< 4 {press(&a, "1")}
	press(&a, "2")
	press(&a, "4")
	press(&a, "1")
	dump(&a, "02-play")
	for _ in 0 ..< 3 {press(&a, "1")}
	for _ in 0 ..< 2 {
		if can_forage(&a.game) {press(&a, a.game.snax > 0 ? "4" : "3")}
	}
	dump(&a, "03-forage")
	app_set_layout(&a, true)
	dump(&a, "04-phone")
}
