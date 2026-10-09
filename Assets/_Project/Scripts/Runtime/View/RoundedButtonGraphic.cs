using UnityEngine;
using UnityEngine.UI;

namespace Playable.View
{
    [RequireComponent(typeof(CanvasRenderer))]
    public sealed class RoundedButtonGraphic : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper mesh)
        {
            mesh.Clear();
            Rect bounds = GetPixelAdjustedRect();
            Rect shadow = bounds;
            shadow.y -= 8f;
            Fill(mesh, shadow, 24f, new Color(0.02f, 0.12f, 0.2f), false);
            Fill(mesh, bounds, 24f, new Color(0.02f, 0.2f, 0.32f), false);
            Rect face = new Rect(bounds.x + 5f, bounds.y + 7f, bounds.width - 10f, bounds.height - 12f);
            Fill(mesh, face, 20f, color, true);
        }

        private static void Fill(VertexHelper mesh, Rect rect, float radius, Color tint, bool gradient)
        {
            radius = Mathf.Min(radius, Mathf.Min(rect.width, rect.height) * 0.5f);
            int start = mesh.currentVertCount;
            mesh.AddVert(rect.center, tint, Vector2.zero);
            for (int corner = 0; corner < 4; corner++)
            {
                bool right = corner == 0 || corner == 3, top = corner < 2;
                Vector2 center = new Vector2(right ? rect.xMax - radius : rect.xMin + radius,
                    top ? rect.yMax - radius : rect.yMin + radius);
                for (int point = 0; point < 4; point++)
                {
                    float angle = (corner * 90f + point * 30f) * Mathf.Deg2Rad;
                    Vector2 position = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                    Color shade = gradient ? Color.Lerp(tint * 0.86f, Color.Lerp(tint, Color.white, 0.22f), (position.y - rect.yMin) / rect.height) : tint;
                    shade.a = tint.a;
                    mesh.AddVert(position, shade, Vector2.zero);
                }
            }
            for (int i = 0; i < 16; i++) mesh.AddTriangle(start, start + 1 + i, start + 1 + (i + 1) % 16);
        }
    }
}
