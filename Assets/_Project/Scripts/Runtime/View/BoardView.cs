using UnityEngine;

namespace Playable.View
{
#if UNITY_EDITOR
    [ExecuteAlways]
#endif
    public sealed class BoardView : MonoBehaviour
    {
        [SerializeField] private Transform[] blocks = new Transform[0];
        [SerializeField] private Color[] blockColors = new Color[0];
        [SerializeField] private MeshFilter selectionOutline;
        [SerializeField] private ExitBurst exitBurst;
        private MaterialPropertyBlock outlineProperties;
        private int selectedBlock = -1;
        private Vector3[] targets;
        private int[] moving;
        private int movingCount;

        public int BlockCount { get { return blocks.Length; } }
        public bool BurstPlaying { get { return exitBurst.IsPlaying; } }
        private void OnEnable() { ResetMotion(); ApplyColors(); }

        private void ResetMotion()
        {
            targets = new Vector3[blocks.Length];
            moving = new int[blocks.Length];
            movingCount = 0;
            outlineProperties = new MaterialPropertyBlock();
            outlineProperties.SetVector("_Color", Vector4.one);
            outlineProperties.SetFloat("_Highlight", 1f);
        }

#if UNITY_EDITOR
        public void Configure(Transform[] transforms, Color[] colors, MeshFilter outline, ExitBurst burst)
        {
            blocks = transforms;
            blockColors = colors;
            selectionOutline = outline;
            exitBurst = burst;
            ResetMotion();
            ApplyColors();
        }
#endif

        public void SelectBlock(int index)
        {
            ClearSelection();
            selectedBlock = index;
            blocks[index].localScale = Vector3.one * 1.025f;
            Mesh mesh = blocks[index].GetComponent<MeshFilter>().sharedMesh;
            selectionOutline.sharedMesh = mesh;
            selectionOutline.transform.SetParent(blocks[index], false);
            selectionOutline.transform.localScale = Vector3.one * 1.04f;
            selectionOutline.transform.localPosition = mesh.bounds.center * -0.04f + Vector3.forward * 0.12f;
            selectionOutline.GetComponent<MeshRenderer>().SetPropertyBlock(outlineProperties);
            selectionOutline.gameObject.SetActive(true);
        }

        public void ClearSelection()
        {
            if (selectedBlock < 0) return;
            blocks[selectedBlock].localScale = Vector3.one;
            selectionOutline.gameObject.SetActive(false);
            selectedBlock = -1;
        }
        private void ApplyColors()
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                // Playworks keeps the property block reference; each renderer needs its own.
                MaterialPropertyBlock properties = new MaterialPropertyBlock();
                // SetVector avoids Unity/Playworks differences in implicit color conversion.
                Color tint = QualitySettings.activeColorSpace == ColorSpace.Linear ? blockColors[i].linear : blockColors[i];
                properties.SetVector("_Color", tint);
                blocks[i].GetComponent<MeshRenderer>().SetPropertyBlock(properties);
            }
        }

        public void MoveTo(int index, Vector3 target)
        {
            targets[index] = target;
            for (int i = 0; i < movingCount; i++) if (moving[i] == index) return;
            moving[movingCount++] = index;
        }

        public void TickMotion(float blend)
        {
            for (int i = movingCount - 1; i >= 0; i--)
            {
                int index = moving[i];
                Vector3 position = Vector3.Lerp(blocks[index].localPosition, targets[index], blend);
                if ((position - targets[index]).sqrMagnitude <= 0.000001f)
                {
                    position = targets[index];
                    moving[i] = moving[--movingCount];
                }
                blocks[index].localPosition = position;
            }
        }

        public void StopMoving(int index)
        {
            for (int i = 0; i < movingCount; i++)
                if (moving[i] == index) { moving[i] = moving[--movingCount]; return; }
        }

        public void PlaceBlock(int index, Vector3 position) { blocks[index].localPosition = position; }
        public void HideBlock(int index) { blocks[index].gameObject.SetActive(false); }
        public Vector3 BlockCenter(int index) { return blocks[index].localPosition + blocks[index].GetComponent<MeshFilter>().sharedMesh.bounds.center; }
        public void Burst(int index, Vector3 origin, Vector2 direction, float duration) { exitBurst.Begin(origin, direction, blockColors[index], duration); }
        public void TickBurst(float dt) { exitBurst.Tick(dt); }
    }
}
