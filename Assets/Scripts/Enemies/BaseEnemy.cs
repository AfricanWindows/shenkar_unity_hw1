using System;
using UnityEngine;

/// <summary>
/// Everything every enemy shares: it can be damaged, it dies, and it kills
/// Mario on touch. HOW an enemy behaves is decided by the child class.
/// </summary>
public abstract class BaseEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 1;
    [SerializeField] private string playerTag = "Player";

    /// <summary>Raised with the death position - used by the spawner (bonus).</summary>
    public static event Action<Vector3> OnEnemyDied;

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;

        health -= amount;

        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        if (OnEnemyDied != null)
            OnEnemyDied(transform.position);

        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col != null)
            TryKillPlayer(col.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null)
            TryKillPlayer(col.gameObject);
    }

    private void TryKillPlayer(GameObject other)
    {
        if (!other.CompareTag(playerTag))
            return;

        PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();
        if (playerDeath != null)
            playerDeath.Kill();
    }
}
