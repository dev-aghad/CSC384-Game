using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float damageCooldown = 1f;

    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject shieldUI;
    [SerializeField] private TMP_Text shieldTimerText;
    [SerializeField] private AudioSource shieldAudio;
    [SerializeField] private AudioClip shieldActivateClip;
    [SerializeField] private AudioClip shieldDeactivateClip;

    private SpriteRenderer playerSprite;

    private int currentHealth;
    private float lastDamageTime;
    private bool isInvulnerable = false;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        UpdateHearts();

        if (shieldUI != null)
        {
            shieldUI.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
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
        FindFirstObjectByType<FadeController>().FadeToScene("MainMenuScene");
    }

    public void ActivateShield()
    {
        if (shieldAudio != null && shieldActivateClip != null)
        {
            shieldAudio.PlayOneShot(shieldActivateClip);
        }

        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
        isInvulnerable = true;

        shieldUI.SetActive(true);

        float duration = 5f;
        float flashSpeed = 0.1f;

        while (duration > 0)
        {
            playerSprite.color = Color.blue;
            yield return new WaitForSeconds(flashSpeed);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(flashSpeed);
            duration -= flashSpeed * 2;

            shieldTimerText.text = Mathf.Ceil(duration).ToString();
            duration -= Time.deltaTime;
            yield return null;
        }

        if (shieldAudio != null && shieldDeactivateClip != null)
        {
            shieldAudio.PlayOneShot(shieldDeactivateClip);
        }

        shieldUI.SetActive(false);
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
