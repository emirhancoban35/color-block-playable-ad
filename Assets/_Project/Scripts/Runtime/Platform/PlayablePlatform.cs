using System;
using UnityEngine;

namespace Playable.Platform
{
    // SDK calls have one boundary. Unity previews work before the SDK is installed.
    public sealed class PlayablePlatform : IDisposable
    {
        public event Action<bool> PauseChanged;
        private bool ended;
        public bool IsAvailable
        {
            get
            {
#if PLAYWORKS_SDK
                return true;
#else
                return false;
#endif
            }
        }

        public PlayablePlatform()
        {
#if PLAYWORKS_SDK
            Luna.Unity.LifeCycle.OnPause += Pause;
            Luna.Unity.LifeCycle.OnResume += Resume;
#endif
        }
        private void Pause() { if (PauseChanged != null) PauseChanged(true); }
        private void Resume() { if (PauseChanged != null) PauseChanged(false); }
        public void TutorialStarted()
        {
#if PLAYWORKS_SDK
            Luna.Unity.Analytics.LogEvent(Luna.Unity.Analytics.EventType.TutorialStarted);
#endif
        }
        public void FirstMoveCompleted()
        {
#if PLAYWORKS_SDK
            Luna.Unity.Analytics.LogEvent("FirstMoveCompleted", 1);
#endif
        }
        public void GameEnded(bool won, bool endCardShown)
        {
            if (ended) return;
            ended = true;
#if PLAYWORKS_SDK
            Luna.Unity.Analytics.LogEvent(won ? Luna.Unity.Analytics.EventType.LevelWon : Luna.Unity.Analytics.EventType.LevelFailed);
            if (won) Luna.Unity.Analytics.LogEvent("PlayerWon", 1);
            else Luna.Unity.Analytics.LogEvent("PlayerLost", 0);
            if (endCardShown) Luna.Unity.Analytics.LogEvent(Luna.Unity.Analytics.EventType.EndCardShown);
            Luna.Unity.LifeCycle.GameEnded();
#endif
        }
        public void Install()
        {
#if PLAYWORKS_SDK
            Luna.Unity.Analytics.LogEvent("CtaClicked", ended ? 1 : 0);
            Luna.Unity.Playable.InstallFullGame();
#else
            Debug.Log("[Playable Preview] CTA clicked. Install the Playworks SDK to enable store navigation.");
#endif
        }
        public void Dispose()
        {
#if PLAYWORKS_SDK
            Luna.Unity.LifeCycle.OnPause -= Pause;
            Luna.Unity.LifeCycle.OnResume -= Resume;
#endif
        }
    }
}
