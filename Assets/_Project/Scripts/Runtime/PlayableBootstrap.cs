using Data.Core;
using Data.Variant;
using Playable.Core;
using Playable.Flow;
using Playable.Platform;
using Playable.View;
using UnityEngine;
using Utilities;

namespace Playable
{
    public sealed class PlayableBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayableVariantConfig variant;
        [SerializeField] private BoardView view;
        private GridBoard board;
        [SerializeField] private PlayableHud hud;
        private PlayablePlatform platform;
        private AdSession session;
        [SerializeField] private Camera boardCamera;
        private int selected = -1, escaping = -1;
        private Vector2 grabOffset;
        private Vector3 escapeStart, escapeTarget;
        private Vector3 burstOrigin;
        private Vector2 escapeDirection;
        private bool escapeBurstStarted;
        private float escapeTime, endTime;
        private bool gestureMoved, paused, interacted, presented;
        private int screenWidth, screenHeight;
        private Rect safeArea;

        public PlayableVariantConfig Variant { get { return variant; } }
        public BoardView BoardView { get { return view; } }
        public PlayableHud Hud { get { return hud; } }
        public Camera BoardCamera { get { return boardCamera; } }
#if UNITY_EDITOR
        public void Configure(PlayableVariantConfig config, BoardView boardView, Camera camera, PlayableHud preparedHud)
        {
            variant = config;
            boardCamera = camera;
            hud = preparedHud;
            view = boardView;
        }
#endif

        private void Start()
        {
            platform = new PlayablePlatform();
#if !UNITY_EDITOR
            if (!platform.IsAvailable) { Debug.LogError("Playworks SDK is required for a release build."); enabled = false; return; }
#endif
            platform.PauseChanged += SetPaused;
            var level = variant.levelConfig;
            board = new GridBoard(level.width, level.height, level.cells.ToArray(), level.blocks.ToArray(), level.exits.ToArray());
            session = new AdSession(variant.adFlowConfig);
            boardCamera.backgroundColor = variant.visualTheme.backgroundColor;
            hud.Initialize(variant.adFlowConfig);
            hud.Progress(0, board.BlockCount, 0);
            Input.simulateMouseWithTouches = true;
            Application.targetFrameRate = 60;
            Resize();
            if (variant.adFlowConfig.showTutorial) platform.TutorialStarted();
        }

        private void Update()
        {
            if (board == null || paused) return;
            if (screenWidth != Screen.width || screenHeight != Screen.height || safeArea != Screen.safeArea) Resize();
            float dt = Time.deltaTime;
            var flow = variant.adFlowConfig;
            session.Tick(dt);
            if (Input.GetMouseButtonDown(0))
            {
                if (hud.HitCta(Input.mousePosition)) { platform.Install(); return; }
                if (!session.Ended && escaping < 0 && !view.BurstPlaying)
                {
                    Vector3 world = PointerWorld();
                    selected = board.BlockAt(GridMath.WorldToGrid(world, 1f));
                    if (selected >= 0)
                    {
                        view.SelectBlock(selected);
                        Vector2Int origin = board.Origin(selected);
                        grabOffset = new Vector2(world.x - origin.x, world.y - origin.y);
                        interacted = true;
                        gestureMoved = false;
                    }
                }
            }
            if (selected >= 0 && Input.GetMouseButton(0) && !session.Ended) Drag();
            if (selected >= 0 && Input.GetMouseButtonUp(0)) FinishGesture();
            float blend = Mathf.Min(1f, dt * variant.moveSpeed);
            view.TickMotion(blend);
            if (escaping >= 0)
            {
                escapeTime += dt;
                float t = Mathf.Clamp01(escapeTime / variant.exitDuration);
                if (!escapeBurstStarted)
                {
                    view.PlaceBlock(escaping, Vector3.Lerp(escapeStart, escapeTarget, t));
                    if (t >= 0.35f)
                    {
                        view.HideBlock(escaping);
                        view.Burst(escaping, burstOrigin, escapeDirection, variant.exitDuration);
                        escapeBurstStarted = true;
                    }
                }
                if (t >= 1f) { view.HideBlock(escaping); escaping = -1; }
            }
            view.TickBurst(dt);
            if (escaping < 0 && !view.BurstPlaying) session.Evaluate(board.ClearedCount, board.BlockCount);
            if (session.Ended)
            {
                if (selected >= 0) FinishGesture();
                endTime += dt;
                if (!presented && endTime >= flow.endCardDelay)
                {
                    presented = true;
                    bool showCard = !session.Won || flow.showEndCardOnWin;
                    hud.EndCard(session.Won, showCard, flow.ctaMode == CtaMode.FullScreenEndCardClick, flow.endCardTitle);
                    platform.GameEnded(session.Won, showCard);
                }
            }
            hud.Tutorial(flow.showTutorial && !interacted && session.Elapsed >= flow.tutorialDelay && !session.Ended);
            hud.Cta(flow.ctaMode == CtaMode.PersistentButton || (flow.ctaMode == CtaMode.DelayedButton && session.Elapsed >= flow.ctaDelay) || presented);
        }

