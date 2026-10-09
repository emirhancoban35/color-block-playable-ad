using Data.Level;
using Data.Theme;
using Playable.Core;
using UnityEngine;
using Utilities;

namespace Playable.View
{
    public sealed class BoardView
    {
        private readonly Transform[] blocks;
        private readonly Mesh[] meshes;
        private readonly GameObject root;

        public BoardView(GridBoard board, LevelConfig level, VisualThemeConfig theme, Transform parent)
        {
            root = new GameObject("Board View");
            root.transform.SetParent(parent, false);
            blocks = new Transform[board.BlockCount];
            meshes = new Mesh[board.BlockCount + 1];
            FlatMeshBuilder background = new FlatMeshBuilder();
            background.Rect(-0.85f, -0.85f, board.Width + 0.7f, board.Height + 0.7f, 0.5f, theme.borderColor);
            for (int y = 0; y < board.Height; y++)
                for (int x = 0; x < board.Width; x++)
                {
                    Vector2Int cell = new Vector2Int(x, y);
                    if (!board.IsActive(cell)) continue;
                    Color color = board.IsBlocked(cell) ? theme.borderColor : theme.boardColor;
                    background.Tile(new Vector2(x, y), -0.48f, 0.48f, -0.48f, 0.48f, 0.08f, color);
                }
            for (int i = 0; i < level.exits.Count; i++)
            {
                ExitGateData gate = level.exits[i];
                Vector2Int dir = ExitSideUtility.ToDirection(gate.side);
                Vector2 center;
                bool horizontal = dir.y != 0;
                if (horizontal) center = new Vector2(gate.startIndex + (gate.length - 1) * 0.5f, dir.y > 0 ? board.Height - 0.3f : -0.7f);
                else center = new Vector2(dir.x > 0 ? board.Width - 0.3f : -0.7f, gate.startIndex + (gate.length - 1) * 0.5f);
                float halfWidth = horizontal ? gate.length * 0.5f - 0.03f : 0.17f;
                float halfHeight = horizontal ? 0.17f : gate.length * 0.5f - 0.03f;
                background.Tile(center, -halfWidth, halfWidth, -halfHeight, halfHeight, 0.06f, theme.GetColor(gate.colorId));
                background.Arrow(center, dir, Color.white);
            }
            meshes[board.BlockCount] = background.Build("Board and gates");
            MakeMesh("Board", meshes[board.BlockCount], theme.sharedMaterial, root.transform).localPosition = new Vector3(0, 0, 0.2f);

            for (int b = 0; b < board.BlockCount; b++)
            {
                FlatMeshBuilder builder = new FlatMeshBuilder();
                for (int c = 0; c < board.CellCount(b); c++)
                {
                    Vector2Int cell = board.LocalCell(b, c);
                    bool left = Contains(board, b, cell + Vector2Int.left), right = Contains(board, b, cell + Vector2Int.right);
                    bool bottom = Contains(board, b, cell + Vector2Int.down), top = Contains(board, b, cell + Vector2Int.up);
                    float edge = 0.5f - theme.cellGap * 0.5f;
                    builder.Tile(cell, left ? -0.5f : -edge, right ? 0.5f : edge, bottom ? -0.5f : -edge, top ? 0.5f : edge,
                        theme.cornerRadius, theme.GetColor(board.Color(b)), left, right, bottom, top, theme.showStuds);
                }
                meshes[b] = builder.Build("Block " + b);
                blocks[b] = MakeMesh("Block " + b, meshes[b], theme.sharedMaterial, root.transform);
                blocks[b].localPosition = GridMath.GridToWorld(board.Origin(b), 1f, -0.1f);
            }
        }

        private static bool Contains(GridBoard board, int block, Vector2Int cell)
        {
            for (int i = 0; i < board.CellCount(block); i++) if (board.LocalCell(block, i) == cell) return true;
            return false;
        }

        private static Transform MakeMesh(string name, Mesh mesh, Material material, Transform parent)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent, false);
            go.AddComponent<MeshFilter>().sharedMesh = mesh;
            MeshRenderer renderer = go.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = material;
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;
            return go.transform;
        }

        public void UpdateBlock(int index, Vector3 target, float blend)
        {
            blocks[index].localPosition = Vector3.Lerp(blocks[index].localPosition, target, blend);
        }
        public void HideBlock(int index) { blocks[index].gameObject.SetActive(false); }
        public void Dispose()
        {
            for (int i = 0; i < meshes.Length; i++) Object.Destroy(meshes[i]);
            Object.Destroy(root);
        }
    }
}
