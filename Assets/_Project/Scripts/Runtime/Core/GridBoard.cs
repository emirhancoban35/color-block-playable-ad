using System;
using Data.Core;
using Data.Level;
using UnityEngine;

namespace Playable.Core
{
    public enum MoveResult { Blocked, Moved, Cleared }

    // The model owns its state. Moving a block never mutates a ScriptableObject.
    public sealed class GridBoard
    {
        private sealed class BlockState
        {
            public Vector2Int origin;
            public Vector2Int[] shape;
            public ColorId color;
            public MovementMode movement;
            public bool cleared;
        }

        private struct Gate
        {
            public ColorId color;
            public ExitSide side;
            public int start, length;
        }

        private readonly int[] occupancy;
        private readonly bool[] active;
        private readonly BlockState[] blocks;
        private readonly Gate[] gates;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int BlockCount { get { return blocks.Length; } }
        public int ClearedCount { get; private set; }

        public GridBoard(int width, int height, CellData[] cells, BlockData[] sourceBlocks, ExitGateData[] sourceGates)
        {
            if (width < 1 || height < 1 || width > 64 || height > 64)
                throw new ArgumentException("Grid dimensions must be between 1 and 64.");
            Width = width;
            Height = height;
            occupancy = new int[width * height];
            active = new bool[occupancy.Length];
            for (int i = 0; i < occupancy.Length; i++) { occupancy[i] = -1; active[i] = true; }

            // An empty cell list means a full rectangular board; entries override cells.
            if (cells != null)
                for (int i = 0; i < cells.Length; i++)
                {
                    CellData cell = cells[i];
                    if (cell == null || !Inside(cell.position)) throw new ArgumentException("Invalid cell override.");
                    int index = Index(cell.position);
                    active[index] = cell.isActive;
                    occupancy[index] = !cell.isActive || cell.isBlocker ? -2 : -1;
                }

            if (sourceBlocks == null || sourceBlocks.Length == 0) throw new ArgumentException("Level needs at least one block.");
            blocks = new BlockState[sourceBlocks.Length];
            for (int i = 0; i < blocks.Length; i++)
            {
                BlockData source = sourceBlocks[i];
                if (source == null || source.localCells == null || source.localCells.Count == 0)
                    throw new ArgumentException("Block shape cannot be empty.");
                if ((int)source.colorId < 0 || (int)source.colorId > 7 || (int)source.movementMode < 0 || (int)source.movementMode > 7)
                    throw new ArgumentException("Invalid block color or movement mode.");
                BlockState block = new BlockState {
                    origin = source.origin, shape = source.localCells.ToArray(), color = source.colorId, movement = source.movementMode
                };
                blocks[i] = block;
                for (int j = 0; j < block.shape.Length; j++)
                {
                    Vector2Int cell = block.origin + block.shape[j];
                    if (!Inside(cell) || occupancy[Index(cell)] != -1)
                        throw new ArgumentException("Blocks overlap, leave the board, or occupy a disabled cell.");
                    occupancy[Index(cell)] = i;
                }
            }

            gates = new Gate[sourceGates == null ? 0 : sourceGates.Length];
            for (int i = 0; i < gates.Length; i++)
            {
                ExitGateData source = sourceGates[i];
                if (source == null || (int)source.side < 0 || (int)source.side > 3 || (int)source.colorId < 0 || (int)source.colorId > 7)
                    throw new ArgumentException("Invalid gate.");
                int sideLength = source.side == ExitSide.Top || source.side == ExitSide.Bottom ? width : height;
                if (source.startIndex < 0 || source.length < 1 || source.startIndex > sideLength - source.length)
                    throw new ArgumentException("Gate is outside its board edge.");
                gates[i] = new Gate { color = source.colorId, side = source.side, start = source.startIndex, length = source.length };
            }
        }

        public bool Inside(Vector2Int cell) { return cell.x >= 0 && cell.y >= 0 && cell.x < Width && cell.y < Height; }
        private int Index(Vector2Int cell) { return cell.y * Width + cell.x; }
        public int BlockAt(Vector2Int cell) { return Inside(cell) ? occupancy[Index(cell)] : -1; }
        public bool IsActive(Vector2Int cell) { return Inside(cell) && active[Index(cell)]; }
        public bool IsBlocked(Vector2Int cell) { return Inside(cell) && occupancy[Index(cell)] == -2; }
        public Vector2Int Origin(int block) { return blocks[block].origin; }
        public Vector2Int LocalCell(int block, int cell) { return blocks[block].shape[cell]; }
        public int CellCount(int block) { return blocks[block].shape.Length; }
        public ColorId Color(int block) { return blocks[block].color; }
        public bool IsCleared(int block) { return blocks[block].cleared; }

        public MoveResult TryStep(int blockIndex, Vector2Int direction)
        {
            if (blockIndex < 0 || blockIndex >= blocks.Length || Math.Abs(direction.x) + Math.Abs(direction.y) != 1)
                return MoveResult.Blocked;
            BlockState block = blocks[blockIndex];
            if (block.cleared || !Allows(block.movement, direction)) return MoveResult.Blocked;
            Vector2Int target = block.origin + direction;
            bool leavesBoard = false;
            for (int i = 0; i < block.shape.Length; i++)
            {
                Vector2Int cell = target + block.shape[i];
                if (!Inside(cell)) { leavesBoard = true; continue; }
                int occupant = occupancy[Index(cell)];
                if (occupant != -1 && occupant != blockIndex) return MoveResult.Blocked;
            }
            if (leavesBoard && !FitsGate(block, target, direction)) return MoveResult.Blocked;
            WriteOccupancy(block, -1);
            if (leavesBoard)
            {
                block.cleared = true;
                ClearedCount++;
                return MoveResult.Cleared;
            }
            block.origin = target;
            WriteOccupancy(block, blockIndex);
            return MoveResult.Moved;
        }

        private bool FitsGate(BlockState block, Vector2Int target, Vector2Int direction)
        {
            ExitSide side = direction.x < 0 ? ExitSide.Left : direction.x > 0 ? ExitSide.Right : direction.y < 0 ? ExitSide.Bottom : ExitSide.Top;
            bool horizontalEdge = direction.y != 0;
            for (int g = 0; g < gates.Length; g++)
            {
                Gate gate = gates[g];
                if (gate.side != side || gate.color != block.color) continue;
                bool fits = true;
                for (int c = 0; c < block.shape.Length; c++)
                {
                    Vector2Int cell = target + block.shape[c];
                    int transverse = horizontalEdge ? cell.x : cell.y;
                    if (transverse < gate.start || transverse >= gate.start + gate.length) { fits = false; break; }
                }
                if (fits) return true;
            }
            return false;
        }

        private void WriteOccupancy(BlockState block, int value)
        {
            for (int i = 0; i < block.shape.Length; i++) occupancy[Index(block.origin + block.shape[i])] = value;
        }

        private static bool Allows(MovementMode mode, Vector2Int d)
        {
            switch (mode)
            {
                case MovementMode.Free: return true;
                case MovementMode.HorizontalOnly: return d.y == 0;
                case MovementMode.VerticalOnly: return d.x == 0;
                case MovementMode.UpOnly: return d.y > 0;
                case MovementMode.DownOnly: return d.y < 0;
                case MovementMode.LeftOnly: return d.x < 0;
                case MovementMode.RightOnly: return d.x > 0;
                default: return false;
            }
        }
    }
}
