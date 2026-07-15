using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Luna.Unity;

public class PlayableSmokeController : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform block;
    [SerializeField] private Transform exitPoint;
    [SerializeField] private GameObject endCard;
    [SerializeField] private Button ctaButton;

    [Header("Movement")]
    [SerializeField] private float moveDuration = 0.35f;

    private bool completed;
    private bool moving;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (endCard != null)
            endCard.SetActive(false);

        if (ctaButton != null)
            ctaButton.onClick.AddListener(OnCtaClicked);
    }

    private void Start()
    {
        Debug.Log("[Smoke] Playable smoke scene started.");

        Analytics.LogEvent(Analytics.EventType.TutorialStarted);
    }

    private void Update()
    {
        if (completed || moving)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            TrySelectBlock();
        }
    }

    private void TrySelectBlock()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == block)
            {
                Debug.Log("[Smoke] Block selected.");
                StartCoroutine(CompleteMiniLevel());
            }
        }
    }

    private IEnumerator CompleteMiniLevel()
    {
        moving = true;

        Vector3 start = block.position;
        Vector3 target = exitPoint.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            block.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        block.position = target;

        completed = true;
        moving = false;

        Debug.Log("[Smoke] Level won.");

        Analytics.LogEvent(Analytics.EventType.LevelWon);

        if (endCard != null)
            endCard.SetActive(true);

        Analytics.LogEvent(Analytics.EventType.EndCardShown);

        LifeCycle.GameEnded();
    }

    public void OnCtaClicked()
    {
        Debug.Log("[Smoke] CTA clicked.");

        Analytics.LogEvent("CTA_Clicked" ,1);

        Playable.InstallFullGame();
    }

    private void OnDestroy()
    {
        if (ctaButton != null)
            ctaButton.onClick.RemoveListener(OnCtaClicked);
    }
}