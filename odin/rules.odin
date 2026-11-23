package gwen

// The rules of Gwen's Peregrination (GP.Initializer in the VB original), on one flat struct.
// The RNG is passed in, so tests can seed it. Fixes to the original are marked "Fix:" and
// listed in QUIRKS.md.

MAXIMUM_HEALTH :: 100
MAXIMUM_SATIETY :: 100
INITIAL_DISTANCE_REMAINING :: 2000
INITIAL_FORAGING_SKILL :: 50
INITIAL_PACE :: 3
INITIAL_SNAX :: 10
MINIMUM_PACE :: 1
MAXIMUM_PACE :: 5
SNAX_SATIETY_BENEFIT :: 10

Game :: struct {
	distance_remaining:  int,
	health:              int,
	satiety:             int,
	pace:                int,
	snax:                int,
	foraging_skill:      int,
	foraging_difficulty: int, // -49 to 49; higher is easier (see forage)
	foraging_abundance:  int, // forages left here, 0 to 4
	shortcut:            int, // miles a shortcut here adds (negative: saves); 0 is none
}

// GwenInitializer and WorldInitializer.
game_init :: proc(g: ^Game, r: ^Rng) {
	g^ = {}
	g.distance_remaining = INITIAL_DISTANCE_REMAINING
	g.health = MAXIMUM_HEALTH
	g.satiety = MAXIMUM_SATIETY
	g.pace = INITIAL_PACE
	g.snax = INITIAL_SNAX
	g.foraging_skill = INITIAL_FORAGING_SKILL
	refresh_local_area(g, r)
}

has_won :: proc(g: ^Game) -> bool {return g.distance_remaining == 0}
is_dead :: proc(g: ^Game) -> bool {return g.health == 0}
can_forage :: proc(g: ^Game) -> bool {return g.foraging_abundance > 0}
has_shortcut :: proc(g: ^Game) -> bool {return g.shortcut != 0}

set_pace :: proc(g: ^Game, pace: int) {
	g.pace = clamp(pace, MINIMUM_PACE, MAXIMUM_PACE)
}

// Hunger comes off satiety first; whatever is left over comes off health.
apply_hunger :: proc(g: ^Game, amount: int) {
	if amount > g.satiety {
		g.health = clamp(g.health - (amount - g.satiety), 0, MAXIMUM_HEALTH)
		g.satiety = 0
	} else {
		g.satiety -= amount
	}
}

// A new stretch of road: a new foraging spot and maybe a shortcut.
refresh_local_area :: proc(g: ^Game, r: ^Rng) {
	g.foraging_difficulty = roll_dice(r, 1, 50) + roll_dice(r, -1, 50)
	g.foraging_abundance = max(roll_dice(r, 2, 3) + roll_dice(r, -2, 1), 0)
	g.shortcut = rng_range(r, 1, 4) == 1 ? roll_dice(r, 1, 11) + roll_dice(r, -1, 11) : 0
}

// Miles a Keep Going covers. Fix: the original always walked 3 miles whatever the pace.
miles_per_step :: proc(g: ^Game) -> int {
	return g.pace
}

// Keep Going: walk, get hungry by the miles walked, and reach a new stretch of road.
keep_going :: proc(g: ^Game, r: ^Rng) {
	miles := miles_per_step(g)
	g.distance_remaining = max(g.distance_remaining - miles, 0)
	apply_hunger(g, miles)
	refresh_local_area(g, r)
}

// Take Shortcut: the journey changes by the shortcut's miles; costs 1 hunger.
take_shortcut :: proc(g: ^Game, r: ^Rng) -> bool {
	if !has_shortcut(g) {
		return false
	}
	g.distance_remaining = max(g.distance_remaining + g.shortcut, 0)
	apply_hunger(g, 1)
	refresh_local_area(g, r)
	return true
}

// Chance in percent that one forage here succeeds.
forage_chance :: proc(g: ^Game) -> int {
	return clamp(g.foraging_skill + g.foraging_difficulty, 0, 100)
}

// Forage: costs 1 hunger; on success one snax and one less forage here.
// Fix: the original added the difficulty to the roll, so the spots labelled "Very Easy"
// were the hardest; the roll now subtracts it and the labels tell the truth.
forage :: proc(g: ^Game, r: ^Rng) -> bool {
	if !can_forage(g) {
		return false
	}
	apply_hunger(g, 1)
	roll := rng_range(r, 1, 100) - g.foraging_difficulty
	if roll <= g.foraging_skill {
		g.foraging_abundance = max(g.foraging_abundance - 1, 0)
		g.snax += 1
		return true
	}
	return false
}

eat_snax :: proc(g: ^Game) -> bool {
	if g.snax <= 0 {
		return false
	}
	g.snax -= 1
	g.satiety = clamp(g.satiety + SNAX_SATIETY_BENEFIT, 0, MAXIMUM_SATIETY)
	return true
}
