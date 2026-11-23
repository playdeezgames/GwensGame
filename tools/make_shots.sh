#!/usr/bin/env bash
# Regenerates assets/screenshots/ from scripted play (odin/shots_test.odin, then tools/make_shots.py).
set -euo pipefail
cd "$(dirname "$0")/.."
mkdir -p build
rm -rf build/shots
"${ODIN:-/home/yermom/ODIN/odin}" test odin -define:ODIN_TEST_THREADS=1 -define:SHOTS=true \
	-define:ODIN_TEST_NAMES=gwen.make_shots -out:build/odin-shots
python3 tools/make_shots.py
