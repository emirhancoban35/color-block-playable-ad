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
    }
}