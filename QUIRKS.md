# Quirks: what the Odin port kept, fixed and dropped

The VB.NET original (September 2023, `GP.*` projects with the `abettor-of-splorr` S0W80 terminal) is still in this repo. The port is in `odin/`. The author's decision (November 23, 2026): **fix quirks as we go** and log each one here; leave the voice and the design alone.

## Fixed

| Original | Port | Where |
| --- | --- | --- |
| Keep Going always walked 3 miles. Pace was shown and could be changed, but did nothing. | Keep Going walks your pace in miles (Very Slow 1 to Very Fast 5), at 1 hunger per mile as before. A faster pace means fewer stretches of road, so fewer chances to forage and fewer shortcuts. | `rules.odin` `miles_per_step` |
| Foraging rolled `1d100 + difficulty` against the skill, so the spots labelled "Very Easy" were the hardest (about 10 percent) and "Very Difficult" the easiest. | The roll is `1d100 - difficulty`, so the labels are true: the chance is skill plus difficulty (50 percent at Moderate, up to 99 at Very Easy). | `rules.odin` `forage` |
| The lowest forage band read "Very Difficulty". | "Very Difficult". | `screens.odin` |
| The Game Menu and Load screens printed no `>` prompt. | Both print one. | `screens.odin` |
| A failed load left the screen with no menu or prompt. | The Load menu is printed again. | `screens.odin` |
| "1 " (a trailing space) was "Please enter a valid number!" | Spaces around the number are ignored. | `app.odin` `execute` |

## Kept

- The scrolling 80-column terminal, the 16 colours, every message and menu text, and the menu numbers. Typing a number and pressing Enter is still how you play; a tap on a menu line types its number for you.
- Five save slots (`Slot 1` to `Slot 5`), saved only from the Game Menu, with "(will overwrite)". They live in the browser (`localStorage`, keys `gwen1:slot1` to `gwen1:slot5`).
- Winning and dying go straight back to the Main Menu with one line ("You made it! ..." / "Yer dead!"). Health never comes back; only satiety does.
- The PlayerHit sound when the volume is set and PlayerDead when you die (the original's two WAVs, `assets/sfx/`). Volume is remembered (`gwen1:volume`).
- The game is easy if you forage: a simple bot that eats below 90 satiety, forages spots at 40 percent or better and takes shortcuts that save miles wins every one of 200 seeded walks at Very Fast (`a_sensible_walk_can_be_won`). That was true of the original's rules too; it is the design, not a bug.

## Changed for the browser

- **Font:** DINOBYTE by mby (CC BY 4.0), the author's choice, in place of the original's `Font8x16.png` (an 8 by 16 DOS ROM-style font with no licence record). The grid is 6 by 8 glyphs.
- **Layouts:** a wide window gets 80 by 30; a tall one (a phone held upright) gets 40 by 60 with the text re-wrapped and the title art replaced by the plain title line. The prolog is one paragraph that wraps to the width, rather than four lines broken at column 80.
- **Dropped:** Quit (there is nothing to quit to in a browser; Main Menu `0. Quit` and its confirmation are gone) and Set Display Size (the page scales to the window). Options is now 1 Toggle Fullscreen, 2 Set Volume..., 0 Go Back.
- **New:** Page Up, Page Down and the mouse wheel scroll back through the log (the original could not scroll).
- **Credit:** the title adds "Font: DINOBYTE by mby (CC BY 4.0)".
