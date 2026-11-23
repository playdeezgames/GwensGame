#!/usr/bin/env python3
"""Draws the 630 by 500 itch.io cover from the game's own font sheet (DINOBYTE, unmodified),
in the original's CGA colours."""
import sys
from PIL import Image

SHEET = "assets/dinobyte/dbyte_1x.png"
OUT = sys.argv[1] if len(sys.argv) > 1 else "assets/cover.png"
BG = (0, 0, 0)
INK = {
    "light_red": (255, 85, 85),
    "white": (255, 255, 255),
    "gray": (170, 170, 170),
    "dark_gray": (85, 85, 85),
    "brown": (170, 85, 0),
    "green": (0, 170, 0),
    "light_green": (85, 255, 85),
    "yellow": (255, 255, 85),
    "cyan": (0, 170, 170),
}

sheet = Image.open(SHEET).convert("RGBA")
W, H = 630, 500
img = Image.new("RGB", (W, H), BG)


def glyph(code, color, scale):
    cx, cy = (code % 16) * 6, (code // 16) * 8
    cell = sheet.crop((cx, cy, cx + 6, cy + 8))
    out = Image.new("RGBA", (6, 8), (0, 0, 0, 0))
    px, o = cell.load(), out.load()
    for y in range(8):
        for x in range(6):
            if px[x, y][3] > 0:
                o[x, y] = color + (255,)
    return out.resize((6 * scale, 8 * scale), Image.NEAREST)


def put(x, y, s, color, scale):
    for ch in s:
        code = ch if isinstance(ch, int) else ord(ch)
        g = glyph(code, INK[color], scale)
        img.paste(g, (x, y), g)
        x += 6 * scale


def centered(y, s, color, scale):
    put((W - len(s) * 6 * scale) // 2, y, s, color, scale)


centered(24, "GWEN'S", "light_red", 10)
centered(112, "PEREGRINATION", "light_red", 7)

# A road with Gwen walking it, snax on the way, and the far end marked.
S = 5
cols = W // (6 * S)
left = (W - cols * 6 * S) // 2
scene_y = 196
verge_top = [0x05, 0x20, 0x06, 0x20, 0x05, 0x06, 0x20, 0x20]
verge_bottom = [0x06, 0x20, 0x20, 0x05, 0x20, 0x06, 0x05, 0x20]
for i in range(cols):
    put(left + i * 6 * S, scene_y, [verge_top[i % len(verge_top)]], "green", S)
    put(left + i * 6 * S, scene_y + 8 * S, [0xB0], "dark_gray", S)
    put(left + i * 6 * S, scene_y + 16 * S, [verge_bottom[i % len(verge_bottom)]], "green", S)
road_y = scene_y + 8 * S
put(left + 3 * 6 * S, road_y, [0x02], "white", S)  # Gwen
put(left + 8 * 6 * S, road_y, [0x0F], "yellow", S)  # snax
put(left + 13 * 6 * S, road_y, [0x0F], "yellow", S)
put(left + (cols - 3) * 6 * S, road_y, [0x10], "light_green", S)  # the way on

# A slice of the status screen, as the game prints it.
put(40, 340, "You have 2000 miles left to go!", "gray", 3)
put(40, 370, "> ", "cyan", 3)
put(40 + 2 * 18, 370, "1", "cyan", 3)
centered(424, "Keep going. Eat snax. Forage.", "white", 2)
centered(452, "TheGrumpyGameDev", "brown", 2)
centered(474, "DOS Games June 2023 Jam", "dark_gray", 2)

img.save(OUT)
print("wrote", OUT, img.size)
