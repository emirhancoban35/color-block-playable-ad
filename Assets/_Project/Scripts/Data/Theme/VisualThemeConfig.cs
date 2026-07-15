using System.Collections.Generic;
using UnityEngine;

namespace Data.Theme
{
    [CreateAssetMenu(fileName = "NewVisualTheme", menuName = "Playable/Data/Visual Theme")]
    public class VisualThemeConfig : ScriptableObject
    {
        public Color backgroundColor = Color.gray;
        public Material boardMaterial;
        public Material borderMaterial;
        
        [Header("Block & Gate Materials")]
        public List<ColorMaterialMapping> materialMappings = new List<ColorMaterialMapping>();
    }
}