#+build js
package gwen

import "base:runtime"

foreign import host "env"

@(default_calling_convention = "contextless")
foreign host {
	// Hands the grid to the page, which blits glyphs from the font sheet.
	present :: proc(cells: [^]Cell, cols, rows, stride: i32) ---
	// Milliseconds since the epoch, used only to seed the RNG.
	now_ms :: proc() -> f64 ---
	// localStorage, under the page's "gwen1:" keys. storage_get returns the byte length, or -1.
	storage_get :: proc(key: [^]u8, key_len: i32, buf: [^]u8, cap: i32) -> i32 ---
	storage_set :: proc(key: [^]u8, key_len: i32, val: [^]u8, val_len: i32) ---
	fullscreen_toggle :: proc() ---
	sfx_play :: proc(sfx: i32, volume: f32) ---
}

@(private = "file")
app: App
@(private = "file")
grid: Grid
@(private = "file")
dirty: bool

@(private = "file")
key_buf: [16]u8

@(private = "file")
slot_key :: proc(slot: int) -> string {
	k := "slot0"
	copy(key_buf[:], k)
	key_buf[4] = u8('0' + slot)
	return string(key_buf[:5])
}

@(private = "file")
web_slot_read :: proc(slot: int, buf: []u8) -> int {
	k := slot_key(slot)
	return int(storage_get(raw_data(k), i32(len(k)), raw_data(buf), i32(len(buf))))
}

@(private = "file")
web_slot_write :: proc(slot: int, text: string) {
	k := slot_key(slot)
	storage_set(raw_data(k), i32(len(k)), raw_data(text), i32(len(text)))
}

@(private = "file")
web_volume_write :: proc(volume: int) {
	k := "volume"
	v := [1]u8{u8('a' + volume)} // one letter, a (0%) to k (100%)
	storage_set(raw_data(k), i32(len(k)), &v[0], 1)
}

@(private = "file")
web_fullscreen :: proc() {
	fullscreen_toggle()
}

@(private = "file")
web_sfx :: proc(sfx: Sfx, volume: int) {
	sfx_play(i32(sfx), f32(volume) / 10)
}

main :: proc() {
	volume := 5
	k := "volume"
	v: [4]u8
	if storage_get(raw_data(k), i32(len(k)), &v[0], len(v)) == 1 && v[0] >= 'a' && v[0] <= 'k' {
		volume = int(v[0] - 'a')
	}
	platform := Platform {
		slot_read         = web_slot_read,
		slot_write        = web_slot_write,
		volume_write      = web_volume_write,
		toggle_fullscreen = web_fullscreen,
		play_sfx          = web_sfx,
	}
	app_init(&app, platform, u64(now_ms()), volume)
	dirty = true
}

// odin.js calls this every animation frame.
@(export)
step :: proc(dt: f64) -> bool {
	context = runtime.default_context()
	if dirty {
		// Cleared before present: the page may call set_layout from inside present, which sets
		// dirty again and must not be lost (see the vault's Gotchas).
		dirty = false
		app_draw(&app, &grid)
		present(&grid.cells[0][0], i32(app.cols), i32(app.rows), MAX_COLS)
	}
	free_all(context.temp_allocator)
	return true
}

@(export)
on_key :: proc "c" (key: i32) {
	context = runtime.default_context()
	app_key(&app, key)
	dirty = true
	free_all(context.temp_allocator)
}

@(export)
set_layout :: proc "c" (portrait: bool) {
	context = runtime.default_context()
	app_set_layout(&app, portrait)
	dirty = true
}

@(export)
on_tap :: proc "c" (col, row: i32) {
	context = runtime.default_context()
	app_tap(&app, &grid, int(col), int(row))
	dirty = true
	free_all(context.temp_allocator)
}
