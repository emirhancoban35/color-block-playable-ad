#if UNITY_EDITOR
using System;
using System.Reflection;
using Playable.Editor;
using Playable.View;
using UnityEditor.SceneManagement;
using UnityEngine;

// Copy into an isolated verification project's Editor folder, then run in batch mode.
public static class ResponsiveLayoutChecks
{
    public static void Run()
    {
        PlayableSceneBuilder.VerifyProject();
        var sizes = new[] { new Vector2Int(375, 812), new Vector2Int(390, 844),
            new Vector2Int(430, 932), new Vector2Int(600, 1000), new Vector2Int(812, 375) };
        foreach (var size in sizes)
        {
            Rect full = new Rect(0, 0, size.x, size.y);
            Rect fallback = PlayableHud.GetSafeArea(size.x, size.y, full);
            Require(fallback.yMax < size.y && fallback.yMin > 0, "Full viewport must reserve top and bottom space");
            Rect reported = Rect.MinMaxRect(size.x * .1f, size.y * .1f, size.x * .9f, size.y * .9f);
            Rect protectedArea = PlayableHud.GetSafeArea(size.x, size.y, reported);
            Require(reported.Contains(protectedArea.min) && protectedArea.xMax <= reported.xMax &&
                protectedArea.yMax <= reported.yMax, "Reported insets must never be reduced");
            Render(size, fallback);
        }
        Debug.Log("RESPONSIVE_LAYOUT_PASSED: 5 phone/orientation sizes; full-viewport fallback, real insets preserved, white background at screen top, title and CTA inside protected area; end-card shade covers all four screen corners during and after its entrance animation.");
    }

    private static void Render(Vector2Int size, Rect safe)
    {
        EditorSceneManager.OpenScene(PlayableSceneBuilder.ScenePath);
        var bootstrap = UnityEngine.Object.FindFirstObjectByType<Playable.PlayableBootstrap>();
        typeof(Playable.PlayableBootstrap).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(bootstrap, null);
        Camera camera = bootstrap.BoardCamera;
        camera.aspect = (float)size.x / size.y;
        camera.orthographicSize = Mathf.Max(21.6f / (2f * .62f * safe.height / size.y),
            17.6f / (2f * camera.aspect * .9f * safe.width / size.x));
        var target = new RenderTexture(size.x, size.y, 24);
        camera.targetTexture = target;
        var canvas = bootstrap.Hud.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
        canvas.planeDistance = 1f;
        // Simulate the SDK reporting the full viewport, using the actual layout policy.
        var root = (RectTransform)bootstrap.Hud.transform.Find("Safe Area");
        bootstrap.Hud.Resize(safe, size.x, size.y);
        Canvas.ForceUpdateCanvases();
        var protectedCorners = new Vector3[4];
        root.GetWorldCorners(protectedCorners);
        foreach (string path in new[] { "Header/Title", "CTA" })
        {
            var corners = new Vector3[4];
            ((RectTransform)root.Find(path)).GetWorldCorners(corners);
            Require(corners[0].x >= protectedCorners[0].x - .001f && corners[0].y >= protectedCorners[0].y - .001f &&
                corners[2].x <= protectedCorners[2].x + .001f && corners[2].y <= protectedCorners[2].y + .001f,
                path + " leaves protected area at " + size);
        }
        camera.Render();
        RenderTexture.active = target;
        var image = new Texture2D(size.x, size.y, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, size.x, size.y), 0, 0);
        image.Apply();
        Color top = image.GetPixel(size.x / 2, size.y - 2);
        Require(top.r > .99f && top.g > .99f && top.b > .99f, "White background leaves top gap");
        System.IO.File.WriteAllBytes("/private/tmp/colorblock-responsive-" + size.x + "x" + size.y + ".png", image.EncodeToPNG());
        bootstrap.Hud.EndCard(true, true, true, "YOU GOT IT!");
        foreach (float delta in new[] { 0.01f, 0.3f })
        {
            bootstrap.Hud.TickVisuals(delta, false);
            Canvas.ForceUpdateCanvases();
            camera.Render();
            image.ReadPixels(new Rect(0, 0, size.x, size.y), 0, 0);
            image.Apply();
            foreach (int x in new[] { 1, size.x - 2 })
                foreach (int y in new[] { 1, size.y - 2 })
                {
                    Color corner = image.GetPixel(x, y);
                    Require(Mathf.Max(corner.r, Mathf.Max(corner.g, corner.b)) < .4f,
                        "End-card shade leaves a screen-edge gap at " + size);
                }
        }
        System.IO.File.WriteAllBytes("/private/tmp/colorblock-endcard-" + size.x + "x" + size.y + ".png", image.EncodeToPNG());
        camera.targetTexture = null;
        RenderTexture.active = null;
        UnityEngine.Object.DestroyImmediate(target);
        UnityEngine.Object.DestroyImmediate(image);
    }
    private static void Require(bool condition, string message)
    { if (!condition) throw new InvalidOperationException(message); }
}
#endif
