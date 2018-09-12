public abstract class State<EnemyAI>
{
    public abstract void EnterState(EnemyAI enemyOwner);
    public abstract void ExitState(EnemyAI enemyOwner);
    public abstract void UpdateState(EnemyAI enemyOwner);
}