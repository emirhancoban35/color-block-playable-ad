using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Data.Variant;
using Playable.Core;
using Playable.View;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Playable.Editor
{
    internal static class BoardSceneBuilder
    {
        public const string MeshFolder = "Assets/_Project/Generated/Meshes";

        public static BoardView Build(PlayableVariantConfig variant, PlayableBootstrap bootstrap)
        {
            if (!Directory.Exists(MeshFolder))
            {
                Directory.CreateDirectory(MeshFolder);
                AssetDatabase.Refresh();
            }
            if (bootstrap.BoardView != null) Undo.DestroyObjectImmediate(bootstrap.BoardView.gameObject);

            var level = variant.levelConfig;
            var theme = variant.visualTheme;
            GridBoard board = new GridBoard(level.width, level.height, level.cells.ToArray(), level.blocks.ToArray(), level.exits.ToArray());
            GameObject root = new GameObject("Generated Board");
            root.transform.SetParent(bootstrap.transform, false);

            FlatMeshBuilder background = new FlatMeshBuilder();
            background.RoundedRect(new Vector2((board.Width - 1) * 0.5f, (board.Height - 1) * 0.5f),
                board.Width + 0.7f, board.Height + 0.7f, 0.3f, 0.5f, theme.borderColor);
            for (int y = 0; y < board.Height; y++)
                for (int x = 0; x < board.Width; x++)
                {
                    Vector2Int cell = new Vector2Int(x, y);
                    if (!board.IsActive(cell)) continue;
                    background.Tile(cell, -0.48f, 0.48f, -0.48f, 0.48f, 0.08f, board.IsBlocked(cell) ? theme.borderColor : theme.boardColor);
                }
            foreach (var gate in level.exits)
            {
                Vector2Int direction = ExitSideUtility.ToDirection(gate.side);
                bool horizontal = direction.y != 0;
                Vector2 center = horizontal
                    ? new Vector2(gate.startIndex + (gate.length - 1) * 0.5f, direction.y > 0 ? board.Height - 0.3f : -0.7f)
                    : new Vector2(direction.x > 0 ? board.Width - 0.3f : -0.7f, gate.startIndex + (gate.length - 1) * 0.5f);
                float halfWidth = horizontal ? gate.length * 0.5f - 0.03f : 0.17f;
                float halfHeight = horizontal ? 0.17f : gate.length * 0.5f - 0.03f;
                background.Tile(center, -halfWidth, halfWidth, -halfHeight, halfHeight, 0.12f, theme.GetColor(gate.colorId));
                background.Arrow(center, direction, Color.white);
            }
            Mesh boardMesh = SaveBoardMesh(background.Build("Board and gates"));
            MakeMesh("Board and gates", boardMesh, theme.sharedMaterial, root.transform).localPosition = new Vector3(0, 0, 0.2f);

            Transform[] blocks = new Transform[board.BlockCount];
            Color[] colors = new Color[board.BlockCount];
            for (int b = 0; b < blocks.Length; b++)
            {
                Mesh shape = ShapeMesh(board, b, variant);
                blocks[b] = MakeMesh(level.blocks[b].id, shape, theme.sharedMaterial, root.transform);
                blocks[b].localPosition = GridMath.GridToWorld(board.Origin(b), 1f, -0.1f);
                colors[b] = theme.GetColor(board.Color(b));
            }
            BoardView view = root.AddComponent<BoardView>();
            view.Configure(blocks, colors);
            Undo.RegisterCreatedObjectUndo(root, "Build board");
            EditorUtility.SetDirty(view);
            return view;
        }

        private static Mesh ShapeMesh(GridBoard board, int block, PlayableVariantConfig variant)
        {
            var theme = variant.visualTheme;
            List<Vector2Int> cells = new List<Vector2Int>();
            for (int i = 0; i < board.CellCount(block); i++) cells.Add(board.LocalCell(block, i));
            cells.Sort((a, b) => a.y == b.y ? a.x.CompareTo(b.x) : a.y.CompareTo(b.y));
            StringBuilder key = new StringBuilder();
            foreach (Vector2Int cell in cells) key.Append(cell.x).Append(',').Append(cell.y).Append(';');
            key.Append(theme.cellGap.ToString("R", CultureInfo.InvariantCulture)).Append(';')
                .Append(theme.cornerRadius.ToString("R", CultureInfo.InvariantCulture)).Append(';').Append(theme.showStuds);
            string path = MeshFolder + "/Shape_" + Hash128.Compute(key.ToString()) + ".asset";
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);
            if (mesh != null) return mesh;

            FlatMeshBuilder builder = new FlatMeshBuilder();
            foreach (Vector2Int cell in cells)
            {
                bool left = cells.Contains(cell + Vector2Int.left), right = cells.Contains(cell + Vector2Int.right);
                bool bottom = cells.Contains(cell + Vector2Int.down), top = cells.Contains(cell + Vector2Int.up);
                float edge = 0.5f - theme.cellGap * 0.5f;
                builder.Tile(cell, left ? -0.5f : -edge, right ? 0.5f : edge, bottom ? -0.5f : -edge, top ? 0.5f : edge,
                    theme.cornerRadius, Color.white, left, right, bottom, top, theme.showStuds);
            }
            mesh = builder.Build("Block shape");
            AssetDatabase.CreateAsset(mesh, path);
            return mesh;
        }

        private static Mesh SaveBoardMesh(Mesh mesh)
        {
            string path = MeshFolder + "/Board.asset";
            Mesh existing = AssetDatabase.LoadAssetAtPath<Mesh>(path);
            if (existing == null) { AssetDatabase.CreateAsset(mesh, path); return mesh; }
            EditorUtility.CopySerialized(mesh, existing);
            EditorUtility.SetDirty(existing);
            Object.DestroyImmediate(mesh);
            return existing;
        }

        private static Transform MakeMesh(string name, Mesh mesh, Material material, Transform parent)
        {
            GameObject go = new GameObject(name, typeof(MeshFilter), typeof(MeshRenderer));
            go.transform.SetParent(parent, false);
            go.GetComponent<MeshFilter>().sharedMesh = mesh;
            MeshRenderer renderer = go.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = material;
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;
            return go.transform;
        }
    }
}
