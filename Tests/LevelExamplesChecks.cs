#if UNITY_EDITOR
using System;
using System.IO;
using System.Reflection;
using Data.Variant;
using Playable.Core;
using Playable.Editor;
using Playable.Flow;
using Playable.View;
using UnityEditor;
using UnityEngine;

// Copy into an isolated verification project's Editor folder to run in batch mode.
public static class LevelExamplesChecks
{
    public static void Run()
    {
        foreach (string name in new[] { "Heart", "Shapes", "OneWay" })
        {
            var variant = AssetDatabase.LoadAssetAtPath<PlayableVariantConfig>("Assets/_Project/Configs/Variant_" + name + ".asset");
            var level = variant.levelConfig;
            var board = new GridBoard(level.width, level.height, level.cells.ToArray(), level.blocks.ToArray(), level.exits.ToArray());
            if (name == "Heart")
            {
                Require(!board.IsActive(new Vector2Int(0, 0)) && !board.IsActive(new Vector2Int(4, 9)), "Heart cutouts lost");
                Clear(board, 0, Vector2Int.down); Clear(board, 1, Vector2Int.down);
                Clear(board, 2, Vector2Int.left); Clear(board, 3, Vector2Int.left);
                Clear(board, 4, Vector2Int.right); Clear(board, 5, Vector2Int.right);
                Clear(board, 6, Vector2Int.up); Clear(board, 7, Vector2Int.up);
                Clear(board, 8, Vector2Int.left); Clear(board, 9, Vector2Int.right);
                for (int i = 10; i < 12; i++)
                {
                    Vector2Int direction = i == 10 ? Vector2Int.right : Vector2Int.left;
                    for (int step = 0; step < 2; step++) Require(board.TryStep(i, direction) == MoveResult.Moved, "Heart center route blocked");
                    Clear(board, i, Vector2Int.down);
                }
            }
            else if (name == "Shapes")
            {
                var directions = new[] { Vector2Int.left, Vector2Int.right, Vector2Int.left, Vector2Int.right,
                    Vector2Int.down, Vector2Int.up, Vector2Int.up, Vector2Int.up };
                for (int i = 0; i < directions.Length; i++) Clear(board, i, directions[i]);
            }
            else
            {
                var directions = new[] { Vector2Int.right, Vector2Int.down, Vector2Int.left, Vector2Int.up };
                for (int pair = 0; pair < 4; pair++)
                    for (int i = pair * 2; i < pair * 2 + 2; i++)
                    {
                        Require(board.PreviewStep(i, -directions[pair]) == MoveResult.Blocked, "One-way block moved backward");
                        Clear(board, i, directions[pair]);
                    }
                foreach (var direction in directions) Require(board.PreviewStep(8, direction) == MoveResult.Blocked, "Locked block moved");
            }
            int expected = name == "OneWay" ? 8 : board.BlockCount;
            Require(board.ClearedCount == expected, name + " solution incomplete");
            var session = new AdSession(variant.adFlowConfig);
            session.Evaluate(board.ClearedCount, board.BlockCount);
            Require(session.Won, name + " did not reach its configured win condition");
            PlayableSceneBuilder.Prepare(variant);
            VerifyDirectionSymbols(variant);
            Capture(name, variant);
            Debug.Log("LEVEL_EXAMPLE_PASSED: " + name + "; " + expected + " blocks solved; win condition and scene preparation passed.");
        }
        PlayableSceneBuilder.VerifyProject();
        StressSceneChecks.Run();
        Debug.Log("LEVEL_EXAMPLES_PASSED: three examples solved and rendered; default stress scene restored and verified.");
    }

    private static void Clear(GridBoard board, int index, Vector2Int direction)
    {
        for (int steps = 0; steps < 25; steps++)
        {
            MoveResult result = board.TryStep(index, direction);
            if (result == MoveResult.Cleared) return;
            Require(result == MoveResult.Moved, "Blocked solution at block " + index + " moving " + direction);
        }
        throw new InvalidOperationException("Exit not reached");
    }

    private static void Capture(string name, PlayableVariantConfig variant)
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(PlayableSceneBuilder.ScenePath);
        var bootstrap = UnityEngine.Object.FindFirstObjectByType<Playable.PlayableBootstrap>();
        typeof(Playable.PlayableBootstrap).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(bootstrap, null);
        bootstrap.Hud.Tutorial(true);
        const int width = 600, height = 1000;
        Rect safe = PlayableHud.GetSafeArea(width, height, new Rect(0, 0, width, height));
        Camera camera = bootstrap.BoardCamera;
        camera.aspect = (float)width / height;
        camera.orthographicSize = Mathf.Max((variant.levelConfig.height + 1.6f) / (2f * .62f * safe.height / height),
            (variant.levelConfig.width + 1.6f) / (2f * camera.aspect * .9f * safe.width / width));
        var target = new RenderTexture(width, height, 24) { antiAliasing = 4 };
        camera.targetTexture = target;
        var canvas = bootstrap.Hud.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = camera;
        canvas.planeDistance = 1f;
        var root = (RectTransform)bootstrap.Hud.transform.Find("Safe Area");
        root.anchorMin = new Vector2(safe.xMin / width, safe.yMin / height);
        root.anchorMax = new Vector2(safe.xMax / width, safe.yMax / height);
        root.offsetMin = root.offsetMax = Vector2.zero;
        ((RectTransform)bootstrap.Hud.transform.Find("Header Background")).anchorMin = new Vector2(0, root.anchorMax.y);
        Canvas.ForceUpdateCanvases();
        camera.Render();
        RenderTexture.active = target;
        var image = new Texture2D(width, height, TextureFormat.RGB24, false);
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();
        File.WriteAllBytes("/private/tmp/colorblock-example-" + name + ".png", image.EncodeToPNG());
        camera.targetTexture = null;
        RenderTexture.active = null;
        UnityEngine.Object.DestroyImmediate(target);
        UnityEngine.Object.DestroyImmediate(image);
    }

    private static void VerifyDirectionSymbols(PlayableVariantConfig variant)
    {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(PlayableSceneBuilder.ScenePath);
        var view = UnityEngine.Object.FindFirstObjectByType<Playable.PlayableBootstrap>().BoardView;
        foreach (var block in variant.levelConfig.blocks)
        {
            var transform = view.transform.Find(block.id);
            Mesh mesh = transform.GetComponent<MeshFilter>().sharedMesh;
            int symbols = 0;
            foreach (Color32 color in mesh.colors32) if (color.a == 0) symbols++;
            bool both = block.movementMode == Data.Core.MovementMode.HorizontalOnly || block.movementMode == Data.Core.MovementMode.VerticalOnly;
            bool directional = block.movementMode != Data.Core.MovementMode.Free && block.movementMode != Data.Core.MovementMode.Locked;
            Require(symbols == (directional ? (both ? 10 : 7) : 0), "Incorrect baked direction symbol: " + block.id);
            Require(transform.childCount == 0, "Direction symbols must not add child objects");
        }
    }
    private static void Require(bool condition, string message)
    { if (!condition) throw new InvalidOperationException(message); }
}
#endif
