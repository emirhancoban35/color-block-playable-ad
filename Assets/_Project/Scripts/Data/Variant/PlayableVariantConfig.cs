using UnityEngine;
using Data.Level;
using Data.Theme;
using Data.Flow;

namespace Data.Variant
{
    [CreateAssetMenu(fileName = "NewVariant", menuName = "Playable/Data/Variant Config")]
    public class PlayableVariantConfig : ScriptableObject
    {
        public string variantId = "Variant_A";
        
        [Header("Configurations")]
        public LevelConfig levelConfig;
        public VisualThemeConfig visualTheme;
        public AdFlowConfig adFlowConfig;
        [Header("Animation")]
        [Range(8f, 40f)] public float moveSpeed = 22f;
        [Range(0.1f, 0.6f)] public float exitDuration = 0.24f;
    }
}
