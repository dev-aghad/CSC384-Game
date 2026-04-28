using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private WaveManager waveManager;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        waveManager = FindFirstObjectByType<WaveManager>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        waveManager.EnemyDied();
        Destroy(gameObject);
    }
}
