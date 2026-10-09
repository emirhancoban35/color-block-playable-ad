using Data.Core;
using Data.Flow;

namespace Playable.Flow
{
    public sealed class AdSession
    {
        private readonly AdFlowConfig config;
        public float Elapsed { get; private set; }
        public int Moves { get; private set; }
        public float InteractionTime { get; private set; }
        public bool StoreReady { get { return config.storeAfterInteraction > 0f && InteractionTime >= config.storeAfterInteraction; } }
        public bool Ended { get; private set; }
        public bool Won { get; private set; }

        public AdSession(AdFlowConfig config) { this.config = config; }
        public void Tick(float delta) { if (!Ended) Elapsed += delta; }
        public void TickInteraction(float delta)
        {
            if (!Ended && config.storeAfterInteraction > 0f && !StoreReady)
                InteractionTime = UnityEngine.Mathf.Min(config.storeAfterInteraction, InteractionTime + delta);
        }
        public void RecordMove() { if (!Ended) Moves++; }
        public void Complete(bool won) { if (Ended) return; Ended = true; Won = won; }

        public void Evaluate(int cleared, int total)
        {
            if (Ended) return;
            if (config.endCondition != EndCondition.Manual && cleared == total) { Complete(true); return; }
            switch (config.endCondition)
            {
                case EndCondition.OnTargetBlocksCleared:
                    if (cleared >= config.targetBlocksToClear) Complete(true);
                    break;
                case EndCondition.OnMoveCountReached:
                    if (Moves >= config.moveLimit) Complete(false);
                    break;
                case EndCondition.OnTimerExpired:
                    if (Elapsed >= config.timer) Complete(false);
                    break;
            }
        }
    }
}
