package gwen

import "core:encoding/json"
import "core:mem"

// Save slots: a small versioned JSON document per slot. Every field is range-checked on
// load; a failed load leaves the game untouched.

SAVE_VERSION :: 1
SAVE_GAME_TAG :: "gwens-peregrination"

Save_Data :: struct {
	game:                string,
	version:             i64,
	distance_remaining:  i64,
	health:              i64,
	satiety:             i64,
	pace:                i64,
	snax:                i64,
	foraging_skill:      i64,
	foraging_difficulty: i64,
	foraging_abundance:  i64,
	shortcut:            i64,
}

@(private = "file")
scratch: [1 << 14]u8

@(private = "file")
scratch_allocator :: proc() -> mem.Allocator {
	@(static) arena: mem.Arena
	mem.arena_init(&arena, scratch[:])
	return mem.arena_allocator(&arena)
}

// The text lives in a package buffer until the next save_encode or save_decode.
save_encode :: proc(g: ^Game) -> (text: string, ok: bool) {
	d := Save_Data {
		game                = SAVE_GAME_TAG,
		version             = SAVE_VERSION,
		distance_remaining  = i64(g.distance_remaining),
		health              = i64(g.health),
		satiety             = i64(g.satiety),
		pace                = i64(g.pace),
		snax                = i64(g.snax),
		foraging_skill      = i64(g.foraging_skill),
		foraging_difficulty = i64(g.foraging_difficulty),
		foraging_abundance  = i64(g.foraging_abundance),
		shortcut            = i64(g.shortcut),
	}
	bytes, err := json.marshal(d, allocator = scratch_allocator())
	if err != nil {
		return "", false
	}
	return string(bytes), true
}

@(private = "file")
in_range :: proc(v, lo, hi: i64) -> bool {
	return v >= lo && v <= hi
}

save_decode :: proc(g: ^Game, text: string) -> bool {
	d: Save_Data
	if json.unmarshal_string(text, &d, .JSON, scratch_allocator()) != nil {
		return false
	}
	if d.game != SAVE_GAME_TAG || d.version != SAVE_VERSION {
		return false
	}
	ok :=
		in_range(d.distance_remaining, 1, 1_000_000) &&
		in_range(d.health, 1, MAXIMUM_HEALTH) &&
		in_range(d.satiety, 0, MAXIMUM_SATIETY) &&
		in_range(d.pace, MINIMUM_PACE, MAXIMUM_PACE) &&
		in_range(d.snax, 0, 1_000_000) &&
		in_range(d.foraging_skill, 0, 1000) &&
		in_range(d.foraging_difficulty, -49, 49) &&
		in_range(d.foraging_abundance, 0, 4) &&
		in_range(d.shortcut, -10, 10)
	if !ok {
		return false
	}
	g^ = Game {
		distance_remaining  = int(d.distance_remaining),
		health              = int(d.health),
		satiety             = int(d.satiety),
		pace                = int(d.pace),
		snax                = int(d.snax),
		foraging_skill      = int(d.foraging_skill),
		foraging_difficulty = int(d.foraging_difficulty),
		foraging_abundance  = int(d.foraging_abundance),
		shortcut            = int(d.shortcut),
	}
	return true
}
