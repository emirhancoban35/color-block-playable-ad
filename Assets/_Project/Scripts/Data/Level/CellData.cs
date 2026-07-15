using UnityEngine;

namespace Data.Level
{
    [System.Serializable]
    public class CellData
    {
        public Vector2Int position;
        public bool isActive = true;
        public bool isBlocker = false;
    }
}