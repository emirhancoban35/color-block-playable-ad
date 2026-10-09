using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Playable.Editor
{
    internal static class BackgroundSceneBuilder
    {
        public static void Build(Transform camera, Color tint)
        {
            const string meshPath = "Assets/_Project/Generated/Meshes/Background.asset";
            const string materialPath = "Assets/_Project/Materials/MovingWater.mat";
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(meshPath);
            if (mesh == null)
            {
                const int columns = 6, rows = 12;
                Vector3[] vertices = new Vector3[(columns + 1) * (rows + 1)];
                int[] triangles = new int[columns * rows * 6];
                for (int y = 0; y <= rows; y++)
                    for (int x = 0; x <= columns; x++)
                        vertices[y * (columns + 1) + x] = new Vector3(x * 2f / columns - 1f, y * 2f / rows - 1f, 0f);
                int index = 0;
                for (int y = 0; y < rows; y++)
                    for (int x = 0; x < columns; x++)
                    {
                        int a = y * (columns + 1) + x, b = a + columns + 1;
                        triangles[index++] = a; triangles[index++] = b; triangles[index++] = a + 1;
                        triangles[index++] = a + 1; triangles[index++] = b; triangles[index++] = b + 1;
                    }
                mesh = new Mesh { name = "Background", vertices = vertices, triangles = triangles };
                mesh.RecalculateBounds();
                AssetDatabase.CreateAsset(mesh, meshPath);
            }
            Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);
            if (material == null)
            {
                material = new Material(AssetDatabase.LoadAssetAtPath<Shader>("Assets/_Project/Shaders/MovingWater.shader"));
                AssetDatabase.CreateAsset(material, materialPath);
            }
            material.SetColor("_Color", tint);
            EditorUtility.SetDirty(material);
            GameObject background = new GameObject("Moving Background", typeof(MeshFilter), typeof(MeshRenderer));
            background.transform.SetParent(camera, false);
            background.transform.localPosition = Vector3.forward * 20f;
            background.GetComponent<MeshFilter>().sharedMesh = mesh;
            MeshRenderer renderer = background.GetComponent<MeshRenderer>();
            renderer.sharedMaterial = material;
            renderer.shadowCastingMode = ShadowCastingMode.Off;
            renderer.receiveShadows = false;
        }
    }
}
