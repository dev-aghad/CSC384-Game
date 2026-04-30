using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private WaveManager waveManager;

    private int currentHealth;

    private SpriteRenderer spriteRenderer;
    private float flashDuration = 0.2f;

    [SerializeField] private AudioSource hitAudio;
    [SerializeField] private AudioClip hitClip;

    private Color originalColour;

    private void Start()
    {
        currentHealth = maxHealth;
        waveManager = FindFirstObjectByType<WaveManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColour = spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (hitAudio != null && hitClip != null)
        {
            hitAudio.PlayOneShot(hitClip);
        }

        StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColour;
    }

    private void Die()
    {
        waveManager.EnemyDied();
        Destroy(gameObject);
    }
}
