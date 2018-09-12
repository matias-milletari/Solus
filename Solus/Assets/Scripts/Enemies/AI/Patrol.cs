using UnityEngine;

public class Patrol : State<EnemyAI>
{
    private static Patrol instance;

    private Patrol()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static Patrol Instance
    {
        get
        {
            if (instance == null)
            {
                new Patrol();
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
        GoToNextWaypoint(owner);        
    }

    private void GoToNextWaypoint(EnemyAI owner)
    {
        if (owner.waypoints.Length == 0) return;

		if (!owner.navMeshAgent.pathPending && (owner.navMeshAgent.remainingDistance <= owner.navMeshAgent.stoppingDistance))
        {
            owner.currentWaypoint++;

            if (owner.currentWaypoint >= owner.waypoints.Length)
            {
                owner.currentWaypoint = 0;
            }
        }

		owner.navMeshAgent.SetDestination(owner.waypoints[owner.currentWaypoint].transform.position);
    }
}