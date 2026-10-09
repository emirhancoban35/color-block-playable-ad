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
                    if (board.IsBlocked(cell))
                        Wall(background, cell, 0.96f, 0.96f, theme.borderColor);
                    else
                    {
                        background.RoundedRect(cell, 0.96f, 0.96f, 0.08f, 0f, theme.boardColor * 0.8f);
                        background.RoundedRect((Vector2)cell + new Vector2(0f, -0.012f),
                            0.94f, 0.93f, 0.07f, -0.01f, theme.boardColor);
                    }
                }
            BuildWalls(background, level, theme.borderColor);
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
            Transform outline = MakeMesh("Selection Outline", blocks[0].GetComponent<MeshFilter>().sharedMesh, theme.sharedMaterial, root.transform);
            outline.gameObject.SetActive(false);
            view.Configure(blocks, colors, outline.GetComponent<MeshFilter>());
            Undo.RegisterCreatedObjectUndo(root, "Build board");
            EditorUtility.SetDirty(view);
            return view;
        }

        private static void BuildWalls(FlatMeshBuilder mesh, Data.Level.LevelConfig level, Color color)
        {
            for (int side = 0; side < 4; side++)
            {
                var exitSide = (Data.Core.ExitSide)side;
                Vector2Int direction = ExitSideUtility.ToDirection(exitSide);
                bool horizontal = direction.y != 0;
                int length = horizontal ? level.width : level.height;
                bool[] open = new bool[length];
                foreach (var gate in level.exits)
                    if (gate.side == exitSide)
                        for (int i = gate.startIndex; i < gate.startIndex + gate.length; i++) open[i] = true;
                for (int i = 0; i < length;)
                {
                    if (open[i]) { i++; continue; }
                    int start = i++;
                    while (i < length && !open[i]) i++;
                    float from = start - 0.5f - (start == 0 ? 0.12f : 0f);
                    float to = i - 0.5f + (i == length ? 0.12f : 0f);
                    float center = (from + to) * 0.5f;
                    Vector2 position = horizontal
                        ? new Vector2(center, direction.y > 0 ? level.height - 0.3f : -0.7f)
                        : new Vector2(direction.x > 0 ? level.width - 0.3f : -0.7f, center);
                    Wall(mesh, position, horizontal ? to - from - 0.02f : 0.34f,
                        horizontal ? 0.34f : to - from - 0.02f, color);
                }
            }
            for (int y = 0; y < 2; y++)
                for (int x = 0; x < 2; x++)
                {
                    Vector2 center = new Vector2(x == 0 ? -0.49f : level.width - 0.51f,
                        y == 0 ? -0.49f : level.height - 0.51f);
                    int corner = y == 0 ? (x == 0 ? 2 : 3) : (x == 0 ? 1 : 0);
                    mesh.RoundedCorner(center + new Vector2(0f, -0.035f), 0.38f, 0.04f, corner, 0.06f, color * 0.65f);
                    mesh.RoundedCorner(center, 0.38f, 0.04f, corner, -0.02f, color);
                    mesh.RoundedCorner(center + new Vector2(0f, 0.015f), 0.35f, 0.07f,
                        corner, -0.03f, Color.Lerp(color, Color.white, 0.12f));
                }
        }

        private static void Wall(FlatMeshBuilder mesh, Vector2 center, float width, float height, Color color)
        {
            mesh.RoundedRect(center + new Vector2(0f, -0.035f), width, height, 0.12f, 0.06f, color * 0.65f);
            mesh.RoundedRect(center, width, height, 0.12f, -0.02f, color);
            mesh.RoundedRect(center + new Vector2(0f, 0.015f), width - 0.06f, height - 0.06f,
                0.09f, -0.03f, Color.Lerp(color, Color.white, 0.12f));
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
            if (mesh != null)
            {
                if (mesh.GetVertexAttributeFormat(UnityEngine.Rendering.VertexAttribute.Color) != UnityEngine.Rendering.VertexAttributeFormat.UNorm8)
                {
                    mesh.colors32 = mesh.colors32;
                    EditorUtility.SetDirty(mesh);
                }
                return mesh;
            }

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
