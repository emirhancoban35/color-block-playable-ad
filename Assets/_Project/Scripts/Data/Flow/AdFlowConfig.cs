using UnityEngine;
using Data.Core;

namespace Data.Flow
{
    [CreateAssetMenu(fileName = "NewAdFlow", menuName = "Playable/Data/Ad Flow Config")]
    public class AdFlowConfig : ScriptableObject
    {
        [Header("Core Flow")]
        public EndCondition endCondition = EndCondition.OnAllBlocksCleared;
        public CtaMode ctaMode = CtaMode.EndCardOnly;
        public bool showEndCardOnWin = true;
        
        [Header("Condition Limits")]
        public int moveLimit = 10;
        public int targetBlocksToClear = 5;

        [Header("Timings & Delays")]
        public float timer = 15f;
        public float endCardDelay = 1f;
        public float ctaDelay = 2f;
        public float tutorialDelay = 1f;

        [Header("Hooks & Interactions")]
        public bool showTutorial = true;
        // Add creative hooks when their runtime behavior is implemented.
        
        [Header("Texts")]
#if PLAYWORKS_SDK
        [LunaPlaygroundField("Tutorial Text", 1, "Texts")]
#endif
        public string tutorialText = "Drag to escape!";
#if PLAYWORKS_SDK
        [LunaPlaygroundField("CTA Text", 0, "Texts")]
#endif
        public string ctaText = "PLAY NOW";
#if PLAYWORKS_SDK
        [LunaPlaygroundField("End Card Title", 2, "Texts")]
#endif
        public string endCardTitle = "AWESOME!";
        public string endCardSubtitle = "Can you clear them all?";
    }
}
