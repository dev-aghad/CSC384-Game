using UnityEngine;

public class DoubleDamageCommand : PowerUpCommand
{
    public DoubleDamageCommand(GameObject playerObject) : base(playerObject)
    {
    }

    public override void Execute()
    {
        PlayerShooting playerShooting = player.GetComponent<PlayerShooting>();

        if (playerShooting != null)
        {
            playerShooting.ActivateDoubleDamage();
        }
    }
}
