public class StateMachine<EnemyAI>
{
    public State<EnemyAI> currentState { get; private set; }
    public EnemyAI enemyOwner;

    public StateMachine(EnemyAI enemy)
    {
        enemyOwner = enemy;
        currentState = null;
    }

    public void ChangeState(State<EnemyAI> newState)
    {
        if (currentState != newState)
        {
            if (currentState != null)
            {
                currentState.ExitState(enemyOwner);
            }

            currentState = newState;
            currentState.EnterState(enemyOwner);
        }
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(enemyOwner);
        }
    }
}