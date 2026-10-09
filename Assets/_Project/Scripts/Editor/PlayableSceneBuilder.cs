using System;
using System.IO;
using Data.Variant;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Playable.Editor
{
    public static class PlayableSceneBuilder
    {
        public const string ScenePath = "Assets/_Project/Scenes/Playable_2D.unity";
        public const string DefaultVariantPath = "Assets/_Project/Configs/Variant_A.asset";

        public static void Prepare(PlayableVariantConfig variant)
        {
            var errors = VariantValidator.Validate(variant);
            if (errors.Count > 0) throw new InvalidOperationException(string.Join("\n", errors.ToArray()));
            Scene scene = SceneManager.GetSceneByPath(ScenePath);
            bool alreadyOpen = scene.IsValid() && scene.isLoaded;
            if (!alreadyOpen)
            {
                bool emptyInitialScene = SceneManager.sceneCount == 1 && string.IsNullOrEmpty(SceneManager.GetActiveScene().path) && !SceneManager.GetActiveScene().isDirty;
                for (int i = 0; i < SceneManager.sceneCount; i++)
                    if (!emptyInitialScene && string.IsNullOrEmpty(SceneManager.GetSceneAt(i).path))
                        throw new InvalidOperationException("Save your untitled scene before preparing the playable.");
                scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, emptyInitialScene ? NewSceneMode.Single : NewSceneMode.Additive);
            }
            PlayableBootstrap bootstrap = null;
            foreach (GameObject item in scene.GetRootGameObjects())
                if (item.GetComponent<PlayableBootstrap>() != null) bootstrap = item.GetComponent<PlayableBootstrap>();
            if (bootstrap == null)
            {
                GameObject root = new GameObject("Color Block Playable");
                SceneManager.MoveGameObjectToScene(root, scene);
                bootstrap = root.AddComponent<PlayableBootstrap>();
            }
            Undo.RecordObject(bootstrap, "Select playable variant");
            var board = BoardSceneBuilder.Build(variant, bootstrap);
            if (bootstrap.BoardCamera != null) Undo.DestroyObjectImmediate(bootstrap.BoardCamera.gameObject);
            if (bootstrap.Hud != null) Undo.DestroyObjectImmediate(bootstrap.Hud.gameObject);
            Camera camera = new GameObject("Board Camera", typeof(Camera)).GetComponent<Camera>();
            camera.transform.SetParent(bootstrap.transform, false);
            camera.orthographic = true;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = variant.visualTheme.backgroundColor;
            camera.nearClipPlane = 0.1f;
            camera.farClipPlane = 30f;
            camera.orthographicSize = Mathf.Max(variant.levelConfig.height, variant.levelConfig.width) * 0.5f + 1f;
            camera.transform.localPosition = new Vector3((variant.levelConfig.width - 1) * 0.5f,
                (variant.levelConfig.height - 1) * 0.5f, -10f);
            Undo.RegisterCreatedObjectUndo(camera.gameObject, "Prepare camera");
            var hud = HudSceneBuilder.Build(Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"), variant.adFlowConfig, bootstrap.transform);
            Undo.RegisterCreatedObjectUndo(hud.gameObject, "Prepare HUD");
            bootstrap.Configure(variant, board, camera, hud);
            EditorUtility.SetDirty(bootstrap);
            Directory.CreateDirectory("Assets/_Project/Scenes");
            EditorSceneManager.SaveScene(scene, ScenePath);
            if (!alreadyOpen && SceneManager.sceneCount > 1) EditorSceneManager.CloseScene(scene, true);
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
            AssetDatabase.SaveAssets();
        }

        // Used by batch verification; it exercises the actual scene builder and shader import.
        public static void VerifyProject()
        {
            PlayableVariantConfig variant = AssetDatabase.LoadAssetAtPath<PlayableVariantConfig>(DefaultVariantPath);
            Prepare(variant);
            foreach (string guid in AssetDatabase.FindAssets("t:PlayableVariantConfig", new[] { "Assets/_Project/Configs" }))
            {
                var item = AssetDatabase.LoadAssetAtPath<PlayableVariantConfig>(AssetDatabase.GUIDToAssetPath(guid));
                var errors = VariantValidator.Validate(item);
                if (errors.Count > 0) throw new InvalidOperationException(string.Join("\n", errors.ToArray()));
            }
            VerifyBoardRebuild();
            Shader shader = AssetDatabase.LoadAssetAtPath<Shader>("Assets/_Project/Shaders/VertexColor.shader");
            foreach (var message in ShaderUtil.GetShaderMessages(shader))
                if (message.severity == UnityEditor.Rendering.ShaderCompilerMessageSeverity.Error) throw new InvalidOperationException(message.message);
            Debug.Log("COLOR_BLOCK_VERIFIED: variants, board rebuild, shared meshes and shader import passed.");
        }

        private static void VerifyBoardRebuild()
        {
            Scene scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
            PlayableBootstrap bootstrap = scene.GetRootGameObjects()[0].GetComponent<PlayableBootstrap>();
            PlayableVariantConfig variant = bootstrap.Variant;
            var previous = bootstrap.BoardView;
            MaterialPropertyBlock tint = new MaterialPropertyBlock();
            previous.transform.Find("red").GetComponent<MeshRenderer>().GetPropertyBlock(tint);
            Color expectedTint = variant.visualTheme.GetColor(Data.Core.ColorId.Red);
            if (QualitySettings.activeColorSpace == ColorSpace.Linear) expectedTint = expectedTint.linear;
            if ((tint.GetVector("_Color") - (Vector4)expectedTint).sqrMagnitude > 0.00000001f)
                throw new InvalidOperationException("Block colors were not restored after reopening the scene.");
            GameObject manualObject = new GameObject("Manual object");
            manualObject.transform.SetParent(bootstrap.transform, false);
            Prepare(variant);
            if (previous != null || bootstrap.GetComponentsInChildren<Playable.View.BoardView>().Length != 1)
                throw new InvalidOperationException("Rebuild left a duplicate board.");
            if (bootstrap.GetComponentsInChildren<Camera>(true).Length != 1 || bootstrap.BoardCamera == null ||
                bootstrap.GetComponentsInChildren<Playable.View.PlayableHud>(true).Length != 1 || bootstrap.Hud == null)
                throw new InvalidOperationException("Prepared camera and HUD must be unique and referenced.");
            var ctaText = bootstrap.Hud.transform.Find("Safe Area/CTA/CTA Text").GetComponent<UnityEngine.UI.Text>();
            string originalCta = variant.adFlowConfig.ctaText;
            variant.adFlowConfig.ctaText = "OVERRIDE CHECK";
            bootstrap.Hud.Initialize(variant.adFlowConfig);
            bool overrideApplied = ctaText.text == "OVERRIDE CHECK";
            variant.adFlowConfig.ctaText = originalCta;
            bootstrap.Hud.Initialize(variant.adFlowConfig);
            if (!overrideApplied) throw new InvalidOperationException("Prepared HUD must apply runtime flow overrides.");
            if (manualObject == null || manualObject.transform.parent != bootstrap.transform)
                throw new InvalidOperationException("Rebuild removed a manually placed object.");
            if (bootstrap.BoardView.BlockCount != variant.levelConfig.blocks.Count)
                throw new InvalidOperationException("Prepared block count does not match the level.");
            MeshFilter blue = bootstrap.BoardView.transform.Find("blue").GetComponent<MeshFilter>();
            MeshFilter purple = bootstrap.BoardView.transform.Find("purple").GetComponent<MeshFilter>();
            if (blue.sharedMesh != purple.sharedMesh || !EditorUtility.IsPersistent(blue.sharedMesh))
                throw new InvalidOperationException("Equal shapes must share a persistent mesh.");
            foreach (var filter in bootstrap.BoardView.GetComponentsInChildren<MeshFilter>())
                if (filter.sharedMesh.GetVertexAttributeFormat(UnityEngine.Rendering.VertexAttribute.Color) != UnityEngine.Rendering.VertexAttributeFormat.UNorm8)
                    throw new InvalidOperationException("Prepared meshes must store colors in four bytes per vertex.");
            UnityEngine.Object.DestroyImmediate(manualObject);
            EditorSceneManager.SaveScene(scene);
        }
    }
}
