using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private InputAction moveAction;
    private Vector2 axisValue;
    private Rigidbody2D rb;
    private Vector2 newVelocity;
    private float xVelocity;
    private float yVelocity;
    private bool isGrounded;
    private bool groundedThisFrame;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        newVelocity = rb.linearVelocity;

        if (moveAction != null) 
        {
            axisValue = moveAction.ReadValue<Vector2>();
            newVelocity = rb.linearVelocity;

            // Horizontal movement
            xVelocity = axisValue.x * speed;
            newVelocity.x = xVelocity;
  

            // Vertical movement
            if (axisValue.y > 0.1f && isGrounded)
            {
                yVelocity = jumpForce;
                newVelocity.y = yVelocity;
            }

            rb.linearVelocity = newVelocity;
        }
    }
    public Vector2 GetMovementInput()
    {
        return axisValue;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
