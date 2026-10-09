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

        public int BlockCount { get { return blocks.Length; } }
        private void OnEnable() { ApplyColors(); }

#if UNITY_EDITOR
        public void Configure(Transform[] transforms, Color[] colors)
        {
            blocks = transforms;
            blockColors = colors;
            ApplyColors();
        }
#endif

        private void ApplyColors()
        {
            MaterialPropertyBlock properties = new MaterialPropertyBlock();
            for (int i = 0; i < blocks.Length; i++)
            {
                properties.SetColor("_Color", blockColors[i]);
                blocks[i].GetComponent<MeshRenderer>().SetPropertyBlock(properties);
            }
        }

        public void UpdateBlock(int index, Vector3 target, float blend)
        {
            blocks[index].localPosition = Vector3.Lerp(blocks[index].localPosition, target, blend);
        }

        public void HideBlock(int index) { blocks[index].gameObject.SetActive(false); }
    }
}
