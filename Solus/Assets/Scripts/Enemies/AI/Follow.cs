using UnityEngine;

public class Follow : State<EnemyAI>
{
    private static Follow instance;

    private Follow()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static Follow Instance
    {
        get
        {
            if (instance == null)
            {
                new Follow();
            }

            return instance;
        }
    }

    public override void EnterState(EnemyAI owner)
    {
        owner.animator.SetBool("IsMoving", true);
        owner.navMeshAgent.isStopped = false;
    }

    public override void ExitState(EnemyAI owner)
    {
        owner.animator.SetBool("IsMoving", false);
        owner.navMeshAgent.isStopped = true;
    }

    public override void UpdateState(EnemyAI owner)
    {
        owner.navMeshAgent.SetDestination(owner.player.position);
    }
}