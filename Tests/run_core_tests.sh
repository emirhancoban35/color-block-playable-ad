#!/bin/sh
set -eu
PROJECT_ROOT=$(CDPATH= cd -- "$(dirname -- "$0")/.." && pwd)
UNITY_MANAGED_PATH=${UNITY_MANAGED_PATH:-/Applications/Unity/Hub/Editor/6000.0.72f1/Unity.app/Contents/Managed/UnityEngine}
MONO_BIN=${MONO_BIN:-/Library/Frameworks/Mono.framework/Versions/Current/bin}
UNITY_FACADE_PATH=${UNITY_FACADE_PATH:-$UNITY_MANAGED_PATH/../../UnityReferenceAssemblies/unity-4.8-api/Facades/netstandard.dll}
TEST_DIR=$(mktemp -d)
trap 'rm -rf "$TEST_DIR"' EXIT
find "$PROJECT_ROOT/Assets/_Project/Scripts/Data/Core" -name '*.cs' > "$TEST_DIR/sources.rsp"
find "$PROJECT_ROOT/Assets/_Project/Scripts/Data/Level" -name '*.cs' ! -name 'LevelConfig.cs' >> "$TEST_DIR/sources.rsp"
printf '%s\n' "$PROJECT_ROOT/Assets/_Project/Scripts/Runtime/Core/GridBoard.cs" "$PROJECT_ROOT/Tests/GridBoardTests.cs" >> "$TEST_DIR/sources.rsp"
"$MONO_BIN/mcs" -langversion:7 -out:"$TEST_DIR/tests.exe" -r:"$UNITY_MANAGED_PATH/UnityEngine.CoreModule.dll" -r:"$UNITY_FACADE_PATH" @"$TEST_DIR/sources.rsp"
MONO_PATH="$UNITY_MANAGED_PATH" "$MONO_BIN/mono" "$TEST_DIR/tests.exe"
