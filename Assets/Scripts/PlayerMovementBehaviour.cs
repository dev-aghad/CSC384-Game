using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private InputAction moveAction;
    private Transform tr;
    private Vector2 axisValue;
    private Vector2 moveDistance;

    private void Awake()
    {
        tr = transform;
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
            moveDistance = axisValue * speed * Time.deltaTime;
            tr.Translate(moveDistance);
        }
    }
    public Vector2 GetMovementInput()
    {
        return axisValue;
    }
}
