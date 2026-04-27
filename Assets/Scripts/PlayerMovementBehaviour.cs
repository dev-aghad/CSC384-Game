using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpCooldown = 5f;

    private InputAction moveAction;
    private Transform tr;
    private Vector2 axisValue;
    private Vector2 moveDistance;
    private float lastJumpTime;
    private Rigidbody2D rb;

    private void Awake()
    {
        tr = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Not in Awake since we want to make sure external peripherals and controllers are initialised before starting 
    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        if (moveAction != null) 
        {
            axisValue = moveAction.ReadValue<Vector2>();

            // Horizontal movement
            moveDistance = new Vector2(axisValue.x, 0) * speed * Time.deltaTime;
            tr.Translate(moveDistance);

            // Vertical movement
            if (axisValue.y > 0.1f && Time.time >= lastJumpTime + jumpCooldown)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                lastJumpTime = Time.time;
            }
        }
    }
    public Vector2 GetMovementInput()
    {
        return axisValue;
    }
}
