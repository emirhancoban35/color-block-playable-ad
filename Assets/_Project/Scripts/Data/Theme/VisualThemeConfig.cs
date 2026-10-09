using System.Collections.Generic;
using UnityEngine;

namespace Data.Theme
{
    [CreateAssetMenu(fileName = "NewVisualTheme", menuName = "Playable/Data/Visual Theme")]
    public class VisualThemeConfig : ScriptableObject
    {
        public Color backgroundColor = new Color(0.83f, 0.9f, 1f);
        public Color boardColor = new Color(0.39f, 0.46f, 0.62f);
        public Color borderColor = new Color(0.19f, 0.23f, 0.34f);
        [Range(0.02f, 0.15f)] public float cellGap = 0.06f;
        [Range(0.01f, 0.2f)] public float cornerRadius = 0.2f;
        public bool showStuds = true;
        [Header("One shared material; color comes from mesh vertices")]
        public Material sharedMaterial;
        public List<PaletteEntry> palette = new List<PaletteEntry>();

        public Color GetColor(Data.Core.ColorId id)
        {
            for (int i = 0; i < palette.Count; i++) if (palette[i].colorId == id) return palette[i].color;
            switch (id)
            {
                case Data.Core.ColorId.Red: return new Color(1f, 0.22f, 0.28f);
                case Data.Core.ColorId.Blue: return new Color(0.12f, 0.48f, 1f);
                case Data.Core.ColorId.Yellow: return new Color(1f, 0.87f, 0.12f);
                case Data.Core.ColorId.Green: return new Color(0.38f, 0.94f, 0.08f);
                case Data.Core.ColorId.Purple: return new Color(0.68f, 0.32f, 1f);
                case Data.Core.ColorId.Orange: return new Color(1f, 0.53f, 0.1f);
                case Data.Core.ColorId.Cyan: return new Color(0.06f, 0.92f, 0.86f);
                default: return new Color(1f, 0.32f, 0.78f);
            }
        }
    }
}
