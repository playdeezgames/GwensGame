# Gwen's Peregrination

"              ___                 _"
"             / __|_ __ _____ _ _ ( )___"
"            | (_ \ V  V / -_) ' \|/(_-<"
"  ___        \___|\_/\_/\___|_||_| /__/_   _"
" | _ \___ _ _ ___ __ _ _ _(_)_ _  __ _| |_(_)___ _ _"
" |  _/ -_) '_/ -_) _` | '_| | ' \/ _` |  _| / _ \ ' \"
" |_| \___|_| \___\__, |_| |_|_||_\__,_|\__|_\___/_||_|"
"                 |___/"

A turn-based walk of 2,000 miles: you are Gwen, you get hungry, you eat snax, forage and take shortcuts. Made for DOS Games June 2023 Jam by TheGrumpyGameDev. itch.io: https://thegrumpygamedev.itch.io/gwens-peregrination

## The browser version (Odin/wasm, November 2026)

`odin/` is a rebuild in Odin compiled to `js_wasm32` (canvas text grid, no WebGL), ported from the VB.NET projects in this repo (`GP.*` with the `abettor-of-splorr` submodule), which are kept for reference. What changed and why: `QUIRKS.md`.

```bash
tools/test.sh            # native tests
tools/build.sh [--size]  # build/web; serve it with any static server to play
tools/pages.sh           # rebuild docs/ for GitHub Pages (never commits or pushes)
tools/make_shots.sh      # assets/screenshots/ from scripted play
python3 tools/make_cover.py   # assets/cover.png
./shippit.sh             # tests, size build, zip; uploads to itch.io ONLY with --push
```

Odin is expected at `/home/yermom/ODIN/odin` (override with `ODIN=`).

Font: DINOBYTE by mby, CC BY 4.0 (`assets/dinobyte/`). The rebuild was written by an AI agent (Claude Code) at the author's direction.
