using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float damageCooldown = 1f;
    private int currentHealth;
    private float lastDamageTime;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Time.time >= lastDamageTime + damageCooldown)
        {
            currentHealth -= damage;
            lastDamageTime = Time.time;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
