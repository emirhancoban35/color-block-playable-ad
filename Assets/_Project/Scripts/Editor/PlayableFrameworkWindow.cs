using System;
using System.Collections.Generic;
using System.IO;
using Data.Core;
using Data.Level;
using Data.Variant;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Playable.Editor
{
    public sealed class PlayableFrameworkWindow : EditorWindow
    {
        private PlayableVariantConfig variant;
        private int selectedBlock;
        private Vector2 scroll;
        private UnityEditor.Editor variantEditor, levelEditor, themeEditor, flowEditor;
        private List<string> errors = new List<string>();

        [MenuItem("Tools/Color Block/Framework")]
        public static void Open() { GetWindow<PlayableFrameworkWindow>("Color Block"); }

        private void OnEnable()
        {
            if (variant == null) variant = AssetDatabase.LoadAssetAtPath<PlayableVariantConfig>(PlayableSceneBuilder.DefaultVariantPath);
            RefreshEditors();
        }
        private void OnDisable() { ClearEditors(); }
        private void ClearEditors()
        {
            if (variantEditor != null) DestroyImmediate(variantEditor);
            if (levelEditor != null) DestroyImmediate(levelEditor);
            if (themeEditor != null) DestroyImmediate(themeEditor);
            if (flowEditor != null) DestroyImmediate(flowEditor);
        }
        private void RefreshEditors()
        {
            ClearEditors();
            variantEditor = levelEditor = themeEditor = flowEditor = null;
            if (variant == null) return;
            variantEditor = UnityEditor.Editor.CreateEditor(variant);
            if (variant.levelConfig != null) levelEditor = UnityEditor.Editor.CreateEditor(variant.levelConfig);
            if (variant.visualTheme != null) themeEditor = UnityEditor.Editor.CreateEditor(variant.visualTheme);
            if (variant.adFlowConfig != null) flowEditor = UnityEditor.Editor.CreateEditor(variant.adFlowConfig);
            errors = VariantValidator.Validate(variant);
        }

        private void OnGUI()
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            EditorGUILayout.LabelField("COLOR BLOCK PLAYABLE", EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();
            variant = (PlayableVariantConfig)EditorGUILayout.ObjectField("Active Variant", variant, typeof(PlayableVariantConfig), false);
            if (EditorGUI.EndChangeCheck()) RefreshEditors();
            if (variant != null)
            {
                if (variant.levelConfig != (levelEditor == null ? null : levelEditor.target) ||
                    variant.visualTheme != (themeEditor == null ? null : themeEditor.target) ||
                    variant.adFlowConfig != (flowEditor == null ? null : flowEditor.target)) RefreshEditors();
                DrawBoard();
                EditorGUILayout.Space();
                DrawInspector("Variant", variantEditor);
                DrawInspector("Level / shapes / gates", levelEditor);
                DrawInspector("Theme / palette", themeEditor);
                DrawInspector("Ad flow", flowEditor);
                if (GUILayout.Button("Validate variant")) errors = VariantValidator.Validate(variant);
                if (GUILayout.Button("Prepare playable scene"))
                {
                    errors = VariantValidator.Validate(variant);
                    if (errors.Count == 0)
                    {
                        try
                        {
                            PlayableSceneBuilder.Prepare(variant);
                            Selection.activeObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(PlayableSceneBuilder.ScenePath);
                            EditorGUIUtility.PingObject(Selection.activeObject);
                        }
                        catch (InvalidOperationException error) { errors.Add(error.Message); }
                    }
                }
                foreach (string error in errors) EditorGUILayout.HelpBox(error, MessageType.Error);
            }
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Drag a block on the grid to reposition it. Shift-click toggles an obstacle. Edit shapes, gate spans, colors and limits below. Prepare exports only the selected variant.", MessageType.Info);
            if (GUILayout.Button("Connect Playworks SDK…")) ConnectSdk();
            EditorGUILayout.EndScrollView();
        }

        private static void DrawInspector(string title, UnityEditor.Editor editor)
        {
            if (editor == null) return;
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
            editor.OnInspectorGUI();
            EditorGUILayout.Space();
        }

        private void DrawBoard()
        {
            LevelConfig level = variant.levelConfig;
            if (level == null || level.width < 1 || level.height < 1 || level.width > 64 || level.height > 64 || level.blocks == null) return;
            if (level.blocks.Count > 0)
            {
                string[] names = new string[level.blocks.Count];
                for (int i = 0; i < names.Length; i++) names[i] = level.blocks[i] == null ? "Missing block" : level.blocks[i].id;
                selectedBlock = EditorGUILayout.Popup("Selected Block", Mathf.Clamp(selectedBlock, 0, names.Length - 1), names);
            }
            float cell = Mathf.Min(42f, (position.width - 32f) / level.width);
            Rect area = GUILayoutUtility.GetRect(level.width * cell, level.height * cell);
            for (int y = 0; y < level.height; y++)
                for (int x = 0; x < level.width; x++)
                {
                    Rect rect = new Rect(area.x + x * cell, area.y + (level.height - 1 - y) * cell, cell - 2, cell - 2);
                    Color color = new Color(0.24f, 0.27f, 0.34f);
                    Vector2Int point = new Vector2Int(x, y);
                    for (int b = 0; b < level.blocks.Count; b++)
                    {
                        var block = level.blocks[b];
                        if (block != null && block.localCells != null && block.localCells.Contains(point - block.origin))
                            color = variant.visualTheme == null ? Color.white : variant.visualTheme.GetColor(block.colorId);
                    }
                    if (level.cells != null)
                        foreach (var item in level.cells)
                            if (item != null && item.position == point && (!item.isActive || item.isBlocker)) color = Color.black;
                    EditorGUI.DrawRect(rect, color);
                    Event input = Event.current;
                    if ((input.type == EventType.MouseDown || input.type == EventType.MouseDrag) && input.button == 0 && rect.Contains(input.mousePosition))
                    {
                        Undo.RecordObject(level, "Edit board");
                        if (input.shift && input.type == EventType.MouseDown)
                        {
                            if (level.cells == null) level.cells = new List<CellData>();
                            int index = level.cells.FindIndex(c => c != null && c.position == point);
                            if (index >= 0) level.cells.RemoveAt(index);
                            else level.cells.Add(new CellData { position = point, isBlocker = true });
                        }
                        else if (!input.shift && level.blocks.Count > 0 && level.blocks[selectedBlock] != null) level.blocks[selectedBlock].origin = point;
                        EditorUtility.SetDirty(level);
                        input.Use();
                        Repaint();
                    }
                }
        }

        private static void ConnectSdk()
        {
            string path = EditorUtility.OpenFilePanel("Select Playworks scripts/package.json", "", "json");
            if (string.IsNullOrEmpty(path)) return;
            if (Path.GetFileName(path) != "package.json" || !Directory.Exists(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(path)), "pipeline")))
            {
                EditorUtility.DisplayDialog("Playworks SDK", "Select scripts/package.json inside the extracted SDK folder (with its pipeline and tools folders).", "OK");
                return;
            }
            Client.Add("file:" + Path.GetDirectoryName(path));
        }
    }

    [InitializeOnLoad]
    internal static class SdkDefineSync
    {
        static SdkDefineSync() { EditorApplication.delayCall += Sync; }
        private static void Sync()
        {
            bool found = false;
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                if (assembly.GetType("Luna.Unity.Playable") != null) { found = true; break; }
            var target = UnityEditor.Build.NamedBuildTarget.FromBuildTargetGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            string current = PlayerSettings.GetScriptingDefineSymbols(target);
            List<string> defines = new List<string>(current.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
            bool has = defines.Contains("PLAYWORKS_SDK");
            if (found == has) return;
            if (found) defines.Add("PLAYWORKS_SDK"); else defines.Remove("PLAYWORKS_SDK");
            PlayerSettings.SetScriptingDefineSymbols(target, string.Join(";", defines.ToArray()));
        }
    }
}
