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
        [SerializeField] private Text hook;
        [SerializeField] private RectTransform instruction;
        private float visualTime, resultTime = -1f;
        private bool fullScreenClick;

#if UNITY_EDITOR
        public void Configure(RectTransform safe, Text progressLabel, Text tutorialLabel, GameObject card,
            Text resultLabel, Text subtitleLabel, RectTransform button, Text buttonLabel, Text hookLabel, RectTransform instructionPanel)
        {
            safeRoot = safe;
            progress = progressLabel;
            tutorial = tutorialLabel;
            endCard = card;
            endTitle = resultLabel;
            subtitle = subtitleLabel;
            cta = button;
            ctaText = buttonLabel;
            hook = hookLabel;
            instruction = instructionPanel;
        }
#endif
        public void Initialize(AdFlowConfig flow)
        {
            hook.text = flow.hookText;
            progress.gameObject.SetActive(flow.showProgress);
            visualTime = 0f;
            resultTime = -1f;
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
        public void Progress(int cleared, int total, int moves) { if (!progress.gameObject.activeSelf) return; progress.text = cleared + " / " + total + " CLEARED   ·   " + moves + " MOVES"; }
        public void Tutorial(bool visible) { if (instruction.gameObject.activeSelf != visible) instruction.gameObject.SetActive(visible); }
        public void Cta(bool visible) { if (cta.gameObject.activeSelf != visible) cta.gameObject.SetActive(visible); }
        public void TickVisuals(float dt, bool idle)
        {
            visualTime += dt;
            if (visualTime - dt < 0.35f) hook.transform.localScale = Vector3.one * Mathf.Lerp(0.94f, 1f, Mathf.Min(1f, visualTime / 0.35f));
            if (instruction.gameObject.activeSelf) instruction.localScale = Vector3.one * (idle ? 1f + Mathf.Sin(visualTime * 3f) * 0.015f : 1f);
            if (cta.gameObject.activeSelf) cta.localScale = Vector3.one * (1f + Mathf.Sin(visualTime * 3.5f) * 0.04f);
            if (resultTime >= 0f && resultTime < 0.3f)
            {
                resultTime += dt;
                float t = Mathf.Min(1f, resultTime / 0.25f);
                endCard.transform.localScale = Vector3.one * (1f - 0.05f * (1f - t) * (1f - t));
            }
        }
        public void EndCard(bool won, bool visible, bool fullScreen, string title)
        {
            endTitle.text = won ? title : "TRY AGAIN!";
            endCard.SetActive(visible);
            if (visible) resultTime = 0f;
            fullScreenClick = visible && fullScreen;
        }
        public bool HitCta(Vector2 screenPoint)
        {
            return fullScreenClick || (cta.gameObject.activeSelf && RectTransformUtility.RectangleContainsScreenPoint(cta, screenPoint));
        }
    }
}
