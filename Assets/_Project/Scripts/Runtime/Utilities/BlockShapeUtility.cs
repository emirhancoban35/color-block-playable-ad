using System.Collections.Generic;
using UnityEngine;
using Data.Level;

namespace Utilities
{
    public static class BlockShapeUtility
    {
        public static List<Vector2Int> GetWorldCells(BlockData block)
        {
            var worldCells = new List<Vector2Int>();
            if (block == null || block.localCells == null) return worldCells;

            foreach (var localCell in block.localCells)
            {
                worldCells.Add(block.origin + localCell);
            }
            return worldCells;
        }

        // For callers that reuse their buffer; GetWorldCells is for authoring only.
        public static void CopyWorldCells(BlockData block, List<Vector2Int> destination)
        {
            destination.Clear();
            if (block == null || block.localCells == null) return;
            for (int i = 0; i < block.localCells.Count; i++) destination.Add(block.origin + block.localCells[i]);
        }

        public static void Normalize(BlockData block)
        {
            if (block != null) block.NormalizeLocalCells();
        }

        public static bool HasDuplicateCells(BlockData block)
        {
            if (block == null || block.localCells == null) return false;

            var set = new HashSet<Vector2Int>();
            foreach (var cell in block.localCells)
            {
                if (!set.Add(cell)) return true;
            }
            return false;
        }

        public static bool IsConnected(BlockData block)
        {
            if (block == null || block.localCells == null || block.localCells.Count == 0) return false;
            if (block.localCells.Count == 1) return true;

            var cellSet = new HashSet<Vector2Int>(block.localCells);
            var visited = new HashSet<Vector2Int>();
            var queue = new Queue<Vector2Int>();

            var startCell = block.localCells[0];
            queue.Enqueue(startCell);
            visited.Add(startCell);

            Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var dir in directions)
                {
                    var neighbor = current + dir;
                    if (cellSet.Contains(neighbor) && !visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return visited.Count == block.localCells.Count;
        }

        public static RectInt GetBounds(BlockData block, bool useWorldSpace = false)
        {
            if (block == null || block.localCells == null || block.localCells.Count == 0)
                return new RectInt(Vector2Int.zero, Vector2Int.zero);

            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;

            foreach (var cell in block.localCells)
            {
                if (cell.x < minX) minX = cell.x;
                if (cell.y < minY) minY = cell.y;
                if (cell.x > maxX) maxX = cell.x;
                if (cell.y > maxY) maxY = cell.y;
            }

            int width = (maxX - minX) + 1;
            int height = (maxY - minY) + 1;

            Vector2Int pos = useWorldSpace ? block.origin + new Vector2Int(minX, minY) : new Vector2Int(minX, minY);
            return new RectInt(pos, new Vector2Int(width, height));
        }
    }
}
