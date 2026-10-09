#if UNITY_EDITOR
using System;
using Data.Variant;
using Playable.Core;
using Playable.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// Copy into an isolated verification project's Editor folder to run in batch mode.
public static class StressSceneChecks
{
    public static void Run()
    {
        var variant = AssetDatabase.LoadAssetAtPath<PlayableVariantConfig>("Assets/_Project/Configs/Variant_Stress.asset");
        var level = variant.levelConfig;
        var board = new GridBoard(level.width, level.height, level.cells.ToArray(), level.blocks.ToArray(), level.exits.ToArray());
        for (int i = 0; i < board.BlockCount; i++)
        {
            while (board.Origin(i).y > 0)
                Require(board.TryStep(i, Vector2Int.down) == MoveResult.Moved, "Bottom lane unreachable");
            int target = level.exits.Find(gate => gate.colorId == board.Color(i)).startIndex;
            while (board.Origin(i).x != target)
                Require(board.TryStep(i, board.Origin(i).x < target ? Vector2Int.right : Vector2Int.left) == MoveResult.Moved, "Bottom lane blocked");
            Require(board.TryStep(i, Vector2Int.down) == MoveResult.Cleared, "Matching exit failed");
        }
        Require(board.ClearedCount == 80, "Stress level must contain 80 solvable blocks");
        EditorSceneManager.OpenScene(PlayableSceneBuilder.ScenePath);
        var view = UnityEngine.Object.FindFirstObjectByType<Playable.PlayableBootstrap>().BoardView;
        Mesh shape = view.transform.Find(level.blocks[0].id).GetComponent<MeshFilter>().sharedMesh;
        int vertices = 0, triangles = 0;
        var transforms = new Transform[level.blocks.Count];
        var targets = new Vector3[level.blocks.Count];
        for (int i = 0; i < level.blocks.Count; i++)
        {
            var block = view.transform.Find(level.blocks[i].id);
            Require(block.GetComponent<MeshFilter>().sharedMesh == shape, "Repeated shapes duplicated mesh data");
            transforms[i] = block;
            targets[i] = block.localPosition + Vector3.right * 0.1f;
            for (int repeat = 0; repeat < 10; repeat++) view.MoveTo(i, targets[i]);
        }
        view.TickMotion(1f);
        for (int i = 0; i < transforms.Length; i++)
            Require(transforms[i].localPosition == targets[i], "Motion queue lost a block or duplicated a target");
        foreach (var filter in view.GetComponentsInChildren<MeshFilter>())
        { vertices += filter.sharedMesh.vertexCount; triangles += filter.sharedMesh.triangles.Length / 3; }
        var live = new GridBoard(level.width, level.height, level.cells.ToArray(), level.blocks.ToArray(), level.exits.ToArray());
        for (int i = 0; i < 100; i++) live.PreviewStep(i % 80, Vector2Int.down);
        long previewStart = GC.GetAllocatedBytesForCurrentThread();
        for (int i = 0; i < 10000; i++) live.PreviewStep(i % 80, Vector2Int.down);
        long previewAllocated = GC.GetAllocatedBytesForCurrentThread() - previewStart;
        Require(previewAllocated == 0, "Hint preview allocated managed memory");
        for (int i = 0; i < transforms.Length; i++) view.MoveTo(i, targets[i] + Vector3.right * 100f);
        view.TickMotion(0.001f);
        long motionStart = GC.GetAllocatedBytesForCurrentThread();
        for (int i = 0; i < 1000; i++) view.TickMotion(0.00001f);
        long motionAllocated = GC.GetAllocatedBytesForCurrentThread() - motionStart;
        Require(motionAllocated == 0, "Active motion allocated managed memory");
        view.TickMotion(1f);
        int objects = view.GetComponentsInChildren<Transform>(true).Length;
        for (int i = 0; i < 100; i++) view.TickMotion(0.2f);
        long before = GC.GetAllocatedBytesForCurrentThread();
        for (int i = 0; i < 10000; i++) view.TickMotion(0.2f);
        long allocated = GC.GetAllocatedBytesForCurrentThread() - before;
        Require(allocated == 0, "Idle motion allocated managed memory");
        Require(objects == view.GetComponentsInChildren<Transform>(true).Length, "Motion created scene objects");
        Debug.Log($"STRESS_PASSED: 80 blocks solved; shared shape {shape.vertexCount} vertices; visible geometry {vertices} vertices / {triangles} triangles; 10000 idle motion ticks allocated {allocated} bytes; 10000 hint previews allocated {previewAllocated} bytes; 1000 ticks moving all 80 blocks allocated {motionAllocated} bytes; object count {objects} unchanged.");
    }
    static void Require(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
}
#endif
