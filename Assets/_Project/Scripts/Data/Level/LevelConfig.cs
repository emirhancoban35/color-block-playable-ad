using System.Collections.Generic;
using UnityEngine;

namespace Data.Level
{
    [CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Playable/Data/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Grid Bounds")]
        public int width = 8;
        public int height = 8;

        [Header("Board Data")]
        public List<CellData> cells = new List<CellData>();
        public List<BlockData> blocks = new List<BlockData>();
        public List<ExitGateData> exits = new List<ExitGateData>();
    }
}