using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float speed;

    private int damage;

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

        // Reason I am doing it this way instead of the way above with layers is
        // because I can avoid unnecessary coupling since the bullet does not care
        // about enemy types
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            // Hopefully garbage collection shouldn't lag the game
            // (Remember to change to setActive(false) when testing)
            Destroy(gameObject);
        }
    }

    public void SetDamage(int bulletDamage)
    {
        damage = bulletDamage;
    }
}
