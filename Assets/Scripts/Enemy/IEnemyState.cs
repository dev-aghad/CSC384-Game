public interface IEnemyState
{
    IEnemyState Tick(EnemyBehaviour enemy);
    void Enter(EnemyBehaviour enemy);
    void Exit(EnemyBehaviour enemy);
}
