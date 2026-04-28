using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    public void Enter(EnemyBehaviour enemy)
    {
        Debug.Log("Enemy entered chase state");
    }

    public IEnemyState Tick(EnemyBehaviour enemy)
    {
        enemy.MoveTowardsPlayer();

        return null;
    }

    public void Exit(EnemyBehaviour enemy)
    {
        Debug.Log("Enemy exiting chase state");
    }
}
