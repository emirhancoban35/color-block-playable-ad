using UnityEngine;

namespace Playable.View
{
    public sealed class ExitBurst : MonoBehaviour
    {
        [SerializeField] private Transform[] pieces = new Transform[0];
        private Vector3[] velocities;
        private MeshRenderer[] renderers;
        private MaterialPropertyBlock properties;
        private float elapsed, duration;
        public bool IsPlaying { get; private set; }

        private void OnEnable() { Initialize(); }
        private void Initialize()
        {
            velocities = new Vector3[pieces.Length];
            renderers = new MeshRenderer[pieces.Length];
            properties = new MaterialPropertyBlock();
            for (int i = 0; i < pieces.Length; i++) renderers[i] = pieces[i].GetComponent<MeshRenderer>();
        }
#if UNITY_EDITOR
        public void Configure(Transform[] transforms) { pieces = transforms; Initialize(); }
#endif
        public void Begin(Vector3 origin, Vector2 direction, Color color, float seconds)
        {
            elapsed = 0f;
            duration = Mathf.Max(0.5f, seconds * 2.5f);
            IsPlaying = true;
            Color tint = QualitySettings.activeColorSpace == ColorSpace.Linear ? color.linear : color;
            properties.SetVector("_Color", tint);
            Vector2 side = new Vector2(-direction.y, direction.x);
            for (int i = 0; i < pieces.Length; i++)
            {
                float spread = (i - (pieces.Length - 1) * 0.5f) / pieces.Length;
                velocities[i] = (Vector3)(direction * (4.5f + (i % 3) * 1.2f) + side * spread * 7f);
                pieces[i].localPosition = origin + (Vector3)(side * spread * 1.6f);
                pieces[i].localRotation = Quaternion.Euler(0f, 0f, i * 47f);
                pieces[i].localScale = Vector3.one * (2f + (i % 3) * 0.4f);
                // All fragments intentionally share one immutable tint during this burst.
                renderers[i].SetPropertyBlock(properties);
                renderers[i].enabled = true;
            }
        }
        public void Tick(float dt)
        {
            if (!IsPlaying) return;
            elapsed += dt;
            float t = Mathf.Clamp01(elapsed / duration);
            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].localPosition += velocities[i] * dt;
                pieces[i].localRotation = Quaternion.Euler(0f, 0f, i * 47f + elapsed * (i % 2 == 0 ? 430f : -430f));
                pieces[i].localScale = Vector3.one * (2f + (i % 3) * 0.4f) * (1f - t * t);
                if (t >= 1f) renderers[i].enabled = false;
            }
            if (t >= 1f) IsPlaying = false;
        }
    }
}
