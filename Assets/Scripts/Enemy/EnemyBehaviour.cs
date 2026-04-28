using Unity.Burst;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private PlayerHealth playerHealth;
    private GameObject player;
    private IEnemyState currentState = new EnemyChaseState();

    private void Start()
    {
        // Trying to find the game object via layer would be a lot more work so
        // I added a tag for the player instead to search by that
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        /*
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.transform.position,
                moveSpeed * Time.deltaTime
            );
        }
        */
        UpdateState();
    }

    private void UpdateState()
    {
        IEnemyState newState = currentState.Tick(this);

        if (newState != null)
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }

    public void MoveTowardsPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.transform.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(50);
        }
    }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(50);

                currentState.Exit(this);
                currentState = new EnemyAttackState();
                currentState.Enter(this);
            }
        }
    }

    public GameObject getPlayer
    {
        get { return player; }
    }

    public float getMoveSpeed
    {
        get { return moveSpeed; }
    }
}
