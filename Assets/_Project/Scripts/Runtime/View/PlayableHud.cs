using Data.Flow;
using UnityEngine;
using UnityEngine.UI;

namespace Playable.View
{
    public sealed class PlayableHud : MonoBehaviour
    {
        [SerializeField] private RectTransform safeRoot;
        [SerializeField] private Text progress;
        [SerializeField] private Text tutorial;
        [SerializeField] private GameObject endCard;
        [SerializeField] private Text endTitle;
        [SerializeField] private Text subtitle;
        [SerializeField] private RectTransform cta;
        [SerializeField] private Text ctaText;
        private bool fullScreenClick;

#if UNITY_EDITOR
        public void Configure(RectTransform safe, Text progressLabel, Text tutorialLabel, GameObject card,
            Text resultLabel, Text subtitleLabel, RectTransform button, Text buttonLabel)
        {
            safeRoot = safe;
            progress = progressLabel;
            tutorial = tutorialLabel;
            endCard = card;
            endTitle = resultLabel;
            subtitle = subtitleLabel;
            cta = button;
            ctaText = buttonLabel;
        }
#endif
        public void Initialize(AdFlowConfig flow)
        {
            tutorial.text = flow.tutorialText;
            endTitle.text = flow.endCardTitle;
            subtitle.text = flow.endCardSubtitle;
            ctaText.text = flow.ctaText;
            endCard.SetActive(false);
            fullScreenClick = false;
            Tutorial(false);
            Cta(flow.ctaMode == Data.Core.CtaMode.PersistentButton);
        }

        public void Resize()
        {
            Rect safe = Screen.safeArea;
            safeRoot.anchorMin = new Vector2(safe.xMin / Screen.width, safe.yMin / Screen.height);
            safeRoot.anchorMax = new Vector2(safe.xMax / Screen.width, safe.yMax / Screen.height);
            safeRoot.offsetMin = safeRoot.offsetMax = Vector2.zero;
        }
        public void Progress(int cleared, int total, int moves) { progress.text = cleared + " / " + total + " CLEARED   ·   " + moves + " MOVES"; }
        public void Tutorial(bool visible) { tutorial.gameObject.SetActive(visible); }
        public void Cta(bool visible) { cta.gameObject.SetActive(visible); }
        public void EndCard(bool won, bool visible, bool fullScreen, string title)
        {
            endTitle.text = won ? title : "TRY AGAIN!";
            endCard.SetActive(visible);
            fullScreenClick = visible && fullScreen;
        }
        public bool HitCta(Vector2 screenPoint)
        {
            return fullScreenClick || (cta.gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(cta, screenPoint));
        }
    }
}
