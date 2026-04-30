using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerAnimator))]
[RequireComponent (typeof(SpriteRenderer))]

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject damageUI;
    [SerializeField] private TMP_Text damageTimerText;
    
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float fireCooldown = 0.5f;
    [SerializeField] private Animator shootEffectAnimator;
    [SerializeField] private GameObject shootEffectObject;

    [SerializeField] private AudioSource shootAudio;
    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private AudioClip damageActivateClip;
    [SerializeField] private AudioClip damageDeactivateClip;

    private Vector2 originalFirePointPosition;
    private Vector2 originalShootEffectPosition;

    private Quaternion bulletRotation;
    private float lastFireTime;
    private PlayerAnimator playerAnimator;
    private InputAction shootAction;
    private SpriteRenderer shootEffectSprite;
    private int damage = 1;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        shootEffectSprite = shootEffectObject.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");

        originalFirePointPosition = firePoint.transform.localPosition;
        originalShootEffectPosition = shootEffectObject.transform.localPosition;

        shootEffectObject.SetActive(false);

        if (damageUI != null)
        {
            damageUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerAnimator.IsFacingRight())
        {
            firePoint.transform.localPosition = originalFirePointPosition;

            shootEffectObject.transform.localPosition = originalShootEffectPosition;

            shootEffectSprite.flipX = false;
        }
        else
        {
            firePoint.transform.localPosition = new Vector2(
                -originalFirePointPosition.x,
                originalFirePointPosition.y
            );

            shootEffectObject.transform.localPosition = new Vector2(
                -originalShootEffectPosition.x,
                originalShootEffectPosition.y
            );

            shootEffectSprite.flipX = true;
        }

        if (shootAction != null && 
            shootAction.triggered && 
            Time.time >= lastFireTime + fireCooldown)
        {
            Shoot();
            lastFireTime = Time.time;
        }
    }

    private void Shoot()
    {
        if (playerAnimator.IsFacingRight())
        {
            bulletRotation = Quaternion.identity;
        }
        else
        {
            bulletRotation = Quaternion.Euler(0, 0, 180);
        }

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.transform.position,
            bulletRotation
        );

        shootAudio.Play();

        BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();

        bulletBehaviour.SetDamage(damage);

        if (damage > 1)
        {
            bulletBehaviour.SetColor(new Color(255f, 0f, 0f));
            shootEffectSprite.color = new Color(255f, 0f, 0f);
        } 
        else
        {
            bulletBehaviour.SetColor(Color.white);
            shootEffectSprite.color = Color.white;
        }

        shootEffectObject.SetActive(true);
        shootEffectAnimator.Play("ShootingEffect");

        Invoke(nameof(HideShootEffect), 0.2f);
    }

    private void HideShootEffect()
    {
        shootEffectObject.SetActive(false);
    }

    public void ActivateDoubleDamage()
    {
        if (damageAudio != null && damageActivateClip != null)
        {
            damageAudio.PlayOneShot(damageActivateClip);
        }

        StartCoroutine(DoubleDamageRoutine());
    }

    private IEnumerator DoubleDamageRoutine()
    {
        damage = 2;

        damageUI.SetActive(true);

        float duration = 5f;

        while (duration > 0)
        {
            shootAudio.pitch = 1.2f;
            damageTimerText.text = Mathf.Ceil(duration).ToString();
            duration -= Time.deltaTime;
            yield return null;
        }

        damageUI.SetActive(false);
        shootAudio.pitch = 1f;
        damage = 1;

        if (damageAudio != null && damageDeactivateClip != null)
        {
            damageAudio.PlayOneShot(damageDeactivateClip);
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
