using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("General")]
    public bool isMelee;
    public bool isRanged;

    [HideInInspector] public StateMachine<EnemyAI> stateMachine;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform player;
    [HideInInspector] public PlayerHealth playerHealth;
    [HideInInspector] public EnemySensor enemySensor;

    private EnemyHealth enemyHealth;
    private EnemyStatus enemyStatus;
    private PlayerController currentTarget;

    [Header("Patrol")]
    public int currentWaypoint;
    public Transform[] waypoints;

    [Header("Follow")]
    public float timeToStopFollowing;

    private float timerSinceLostTarget;

    [Header("RangedAttack")]
    public GameObject enemyProjectile;
    public Transform shootingPoint;
    public float projectileSpeed;
    public float rangedDistance;
    public float timeBetweenRangedAttacks;
    public int rangedAttackDamage;

    [Header("MeleeAttack")]
    public float meleeDistance;
    public float timeBetweenMeleeAttacks;
    public int meleeAttackDamage;

    private void Awake()
    {
        player = PlayerController.instance.gameObject.transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyStatus = GetComponent<EnemyStatus>();
        enemySensor = GetComponent<EnemySensor>();
    }

    private void Start()
    {
        stateMachine = new StateMachine<EnemyAI>(this);
        stateMachine.ChangeState(Patrol.Instance);
    }

    private void Update()
    {
        if (!enemyHealth.IsAlive()) return;

        if (enemyStatus.IsStunned())
        {
            stateMachine.ChangeState(Stunned.Instance);
        }
        else if (playerHealth.IsAlive())
        {
            FindTarget();

            if (currentTarget != null)
            {
                if (isMelee && enemySensor.PlayerInAttackRange(transform.position + Vector3.up * enemySensor.heightOffset, transform.forward, meleeDistance))
                {
                    stateMachine.ChangeState(MeleeAttack.Instance);
                }
                else if (isRanged && enemySensor.PlayerInAttackRange(shootingPoint.position, transform.forward, rangedDistance))
                {
                    stateMachine.ChangeState(RangedAttack.Instance);
                }
                else
                {
                    stateMachine.ChangeState(Follow.Instance);
                }
            }
            else
            {
                stateMachine.ChangeState(Patrol.Instance);
            }
        }

        stateMachine.Update();
    }

    public void FindTarget()
    {
        var target = enemySensor.PlayerSighted(transform, false);

        if (currentTarget == null)
        {
            if (target != null)
            {
                currentTarget = target;
            }
        }
        else
        {
            if (target == null)
            {
                timerSinceLostTarget += Time.deltaTime;

                if (timerSinceLostTarget >= timeToStopFollowing)
                {
                    currentTarget = null;
                }
            }
            else
            {
                timerSinceLostTarget = 0f;
            }
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(enemyProjectile, shootingPoint.transform.position, Quaternion.identity);

        projectile.GetComponent<EnemyProjectile>().damage = rangedAttackDamage;

        projectile.transform.LookAt(enemySensor.hitPosition);

        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
    }
}
