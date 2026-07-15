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
        public bool showHandPointer = true;
        public bool showFailHookBeforeWin = false;
        public bool autoPlayFirstMove = false;
        
        [Header("Texts")]
        public string tutorialText = "Drag to escape!";
        public string ctaText = "PLAY NOW";
        public string endCardTitle = "AWESOME!";
        public string endCardSubtitle = "Can you clear them all?";
    }
}