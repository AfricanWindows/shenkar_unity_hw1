using System.Collections;
using UnityEngine;

/// <summary>
/// Shows the "LEVEL COMPLETE" screen when the door reports the level is finished,
/// and a short "you need the key" hint when Mario touches a locked door.
/// </summary>
public class LevelCompleteController : MonoBehaviour
{
    [SerializeField] private LevelExitDoor door;

    [Tooltip("Panel with the LEVEL COMPLETE text. Hidden while playing.")]
    [SerializeField] private GameObject levelCompletePanel;

    [Tooltip("Optional panel saying the door is locked.")]
    [SerializeField] private GameObject lockedHintPanel;

    [SerializeField] private float lockedHintDuration = 1.5f;

    private void OnEnable()
    {
        if (door != null)
        {
            door.OnLevelCompleted += OnLevelCompleted;
            door.OnLockedDoorTouched += OnLockedDoorTouched;
        }
    }

    private void OnDisable()
    {
        if (door != null)
        {
            door.OnLevelCompleted -= OnLevelCompleted;
            door.OnLockedDoorTouched -= OnLockedDoorTouched;
        }
    }

    private void Start()
    {
        if (door == null)
            Debug.LogError("LevelCompleteController: door is not assigned", this);

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);

        if (lockedHintPanel != null)
            lockedHintPanel.SetActive(false);
    }

    private void OnLevelCompleted()
    {
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    private void OnLockedDoorTouched()
    {
        if (lockedHintPanel == null)
            return;

        StopAllCoroutines();
        StartCoroutine(ShowLockedHint());
    }

    private IEnumerator ShowLockedHint()
    {
        lockedHintPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(lockedHintDuration);
        lockedHintPanel.SetActive(false);
    }
}
