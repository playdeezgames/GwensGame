# itch.io page copy: Gwen's Peregrination

Page: https://thegrumpygamedev.itch.io/gwens-peregrination
Jam: DOS Games June 2023 Jam (the live page says "Submission to DOS Games June 2023 Jam").
Pasted in by hand; nothing here is published by the repo. Cover: `assets/cover.png` (630 by 500, made by `tools/make_cover.py`). Screenshots: `assets/screenshots/` in order (made by `tools/make_shots.sh`).

**Draft, November 23, 2026, for the Odin/wasm port. Not shipped yet.** The Pages site below is not live until the author enables GitHub Pages for this repo.

## Short description (the one-line field)

A turn-based metaphor: walk 2,000 miles, eat snax, forage, and don't starve. Made for DOS Games June 2023 Jam.

(111 characters; itch.io allows 120.)

## Page text

**Latest version:** https://playdeezgames.github.io/GwensGame/

**Gwen's Peregrination** is a text-mode journey, in the colours of an old DOS screen. You are Gwen. Yer destination is 2,000 miles away. You walk there one number at a time.

You start with ten snax. Walking makes you hungry, and when yer satiety runs out, yer health goes instead. Along the way:

- pick yer pace, from Very Slow to Very Fast (faster gets there sooner, but you pass fewer places to forage)
- forage for more snax, where the ground is easy enough
- take a shortcut, which might save miles, or might not
- eat snax before it is too late

### Controls

- Type the number next to a choice and press Enter. Or tap the choice.
- Page Up and Page Down (or the mouse wheel) scroll back.
- Save to one of five slots from the Game Menu (0). Saves stay in yer browser.

### About this version

Gwen's Peregrination was first written for **DOS Games June 2023 Jam** in VB.NET, as a Windows, Linux and Mac download. This is a rebuild for the browser in Odin compiled to WebAssembly. The old downloads are retired; the browser version is the same metaphor, with pace now doing what it says and the foraging labels telling the truth.

It is the first Gwen metaphor. The sequel is [Gwen's Peregrination II](https://thegrumpygamedev.itch.io/gwens-peregrination-ii).

**AI disclosure:** this rebuild was written with Claude Code (Claude Opus 5.5): the code, the tests, the cover and screenshot scripts. The original metaphor, its design and its words are by TheGrumpyGameDev.

### Credits

- Written by TheGrumpyGameDev.
- Font: DINOBYTE by mby (https://mby.itch.io/dinobyte), CC BY 4.0, used unmodified.

## Page settings (by hand)

- Kind of project: HTML. Tick "played in the browser".
- Uploads: only the `html` channel. Remove the old windows, linux and mac downloads on ship day (the ports rule of November 5, 2026; done in the dashboard, butler cannot delete them).
- Embed: "Click to launch in fullscreen" (`embed[embed_type]` = `maximized`), not a fixed size.
- External link: "Latest version (GitHub Pages)", https://playdeezgames.github.io/GwensGame/ (once Pages is live).
- **For the author to decide:** the page is "Name your own price" and its text says "I intend to put this game on Steam, so that's why it asks for a dollar donation". Steam is closed and the games are free (no paid games), so the suggestion is: set the price to $0 or donate, no minimum, and delete that note. Not changed by anyone yet.
- Tags: text-based, turn-based, short, survival, dos, retro, metaphor.
- Genre: Simulation (as now).
- AI disclosure field: Code (the cover is drawn from the font by a script).
- Community: leave comments on.
