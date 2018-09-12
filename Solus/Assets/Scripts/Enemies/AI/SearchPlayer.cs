using UnityEngine;

public class SearchPlayer : State<EnemyAI>
{
    private static SearchPlayer instance;

    private SearchPlayer()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static SearchPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                new SearchPlayer();
            }

            return instance;
        }
    }

    public override void EnterState(EnemyAI owner)
    {
        owner.animator.SetBool("IsMoving", false);
        owner.navMeshAgent.isStopped = true;
    }

    public override void ExitState(EnemyAI owner)
    {
        owner.timeToStopFollowing = 0f;
    }

    public override void UpdateState(EnemyAI owner)
    {
        //owner.searchTimer += Time.deltaTime;

        var direction = owner.enemySensor.searchPosition - owner.transform.position;
        var rotation = Quaternion.LookRotation(direction);

        owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, rotation, 2 * Time.deltaTime);
    }
}