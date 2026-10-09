using System.Collections.Generic;
using UnityEngine;

namespace Playable.View
{
    // Geometry is generated once at startup; all faces share one white material.
    internal sealed class FlatMeshBuilder
    {
        private readonly List<Vector3> vertices = new List<Vector3>();
        private readonly List<Color> colors = new List<Color>();
        private readonly List<int> triangles = new List<int>();

        public void Tile(Vector2 center, float left, float right, float bottom, float top, float radius, Color color,
            bool joinLeft = false, bool joinRight = false, bool joinBottom = false, bool joinTop = false, bool stud = false)
        {
            Vector2[] outer = Outline(left, right, bottom, top, radius, joinLeft, joinRight, joinBottom, joinTop);
            Vector2[] inner = Outline(left + (joinLeft ? 0f : 0.045f), right - (joinRight ? 0f : 0.045f),
                bottom + (joinBottom ? 0f : 0.045f), top - (joinTop ? 0f : 0.045f), Mathf.Max(0f, radius - 0.035f),
                joinLeft, joinRight, joinBottom, joinTop);
            Fill(outer, center + new Vector2(0f, -0.045f), 0.025f, Shade(color, 0.47f), false);
            Fill(inner, center, -0.01f, color, true);
            for (int i = 0; i < outer.Length; i++)
            {
                int next = (i + 1) % outer.Length;
                Color edge = Shade(color, outer[i].y > 0f ? 1.18f : 0.65f);
                Quad(center + outer[i], center + outer[next], center + inner[next], center + inner[i], 0f, edge);
            }
            if (stud)
            {
                Disc(center + new Vector2(0f, -0.027f), 0.255f, -0.025f, Shade(color, 0.6f));
                Disc(center + new Vector2(0f, 0.018f), 0.24f, -0.03f, Shade(color, 1.16f));
                Disc(center, 0.222f, -0.035f, color);
            }
        }

        public void Rect(float x, float y, float width, float height, float z, Color color)
        {
            Quad(new Vector2(x, y), new Vector2(x + width, y), new Vector2(x + width, y + height), new Vector2(x, y + height), z, color);
        }

        public void Arrow(Vector2 center, Vector2 direction, Color color)
        {
            Vector2 side = new Vector2(-direction.y, direction.x) * 0.13f;
            int start = vertices.Count;
            Add(center + direction * 0.14f, -0.08f, color);
            Add(center - direction * 0.1f + side, -0.08f, color);
            Add(center - direction * 0.1f - side, -0.08f, color);
            Triangle(start, start + 1, start + 2);
        }

        private static Color Shade(Color color, float amount)
        {
            return new Color(Mathf.Min(1f, color.r * amount), Mathf.Min(1f, color.g * amount), Mathf.Min(1f, color.b * amount), 1f);
        }

        private static Vector2[] Outline(float left, float right, float bottom, float top, float radius, bool jl, bool jr, bool jb, bool jt)
        {
            Vector2[] result = new Vector2[16];
            for (int corner = 0; corner < 4; corner++)
            {
                bool rightCorner = corner == 0 || corner == 3;
                bool topCorner = corner < 2;
                bool round = !(rightCorner ? jr : jl) && !(topCorner ? jt : jb);
                float r = round ? Mathf.Min(radius, Mathf.Min(right - left, top - bottom) * 0.5f) : 0f;
                float x = rightCorner ? right : left;
                float y = topCorner ? top : bottom;
                Vector2 arcCenter = new Vector2(x + (rightCorner ? -r : r), y + (topCorner ? -r : r));
                for (int point = 0; point < 4; point++)
                {
                    float angle = (corner * 90f + point * 30f) * Mathf.Deg2Rad;
                    result[corner * 4 + point] = arcCenter + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * r;
                }
            }
            return result;
        }

        private void Fill(Vector2[] outline, Vector2 offset, float z, Color color, bool gradient)
        {
            int center = vertices.Count;
            Add(offset, z, color);
            for (int i = 0; i < outline.Length; i++)
                Add(offset + outline[i], z, gradient ? Shade(color, 0.93f + outline[i].y * 0.14f) : color);
            for (int i = 0; i < outline.Length; i++) Triangle(center, center + 1 + i, center + 1 + (i + 1) % outline.Length);
        }

        private void Disc(Vector2 center, float radius, float z, Color color)
        {
            int start = vertices.Count;
            Add(center, z, color);
            for (int i = 0; i < 16; i++)
            {
                float a = i * Mathf.PI / 8f;
                Add(center + new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * radius, z, color);
            }
            for (int i = 0; i < 16; i++) Triangle(start, start + 1 + i, start + 1 + (i + 1) % 16);
        }

        private void Quad(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float z, Color color)
        {
            int start = vertices.Count;
            Add(a, z, color); Add(b, z, color); Add(c, z, color); Add(d, z, color);
            Triangle(start, start + 1, start + 2); Triangle(start, start + 2, start + 3);
        }
        private void Add(Vector2 point, float z, Color color) { vertices.Add(new Vector3(point.x, point.y, z)); colors.Add(color); }
        private void Triangle(int a, int b, int c) { triangles.Add(a); triangles.Add(b); triangles.Add(c); }

        public Mesh Build(string name)
        {
            Mesh mesh = new Mesh();
            mesh.name = name;
            if (vertices.Count > 65535) mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mesh.vertices = vertices.ToArray();
            mesh.colors = colors.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateBounds();
            return mesh;
        }
    }
}
