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
            bootstrap.Configure(variant, Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf"));
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
            Shader shader = AssetDatabase.LoadAssetAtPath<Shader>("Assets/_Project/Shaders/VertexColor.shader");
            foreach (var message in ShaderUtil.GetShaderMessages(shader))
                if (message.severity == UnityEditor.Rendering.ShaderCompilerMessageSeverity.Error) throw new InvalidOperationException(message.message);
            Debug.Log("COLOR_BLOCK_VERIFIED: variants, scene generation and shader import passed.");
        }
    }
}
