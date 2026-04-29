using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private float attackTimer = 0.5f;

    public void Enter(EnemyBehaviour enemy)
    {
        //Debug.Log("Enemy attacking");
    }

    public IEnemyState Tick(EnemyBehaviour enemy)
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            return new EnemyChaseState();
        }

        return null;
    }

    public void Exit(EnemyBehaviour enemy)
    {
    }
}
