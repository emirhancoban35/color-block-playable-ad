#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor.SceneManagement;
public static class BurstVisibilityChecks {
 public static void Run() {
  EditorSceneManager.OpenScene(Playable.Editor.PlayableSceneBuilder.ScenePath);
  var b=UnityEngine.Object.FindFirstObjectByType<Playable.PlayableBootstrap>();
  typeof(Playable.PlayableBootstrap).GetMethod("Start",BindingFlags.Instance|BindingFlags.NonPublic).Invoke(b,null);
  var burst=b.BoardView.GetComponentInChildren<Playable.View.ExitBurst>();
  typeof(Playable.View.ExitBurst).GetMethod("OnEnable",BindingFlags.Instance|BindingFlags.NonPublic).Invoke(burst,null);
  var renderers=burst.GetComponentsInChildren<MeshRenderer>(true);
  if(renderers.Length!=12 || Array.Exists(renderers,r=>r.enabled || !r.gameObject.activeSelf))throw new Exception("Prepared renderer pool invalid");
  b.BoardCamera.aspect=.6f;b.BoardCamera.orthographicSize=17.5f;
  b.BoardView.HideBlock(0);b.BoardView.Burst(0,new Vector3(.5f,-.75f,-.3f),Vector2.down,.24f);
  b.BoardView.TickBurst(.15f);
  foreach(var r in renderers)if(!r.enabled || r.transform.localScale.x<1.8f)throw new Exception("Burst not visible at .15 seconds");

  b.BoardView.TickBurst(.2f);if(!b.BoardView.BurstPlaying)throw new Exception("Burst too short");
  b.BoardView.TickBurst(.3f);if(b.BoardView.BurstPlaying || Array.Exists(renderers,r=>r.enabled))throw new Exception("Burst failed to finish");
  Debug.Log("BURST_VISIBILITY_PASSED: 12 active pooled objects, disabled renderers at rest, visible enlarged fragments at .15 seconds, survives .35 seconds, completes by .65 seconds.");
 }
}
#endif
