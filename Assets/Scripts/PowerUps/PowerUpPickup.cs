using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField] private bool isShieldPowerUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PowerUpCommand command;

            if (isShieldPowerUp)
            {
                command = new ShieldCommand(collision.gameObject);
            }
            else
            {
                command = new DoubleDamageCommand(collision.gameObject);
            }

            command.Execute();

            Destroy(gameObject);
        }
    }
}
