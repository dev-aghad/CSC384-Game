using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] private GameObject[] hearts;

    private int currentHealth;
    private float lastDamageTime;
    private bool isInvulnerable = false;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Current Health: " + currentHealth);

        if (Time.time >= lastDamageTime + damageCooldown && !isInvulnerable)
        {
            currentHealth -= damage;
            lastDamageTime = Time.time;
            UpdateHearts();

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

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
