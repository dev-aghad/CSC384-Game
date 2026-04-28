using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerAnimator))]
[RequireComponent (typeof(SpriteRenderer))]

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float fireCooldown = 0.5f;
    [SerializeField] private Animator shootEffectAnimator;
    [SerializeField] private GameObject shootEffectObject;
    private Vector2 originalFirePointPosition;
    private Vector2 originalShootEffectPosition;

    private Quaternion bulletRotation;
    private float lastFireTime;
    private PlayerAnimator playerAnimator;
    private InputAction shootAction;
    private SpriteRenderer shootEffectSprite;

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

        Instantiate(
            bulletPrefab,
            firePoint.transform.position,
            bulletRotation
        );

        shootEffectObject.SetActive(true);
        shootEffectAnimator.Play("ShootingEffect");

        Invoke(nameof(HideShootEffect), 0.2f);
    }

    private void HideShootEffect()
    {
        shootEffectObject.SetActive(false);
    }
}
