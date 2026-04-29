using UnityEngine;

public class ShieldCommand : PowerUpCommand
{
    public ShieldCommand(GameObject playerObject) : base(playerObject)
    {
    }

    public override void Execute()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.ActivateShield();
        }
    }
}
