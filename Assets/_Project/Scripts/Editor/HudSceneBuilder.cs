using Data.Flow;
using Playable.View;
using UnityEngine;
using UnityEngine.UI;

namespace Playable.Editor
{
    internal static class HudSceneBuilder
    {
        public static PlayableHud Build(Font font, AdFlowConfig flow, Transform parent)
        {
            GameObject root = new GameObject("HUD", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler));
            root.transform.SetParent(parent, false);
            root.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = root.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.matchWidthOrHeight = 0.5f;
            RectTransform safeRoot = NewRect("Safe Area", root.transform);
            Text title = Label("Title", "COLOR ESCAPE", font, 66, safeRoot, new Vector2(0.5f, 0.92f), new Vector2(1000, 110));
            title.color = new Color(0.17f, 0.19f, 0.3f);
            Text progress = Label("Progress", "", font, 38, safeRoot, new Vector2(0.5f, 0.86f), new Vector2(900, 70));
            progress.color = title.color;
            Text tutorial = Label("Tutorial", flow.tutorialText, font, 40, safeRoot, new Vector2(0.5f, 0.19f), new Vector2(1000, 90));
            tutorial.color = title.color;
            GameObject endCard = new GameObject("End Card", typeof(RectTransform), typeof(Image));
            endCard.transform.SetParent(safeRoot, false);
            Stretch(endCard.GetComponent<RectTransform>());
            endCard.GetComponent<Image>().color = new Color(0.08f, 0.1f, 0.2f, 0.93f);
            endCard.GetComponent<Image>().raycastTarget = false;
            Text endTitle = Label("Result", flow.endCardTitle, font, 80, endCard.transform, new Vector2(0.5f, 0.6f), new Vector2(1000, 160));
            Text subtitle = Label("Subtitle", flow.endCardSubtitle, font, 38, endCard.transform, new Vector2(0.5f, 0.49f), new Vector2(1000, 120));
            endCard.SetActive(false);
            RectTransform cta = NewRect("CTA", safeRoot);
            cta.anchorMin = cta.anchorMax = new Vector2(0.5f, 0.085f);
            cta.sizeDelta = new Vector2(690, 130);
            Image button = cta.gameObject.AddComponent<Image>();
            button.color = new Color(0.28f, 0.75f, 0.13f);
            button.raycastTarget = false;
            Text ctaText = Label("CTA Text", flow.ctaText, font, 55, cta, new Vector2(0.5f, 0.5f), new Vector2(670, 120));
            cta.gameObject.SetActive(flow.ctaMode == Data.Core.CtaMode.PersistentButton);
            tutorial.gameObject.SetActive(false);
            PlayableHud hud = root.AddComponent<PlayableHud>();
            hud.Configure(safeRoot, progress, tutorial, endCard, endTitle, subtitle, cta, ctaText);
            return hud;
        }

        private static RectTransform NewRect(string name, Transform parent)
        {
            RectTransform rect = new GameObject(name, typeof(RectTransform)).GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            return rect;
        }
        private static void Stretch(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one;
            rect.offsetMin = rect.offsetMax = Vector2.zero;
        }
        private static Text Label(string name, string content, Font font, int size, Transform parent, Vector2 anchor, Vector2 dimensions)
        {
            RectTransform rect = NewRect(name, parent);
            rect.anchorMin = rect.anchorMax = anchor;
            rect.sizeDelta = dimensions;
            Text text = rect.gameObject.AddComponent<Text>();
            text.font = font; text.fontSize = size; text.text = content;
            text.alignment = TextAnchor.MiddleCenter; text.color = Color.white;
            text.raycastTarget = false;
            return text;
        }
    }
}
