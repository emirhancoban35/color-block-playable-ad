using UnityEngine;

namespace Utilities
{
    public static class GridMath
    {
        public static Vector3 GridToWorld(Vector2Int cell, float cellSize, float depth = 0f)
        {
            return new Vector3(cell.x * cellSize, cell.y * cellSize, depth);
        }

        public static Vector3 GridToWorld(Vector2Int cell, float cellSize, Vector3 boardOrigin, float depth = 0f)
        {
            return boardOrigin + new Vector3(cell.x * cellSize, cell.y * cellSize, depth);
        }

        public static Vector2Int WorldToGrid(Vector3 worldPosition, float cellSize)
        {
            int x = Mathf.RoundToInt(worldPosition.x / cellSize);
            int y = Mathf.RoundToInt(worldPosition.y / cellSize);
            return new Vector2Int(x, y);
        }

        public static Vector2Int WorldToGrid(Vector3 worldPosition, float cellSize, Vector3 boardOrigin)
        {
            int x = Mathf.RoundToInt((worldPosition.x - boardOrigin.x) / cellSize);
            int y = Mathf.RoundToInt((worldPosition.y - boardOrigin.y) / cellSize);
            return new Vector2Int(x, y);
        }

        public static bool IsInsideBounds(Vector2Int cell, int width, int height)
        {
            return cell.x >= 0 && cell.x < width && cell.y >= 0 && cell.y < height;
        }
    }
}
