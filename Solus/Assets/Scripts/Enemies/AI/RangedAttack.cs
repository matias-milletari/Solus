using UnityEngine;

public class RangedAttack : State<EnemyAI>
{
    private static RangedAttack instance;
    private float timer;

    private RangedAttack()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static RangedAttack Instance
    {
        get
        {
            if (instance == null)
            {
                new RangedAttack();
            }

            return instance;
        }
    }

    public override void EnterState(EnemyAI owner)
    {
    }

    public override void ExitState(EnemyAI owner)
    {
    }

    public override void UpdateState(EnemyAI owner)
    {
        timer += Time.deltaTime;

        if (timer > owner.timeBetweenRangedAttacks)
        {
            timer = 0f;

            if (owner.playerHealth.IsAlive())
            {
                owner.animator.SetTrigger("Attack");

                owner.Shoot();
            }
        }
    }
}