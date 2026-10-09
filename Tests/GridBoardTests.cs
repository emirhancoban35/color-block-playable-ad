using System;
using System.Collections.Generic;
using Data.Core;
using Data.Level;
using Playable.Core;
using UnityEngine;

internal static class GridBoardTests
{
    private static int passed;
    private static readonly CellData[] NoCells = new CellData[0];
    private static readonly ExitGateData[] NoGates = new ExitGateData[0];
    private static BlockData Block(int x, int y, MovementMode mode = MovementMode.Free, ColorId color = ColorId.Red, params Vector2Int[] shape)
    {
        return new BlockData { origin = new Vector2Int(x, y), movementMode = mode, colorId = color,
            localCells = new List<Vector2Int>(shape.Length == 0 ? new[] { Vector2Int.zero } : shape) };
    }
    private static GridBoard Board(BlockData[] blocks, ExitGateData[] gates = null, CellData[] cells = null, int width = 5, int height = 5)
    {
        return new GridBoard(width, height, cells ?? NoCells, blocks, gates ?? NoGates);
    }
    private static ExitGateData Gate(ExitSide side, int start, int length = 1, ColorId color = ColorId.Red)
    { return new ExitGateData { side = side, startIndex = start, length = length, colorId = color }; }
    private static void Equal<T>(T expected, T actual)
    { if (!EqualityComparer<T>.Default.Equals(expected, actual)) throw new Exception("Expected " + expected + ", got " + actual); }
    private static void Reject(Action action)
    { try { action(); } catch (ArgumentException) { return; } throw new Exception("Expected invalid data rejection"); }
    private static void Test(string name, Action test)
    { test(); passed++; Console.WriteLine("PASS " + name); }
    public static int Main()
    {
        Test("Move updates old and new occupancy", () => {
            var board = Board(new[] { Block(1, 1) });
            Equal(MoveResult.Moved, board.TryStep(0, Vector2Int.right));
            Equal(-1, board.BlockAt(new Vector2Int(1, 1))); Equal(0, board.BlockAt(new Vector2Int(2, 1)));
        });
        Test("Blocked moves are transactional", () => {
            var board = Board(new[] { Block(1, 1), Block(2, 1) });
            Equal(MoveResult.Blocked, board.TryStep(0, Vector2Int.right));
            Equal(new Vector2Int(1, 1), board.Origin(0)); Equal(0, board.BlockAt(new Vector2Int(1, 1)));
        });
        Test("Movement rejects diagonals, zero and jumps", () => {
            var board = Board(new[] { Block(1, 1) });
            foreach (var d in new[] { Vector2Int.zero, new Vector2Int(1, 1), new Vector2Int(2, 0) }) Equal(MoveResult.Blocked, board.TryStep(0, d));
        });
        Test("Multi-cell block can move into its previous cells", () => {
            var board = Board(new[] { Block(1, 1, MovementMode.Free, ColorId.Red, Vector2Int.zero, Vector2Int.right) });
            Equal(MoveResult.Moved, board.TryStep(0, Vector2Int.right));
            Equal(-1, board.BlockAt(new Vector2Int(1, 1))); Equal(0, board.BlockAt(new Vector2Int(2, 1))); Equal(0, board.BlockAt(new Vector2Int(3, 1)));
        });
        Test("All four matching gates clear once", () => {
            var sides = new[] { ExitSide.Left, ExitSide.Right, ExitSide.Bottom, ExitSide.Top };
            var positions = new[] { new Vector2Int(0, 2), new Vector2Int(4, 2), new Vector2Int(2, 0), new Vector2Int(2, 4) };
            var directions = new[] { Vector2Int.left, Vector2Int.right, Vector2Int.down, Vector2Int.up };
            for (int i = 0; i < 4; i++) {
                var board = Board(new[] { Block(positions[i].x, positions[i].y) }, new[] { Gate(sides[i], 2) });
                Equal(MoveResult.Cleared, board.TryStep(0, directions[i])); Equal(1, board.ClearedCount);
                Equal(-1, board.BlockAt(positions[i])); Equal(MoveResult.Blocked, board.TryStep(0, directions[i])); Equal(1, board.ClearedCount);
            }
        });
        Test("Wrong color and wrong span cannot clear", () => {
            var board = Board(new[] { Block(0, 1) }, new[] { Gate(ExitSide.Left, 1, 1, ColorId.Blue), Gate(ExitSide.Left, 3) });
            Equal(MoveResult.Blocked, board.TryStep(0, Vector2Int.left)); Equal(0, board.ClearedCount);
        });
        Test("Whole shape must fit one gate", () => {
            var block = Block(0, 1, MovementMode.Free, ColorId.Red, Vector2Int.zero, Vector2Int.up);
            var board = Board(new[] { block }, new[] { Gate(ExitSide.Left, 1), Gate(ExitSide.Left, 2) });
            Equal(MoveResult.Blocked, board.TryStep(0, Vector2Int.left));
            board = Board(new[] { block }, new[] { Gate(ExitSide.Left, 1, 2) }); Equal(MoveResult.Cleared, board.TryStep(0, Vector2Int.left));
            Equal(-1, board.BlockAt(new Vector2Int(0, 1))); Equal(-1, board.BlockAt(new Vector2Int(0, 2)));
        });
        Test("Inactive cells and obstacles stop movement", () => {
            var board = Board(new[] { Block(1, 1) }, null, new[] {
                new CellData { position = new Vector2Int(2, 1), isBlocker = true },
                new CellData { position = new Vector2Int(1, 2), isActive = false } });
            Equal(MoveResult.Blocked, board.TryStep(0, Vector2Int.right)); Equal(MoveResult.Blocked, board.TryStep(0, Vector2Int.up));
        });
        Test("Every movement mode enforces its directions", () => {
            var directions = new[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            bool[][] allowed = {
                new[] { true, true, true, true }, new[] { false, false, true, true }, new[] { true, true, false, false },
                new[] { true, false, false, false }, new[] { false, true, false, false }, new[] { false, false, true, false },
                new[] { false, false, false, true }, new[] { false, false, false, false } };
            for (int mode = 0; mode < 8; mode++) for (int direction = 0; direction < 4; direction++) {
                var board = Board(new[] { Block(2, 2, (MovementMode)mode) });
                Equal(allowed[mode][direction] ? MoveResult.Moved : MoveResult.Blocked, board.TryStep(0, directions[direction]));
            }
        });
        Test("Runtime owns a copy of authored data", () => {
            var block = Block(1, 1); var gate = Gate(ExitSide.Left, 1);
            var board = Board(new[] { block }, new[] { gate });
            board.TryStep(0, Vector2Int.left); Equal(new Vector2Int(1, 1), block.origin);
            block.localCells[0] = new Vector2Int(99, 99); block.colorId = ColorId.Blue; gate.colorId = ColorId.Blue;
            Equal(MoveResult.Cleared, board.TryStep(0, Vector2Int.left));
        });
        Test("Invalid authoring data rejected", () => {
            Reject(() => Board(new[] { Block(0, 0), Block(0, 0) }));
            Reject(() => Board(new[] { Block(-1, 0) }));
            Reject(() => Board(new[] { Block(0, 0, MovementMode.Free, ColorId.Red, Vector2Int.zero, Vector2Int.zero) }));
            Reject(() => Board(new[] { Block(0, 0) }, new[] { Gate(ExitSide.Top, 4, 2) }));
            Reject(() => Board(new[] { Block(0, 0) }, null, null, 0));
        });
        Test("Irregular shape exit removes every occupied cell", () => {
            var board = Board(new[] { Block(3, 1, MovementMode.Free, ColorId.Red, Vector2Int.zero, Vector2Int.up, new Vector2Int(1, 1)) }, new[] { Gate(ExitSide.Right, 1, 2) });
            Equal(MoveResult.Cleared, board.TryStep(0, Vector2Int.right));
            Equal(-1, board.BlockAt(new Vector2Int(3, 1))); Equal(-1, board.BlockAt(new Vector2Int(3, 2))); Equal(-1, board.BlockAt(new Vector2Int(4, 2)));
        });
        Test("Random moves preserve occupancy and never overlap", () => {
            var board = Board(new[] { Block(0, 0), Block(2, 2), Block(4, 4), Block(0, 3) });
            var random = new System.Random(714); var directions = new[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            for (int n = 0; n < 5000; n++) {
                board.TryStep(random.Next(4), directions[random.Next(4)]);
                var occupied = new HashSet<Vector2Int>();
                for (int b = 0; b < 4; b++) { Equal(true, occupied.Add(board.Origin(b))); Equal(b, board.BlockAt(board.Origin(b))); }
            }
        });
        Console.WriteLine(passed + " test groups passed.");
        return 0;
    }
}
