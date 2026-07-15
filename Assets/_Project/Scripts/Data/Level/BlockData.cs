using System.Collections.Generic;
using UnityEngine;
using Data.Core;

namespace Data.Level
{
    [System.Serializable]
    public class BlockData
    {
        public string id;
        public ColorId colorId;
        public Vector2Int origin;
        public List<Vector2Int> localCells = new List<Vector2Int>();
        public MovementMode movementMode = MovementMode.Free;

        public void NormalizeLocalCells()
        {
            if (localCells == null || localCells.Count == 0) return;

            int minX = int.MaxValue;
            int minY = int.MaxValue;

            foreach (var cell in localCells)
            {
                if (cell.x < minX) minX = cell.x;
                if (cell.y < minY) minY = cell.y;
            }

            origin += new Vector2Int(minX, minY);

            for (int i = 0; i < localCells.Count; i++)
            {
                localCells[i] = new Vector2Int(localCells[i].x - minX, localCells[i].y - minY);
            }
        }
    }
}