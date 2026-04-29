using UnityEngine;

public abstract class PowerUpCommand
{
    protected GameObject player;

    public PowerUpCommand(GameObject playerObject)
    {
        player = playerObject;
    }

    public abstract void Execute();
}
