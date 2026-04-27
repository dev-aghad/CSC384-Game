using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }
}
