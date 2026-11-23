# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

"Gwen's Peregrination" by TheGrumpyGameDev, made for DOS Games June 2023 Jam: a turn-based walk of 2,000 miles in an 80-column scrolling text terminal (a "Metaphor" in the author's terms). The original is VB.NET on MonoGame (`GP.*` projects, `GwensPeregrination/` host, the `abettor-of-splorr` submodule's S0W80 terminal), shipped to itch.io as Windows, Linux and Mac downloads. It was ported to **Odin compiled to `js_wasm32`** on November 23, 2026 (`odin/`); the VB source and the committed `pub-*` builds are still here, untouched. itch.io: https://thegrumpygamedev.itch.io/gwens-peregrination. The sequel is `enby-jam-2026` (Gwen's Peregrination II), whose Odin code this port borrows from.

## Related knowledge vault

The author's Obsidian vault at `/home/yermom/git/bok-of-splorr/splorr/` is the source of truth: `Home.md` (standing rules), `Games/Gwen's Peregrination.md` (this game's decisions and history), `Tech/Release standard.md`, `Tech/GitHub Pages.md`, `Tech/LootLocker.md`, `Gotchas.md`.

## Commands

```bash
tools/test.sh            # native tests (one thread: shared globals)
tools/build.sh [--size]  # build/web (game.wasm, odin.js, index.html, lootlocker.js, font, two WAVs)
tools/pages.sh           # rebuild docs/ for GitHub Pages (master, /docs); never commits or pushes
tools/make_shots.sh      # assets/screenshots/ (odin/shots_test.odin with -define:SHOTS=true, then tools/make_shots.py)
python3 tools/make_cover.py   # assets/cover.png, 630 by 500
./shippit.sh             # wrapper for tools/ship.sh: tests, size build, zip; uploads ONLY with --push
```

## Layout (`odin/`)

- `rules.odin`: the rules on one flat `Game` struct; the RNG (`rng.odin`, splitmix64) is passed in.
- `term.odin`: the scrolling terminal as a log of logical lines with per-byte ink, re-wrapped at draw time for 80x30 (wide) or 40x60 (tall window); title art lines are wide-only. Lines can carry a menu choice so a tap types its number.
- `screens.odin`: the original's application states (Run prints, HandleChoice acts); `app.odin`: input, the `Platform` hooks (slots, volume, fullscreen, sfx) and "Please enter a valid number!".
- `save.odin`: JSON per slot (`gwen1:slot1` to `gwen1:slot5`), tagged and range-checked; `web.odin` (`#+build js`) and `odin/web/index.html` are the only platform code.
- `QUIRKS.md`: every fix, keep and drop in the port.

## Rules

- **Never `git push`, never run `./shippit.sh --push` or `butler push`** unless the author says so ("push" means git push; "ship" means itch.io). One itch.io release a day, booked through Coxswain.
- Do not "fix" the metaphor's design or voice ("yer", "snax", the terse win and death lines). Read `QUIRKS.md` before changing anything odd; log any new fix there.
- Keep the DINOBYTE credit (title screen, footer, itch page, `NOTICE-dinobyte.txt`).
- Feedback: the footer's "Send feedback" link stays hidden until `LL_KEY` in `odin/web/index.html` is set; this game has no LootLocker game yet, and creating one is the author's call.
- The page footer keeps the back links (itch.io, more games).
