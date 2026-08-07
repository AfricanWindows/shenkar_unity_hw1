using System;
using UnityEngine;

/// <summary>
/// Exercise item 8: the door that ends the level - but only if Mario
/// already collected the key. It only DETECTS; showing the win screen
/// is LevelCompleteController's job.
/// </summary>
public class LevelExitDoor : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    public event Action OnLevelCompleted;
    public event Action OnLockedDoorTouched;

    private bool completed = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (completed || col == null || !col.gameObject.CompareTag(playerTag))
            return;

        PlayerKeys playerKeys = col.gameObject.GetComponent<PlayerKeys>();

        if (playerKeys == null || !playerKeys.HasKey)
        {
            Debug.Log("Door is locked - find the key first");

            if (OnLockedDoorTouched != null)
                OnLockedDoorTouched();

            return;
        }

        completed = true;

        if (OnLevelCompleted != null)
            OnLevelCompleted();
    }
}
