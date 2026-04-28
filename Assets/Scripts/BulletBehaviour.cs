using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            Destroy(gameObject);
        }
    }
}
