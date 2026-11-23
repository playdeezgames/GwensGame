#!/usr/bin/env python3
"""Renders build/shots/*.txt (grids dumped by odin/shots_test.odin) to assets/screenshots/*.png
with the DINOBYTE sheet and the original's colours, each glyph drawn at 2x."""
import glob, os
from PIL import Image

SHEET = Image.open("assets/dinobyte/dbyte_1x.png").convert("RGBA")
PALETTE = [
    (0, 0, 0), (51, 51, 221), (0, 170, 0), (0, 170, 170), (170, 0, 0), (170, 0, 170), (170, 85, 0), (170, 170, 170),
    (85, 85, 85), (85, 85, 255), (85, 255, 85), (85, 255, 255), (255, 85, 85), (255, 85, 170), (255, 255, 85), (255, 255, 255),
]
SCALE = 2
os.makedirs("assets/screenshots", exist_ok=True)
for path in sorted(glob.glob("build/shots/*.txt")):
    lines = open(path).read().splitlines()
    cols, rows = map(int, lines[0].split())
    img = Image.new("RGB", (cols * 6, rows * 8), (0, 0, 0))
    for r in range(rows):
        for c, cell in enumerate(lines[1 + r].split(" ")):
            ch, ink = map(int, cell.split(","))
            if ch in (0, 32):
                continue
            src = SHEET.crop(((ch % 16) * 6, (ch // 16) * 8, (ch % 16) * 6 + 6, (ch // 16) * 8 + 8))
            solid = Image.new("RGBA", (6, 8), PALETTE[ink] + (255,))
            img.paste(solid, (c * 6, r * 8), src)
    img = img.resize((img.width * SCALE, img.height * SCALE), Image.NEAREST)
    out = os.path.join("assets/screenshots", os.path.basename(path)[:-4] + ".png")
    img.save(out)
    print("wrote", out, img.size)
