using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerAnimator))]

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float fireCooldown = 0.5f;
    private Vector2 originalFirePointPosition;

    private float lastFireTime;
    private PlayerAnimator playerAnimator;
    private InputAction shootAction;

    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        shootAction = InputSystem.actions.FindAction("Attack");

        originalFirePointPosition = firePoint.transform.localPosition;
    }

    void Update()
    {
        if (playerAnimator.IsFacingRight())
        {
            firePoint.transform.localPosition = originalFirePointPosition;
        }
        else
        {
            firePoint.transform.localPosition = new Vector2(
                -originalFirePointPosition.x,
                originalFirePointPosition.y
            );
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
        Quaternion bulletRotation;

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
    }
}
