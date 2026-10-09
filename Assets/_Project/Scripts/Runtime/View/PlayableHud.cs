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
        [SerializeField] private RectTransform headerBackground;
        [SerializeField] private RectTransform endBackdrop;
        private float visualTime, resultTime = -1f;
        private bool fullScreenClick;

#if UNITY_EDITOR
        public void Configure(RectTransform safe, Text progressLabel, Text tutorialLabel, GameObject card,
            Text resultLabel, Text subtitleLabel, RectTransform button, Text buttonLabel, Text hookLabel, RectTransform instructionPanel, RectTransform headerPanel, RectTransform backdrop)
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
            headerBackground = headerPanel;
            endBackdrop = backdrop;
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
            Resize(GetSafeArea(Screen.width, Screen.height, Screen.safeArea), Screen.width, Screen.height);
        }
        public void Resize(Rect safe, int width, int height)
        {
            safeRoot.anchorMin = new Vector2(safe.xMin / width, safe.yMin / height);
            safeRoot.anchorMax = new Vector2(safe.xMax / width, safe.yMax / height);
            safeRoot.offsetMin = safeRoot.offsetMax = Vector2.zero;
            headerBackground.anchorMin = new Vector2(0f, safeRoot.anchorMax.y);
            Vector2 span = safeRoot.anchorMax - safeRoot.anchorMin;
            // The shade covers the canvas while text and CTA keep their safe-area layout.
            endBackdrop.anchorMin = new Vector2(-safeRoot.anchorMin.x / span.x, -safeRoot.anchorMin.y / span.y);
            endBackdrop.anchorMax = new Vector2((1f - safeRoot.anchorMin.x) / span.x, (1f - safeRoot.anchorMin.y) / span.y);
        }
        public static Rect GetSafeArea(int width, int height, Rect reported)
        {
            // Playworks 7.2 reports the full viewport, including the preview's notch overlay.
            bool portrait = height >= width;
            float side = width * (portrait ? 0.02f : 0.06f);
            float top = height * (portrait ? 0.065f : 0.02f);
            float bottom = height * 0.04f;
            return Rect.MinMaxRect(Mathf.Max(reported.xMin, side), Mathf.Max(reported.yMin, bottom),
                Mathf.Min(reported.xMax, width - side), Mathf.Min(reported.yMax, height - top));
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
                endTitle.transform.localScale = subtitle.transform.localScale = Vector3.one * (1f - 0.05f * (1f - t) * (1f - t));
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
