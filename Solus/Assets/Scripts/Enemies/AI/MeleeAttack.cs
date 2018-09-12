using UnityEngine;

public class MeleeAttack : State<EnemyAI>
{
    private static MeleeAttack instance;
    private float timer;

    private MeleeAttack()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static MeleeAttack Instance
    {
        get
        {
            if (instance == null)
            {
                new MeleeAttack();
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

        if (timer > owner.timeBetweenMeleeAttacks)
        {
            timer = 0f;

            if (owner.playerHealth.IsAlive())
            {
                owner.animator.SetTrigger("Attack");

                owner.playerHealth.TakeDamage(owner.meleeAttackDamage);
            }
        }
    }
}