        private Vector3 PointerWorld()
        {
            Vector3 point = Input.mousePosition;
            point.z = 10f;
            return transform.InverseTransformPoint(boardCamera.ScreenToWorldPoint(point));
        }

        private void Drag()
        {
            Vector3 world = PointerWorld();
            Vector2 desired = new Vector2(world.x, world.y) - grabOffset;
            // Cardinal steps prevent a fast pointer from tunnelling through another block.
            for (int step = 0; step < board.Width + board.Height; step++)
            {
                Vector2Int origin = board.Origin(selected);
                Vector2 delta = desired - new Vector2(origin.x, origin.y);
                bool horizontal = Mathf.Abs(delta.x) >= Mathf.Abs(delta.y);
                Vector2Int first = horizontal ? new Vector2Int(delta.x > 0 ? 1 : -1, 0) : new Vector2Int(0, delta.y > 0 ? 1 : -1);
                float amount = horizontal ? Mathf.Abs(delta.x) : Mathf.Abs(delta.y);
                if (amount < 0.55f) break;
                MoveResult result = board.TryStep(selected, first);
                Vector2Int direction = first;
                if (result == MoveResult.Blocked)
                {
                    float secondAmount = horizontal ? Mathf.Abs(delta.y) : Mathf.Abs(delta.x);
                    if (secondAmount < 0.55f) break;
                    direction = horizontal ? new Vector2Int(0, delta.y > 0 ? 1 : -1) : new Vector2Int(delta.x > 0 ? 1 : -1, 0);
                    result = board.TryStep(selected, direction);
                    if (result == MoveResult.Blocked) break;
                }
                gestureMoved = true;
                if (result == MoveResult.Moved)
                    view.MoveTo(selected, GridMath.GridToWorld(board.Origin(selected), 1f, -0.1f));
                if (result == MoveResult.Cleared)
                {
                    view.StopMoving(selected);
                    escaping = selected;
                    escapeTime = 0f;
                    escapeBurstStarted = false;
                    escapeDirection = direction;
                    burstOrigin = view.BlockCenter(selected);
                    burstOrigin.z = -0.3f;
                    if (direction.x != 0) burstOrigin.x = direction.x > 0 ? board.Width - 0.3f : -0.7f;
                    else burstOrigin.y = direction.y > 0 ? board.Height - 0.3f : -0.7f;
                    escapeStart = GridMath.GridToWorld(board.Origin(selected), 1f, -0.15f);
                    int travel = 1;
                    for (int c = 0; c < board.CellCount(selected); c++)
                    {
                        Vector2Int local = board.LocalCell(selected, c);
                        int axis = direction.x != 0 ? local.x : local.y;
                        travel = Mathf.Max(travel, Mathf.Abs(axis) + 2);
                    }
                    escapeTarget = escapeStart + new Vector3(direction.x, direction.y, 0f) * travel;
                    FinishGesture();
                    break;
                }
            }
        }

        private void FinishGesture()
        {
            view.ClearSelection();
            if (gestureMoved && !session.Ended)
            {
                session.RecordMove();
                if (session.Moves == 1) platform.FirstMoveCompleted();
                hud.Progress(board.ClearedCount, board.BlockCount, session.Moves);
            }
            selected = -1;
            gestureMoved = false;
        }
        public void Complete(bool won) { if (session != null) session.Complete(won); }
        private void SetPaused(bool value) { paused = value; if (value && selected >= 0) FinishGesture(); }
        private void OnApplicationPause(bool value) { SetPaused(value); }
        private void OnApplicationFocus(bool value) { SetPaused(!value); }
        private void Resize()
        {
            screenWidth = Mathf.Max(1, Screen.width); screenHeight = Mathf.Max(1, Screen.height); safeArea = Screen.safeArea;
            float safeWidth = Mathf.Max(1f, safeArea.width), safeHeight = Mathf.Max(1f, safeArea.height);
            float usableWidth = safeWidth / screenWidth * 0.9f;
            float usableHeight = safeHeight / screenHeight * 0.62f;
            float aspect = (float)screenWidth / screenHeight;
            boardCamera.orthographicSize = Mathf.Max((board.Height + 1.6f) / (2f * usableHeight), (board.Width + 1.6f) / (2f * aspect * usableWidth));
            hud.Resize();
        }
        private void OnDestroy()
        {
            if (platform != null) { platform.PauseChanged -= SetPaused; platform.Dispose(); }
        }
    }
}
