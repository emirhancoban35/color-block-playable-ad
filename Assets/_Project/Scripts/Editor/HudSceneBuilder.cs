using Data.Flow;
using Playable.View;
using UnityEngine;
using UnityEngine.UI;

namespace Playable.Editor
{
    internal static class HudSceneBuilder
    {
        public static PlayableHud Build(Font font, Font displayFont, AdFlowConfig flow, Transform parent)
        {
            GameObject root = new GameObject("HUD", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler));
            root.transform.SetParent(parent, false);
            root.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = root.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.matchWidthOrHeight = 0.5f;
            RectTransform headerBackground = NewRect("Header Background", root.transform);
            headerBackground.anchorMin = new Vector2(0f, 1f);
            headerBackground.anchorMax = Vector2.one;
            headerBackground.offsetMin = new Vector2(0f, -170f);
            headerBackground.offsetMax = Vector2.zero;
            Image headerImage = headerBackground.gameObject.AddComponent<Image>();
            headerImage.color = Color.white;
            headerImage.raycastTarget = false;
            RectTransform safeRoot = NewRect("Safe Area", root.transform);
            RectTransform header = NewRect("Header", safeRoot);
            header.anchorMin = new Vector2(0f, 1f);
            header.anchorMax = Vector2.one;
            header.pivot = new Vector2(0.5f, 1f);
            header.sizeDelta = new Vector2(0f, 170f);
            header.anchoredPosition = Vector2.zero;
            Text title = Label("Title", flow.hookText, font, 64, header, new Vector2(0.5f, 0.5f), new Vector2(1000, 145));
            title.rectTransform.anchorMin = new Vector2(0f, 0.5f);
            title.rectTransform.anchorMax = new Vector2(1f, 0.5f);
            title.rectTransform.sizeDelta = new Vector2(-80f, 145f);
            title.fontStyle = FontStyle.Bold;
            title.color = new Color(0.03f, 0.3f, 0.65f);
            Text progress = Label("Progress", "", font, 38, safeRoot, new Vector2(0.5f, 0.86f), new Vector2(900, 70));
            progress.color = title.color;
            RectTransform instruction = NewRect("Instruction", safeRoot);
            instruction.anchorMin = instruction.anchorMax = new Vector2(0.5f, 0.145f);
            instruction.sizeDelta = new Vector2(950, 115);
            Text tutorial = Label("Tutorial", flow.tutorialText, displayFont, 54, instruction, new Vector2(0.5f, 0.5f), new Vector2(920, 110));
            Outlined(tutorial);
            GameObject endCard = new GameObject("End Card", typeof(RectTransform));
            endCard.transform.SetParent(safeRoot, false);
            Stretch(endCard.GetComponent<RectTransform>());
            RectTransform backdrop = NewRect("Backdrop", endCard.transform);
            Stretch(backdrop);
            Image shade = backdrop.gameObject.AddComponent<Image>();
            shade.color = new Color(0.08f, 0.1f, 0.2f, 0.93f);
            shade.raycastTarget = false;
            Text endTitle = Label("Result", flow.endCardTitle, displayFont, 80, endCard.transform, new Vector2(0.5f, 0.6f), new Vector2(1000, 160));
            Text subtitle = Label("Subtitle", flow.endCardSubtitle, font, 38, endCard.transform, new Vector2(0.5f, 0.49f), new Vector2(1000, 120));
            endCard.SetActive(false);
            RectTransform cta = NewRect("CTA", safeRoot);
            cta.anchorMin = cta.anchorMax = new Vector2(0.5f, 0f);
            cta.pivot = new Vector2(0.5f, 0f);
            cta.anchoredPosition = new Vector2(0f, 32f);
            cta.sizeDelta = new Vector2(420, 145);
            RoundedButtonGraphic button = cta.gameObject.AddComponent<RoundedButtonGraphic>();
            button.color = new Color(0.02f, 0.58f, 0.94f);
            button.raycastTarget = false;
            Text ctaText = Label("CTA Text", flow.ctaText, displayFont, 66, cta, new Vector2(0.5f, 0.5f), new Vector2(400, 130));
            cta.gameObject.SetActive(flow.ctaMode == Data.Core.CtaMode.PersistentButton);
            instruction.gameObject.SetActive(false);
            PlayableHud hud = root.AddComponent<PlayableHud>();
            Outlined(ctaText);
            hud.Configure(safeRoot, progress, tutorial, endCard, endTitle, subtitle, cta, ctaText, title, instruction, headerBackground, backdrop);
            return hud;
        }

        private static void Outlined(Text text)
        {
            text.fontStyle = FontStyle.Normal;
            text.color = Color.white;
            Outline outline = text.gameObject.AddComponent<Outline>();
            outline.effectColor = new Color(0.025f, 0.035f, 0.055f);
            outline.effectDistance = new Vector2(5f, -5f);
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
