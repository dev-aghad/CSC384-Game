using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovementBehaviour))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovementBehaviour movement;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovementBehaviour>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 input = movement.GetMovementInput();

        if (Mathf.Abs(input.x) > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (!movement.IsGrounded())
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        if (input.x < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
        else if (input.x > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
    }

    public bool IsFacingRight()
    {
        return !spriteRenderer.flipX;
    }
}
