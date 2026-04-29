using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float damageCooldown = 1f;
    private int currentHealth;
    private float lastDamageTime;
    private bool isInvulnerable = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("isInvulnerable: " + isInvulnerable);

        if (Time.time >= lastDamageTime + damageCooldown && !isInvulnerable)
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

    public void ActivateShield()
    {
        Debug.Log("Shield Activated");
        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(5f);

        isInvulnerable = false;
    }
}
