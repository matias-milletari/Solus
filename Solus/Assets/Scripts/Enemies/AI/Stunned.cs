using UnityEngine;

public class Stunned : State<EnemyAI>
{
    private static Stunned instance;

    private Stunned()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static Stunned Instance
    {
        get
        {
            if (instance == null)
            {
                new Stunned();
            }

            return instance;
        }
    }

    public override void EnterState(EnemyAI owner)
    {
        owner.animator.SetBool("IsMoving", false);
        owner.animator.enabled = false;
        owner.navMeshAgent.isStopped = true;
    }

    public override void ExitState(EnemyAI owner)
    {
        owner.animator.SetBool("IsMoving", true);
        owner.animator.enabled = true;
        owner.navMeshAgent.isStopped = false;
    }

    public override void UpdateState(EnemyAI owner)
    {
    }
}