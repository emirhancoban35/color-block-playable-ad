using System.Collections.Generic;
using UnityEngine;

namespace Data.Theme
{
    [CreateAssetMenu(fileName = "NewVisualTheme", menuName = "Playable/Data/Visual Theme")]
    public class VisualThemeConfig : ScriptableObject
    {
        public Color backgroundColor = new Color(0.82f, 0.85f, 1f);
        public Color boardColor = new Color(0.35f, 0.39f, 0.53f);
        public Color borderColor = new Color(0.16f, 0.18f, 0.27f);
        [Range(0.02f, 0.15f)] public float cellGap = 0.06f;
        [Range(0.01f, 0.2f)] public float cornerRadius = 0.12f;
        public bool showStuds = true;
        [Header("One shared material; color comes from mesh vertices")]
        public Material sharedMaterial;
        public List<PaletteEntry> palette = new List<PaletteEntry>();

        public Color GetColor(Data.Core.ColorId id)
        {
            for (int i = 0; i < palette.Count; i++) if (palette[i].colorId == id) return palette[i].color;
            switch (id)
            {
                case Data.Core.ColorId.Red: return new Color(1f, 0.18f, 0.1f);
                case Data.Core.ColorId.Blue: return new Color(0.08f, 0.56f, 1f);
                case Data.Core.ColorId.Yellow: return new Color(1f, 0.81f, 0.04f);
                case Data.Core.ColorId.Green: return new Color(0.33f, 0.83f, 0.03f);
                case Data.Core.ColorId.Purple: return new Color(0.61f, 0.25f, 1f);
                case Data.Core.ColorId.Orange: return new Color(1f, 0.44f, 0.05f);
                case Data.Core.ColorId.Cyan: return new Color(0.05f, 0.86f, 0.9f);
                default: return new Color(0.97f, 0.24f, 0.86f);
            }
        }
    }
}
