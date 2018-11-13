using Panda;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private bool isMelee;
    [SerializeField] private bool isRanged;
    [SerializeField] private float rotationSpeed;

    [Header("Patrol")]
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex;

    [Header("Follow")]
    [SerializeField] private float timeToStopFollowing;
    private float timerSinceLostTarget;
    private PlayerController currentTarget;

    [Header("RangedAttack")]
    [SerializeField] private GameObject enemyProjectile;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float rangedDistance;
    [SerializeField] private float timeBetweenRangedAttacks;
    [SerializeField] private int rangedAttackDamage;

    [Header("MeleeAttack")]
    [SerializeField] private float timeBetweenMeleeAttacks;
    [SerializeField] private float meleeDistance;
    [SerializeField] private int meleeAttackDamage;
    private float timerSinceLastAttack;

    private EnemySensor enemySensor;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemySensor = GetComponent<EnemySensor>();
        animator = GetComponent<Animator>();
    }

    public void SetWaypoints(Transform[] waypointArray)
    {
        waypoints = waypointArray;
    }

    [Task]
    private void SetNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        if (!navMeshAgent.pathPending && (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance))
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].transform.position);

        Task.current.Succeed();
    }

    private PlayerController GetTargetInSight()
    {
        return enemySensor.PlayerSighted(transform, false);
    }

    [Task]
    private bool HasTarget()
    {
        return currentTarget != null;
    }

    [Task]
    private bool IsTargetVisible()
    {
        return GetTargetInSight() != null;
    }

    [Task]
    private void ChaseTarget()
    {
        if (currentTarget != null)
        {
            navMeshAgent.SetDestination(currentTarget.transform.position);
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }

    [Task]
    private void SetTarget()
    {
        var target = GetTargetInSight();

        if (!HasTarget())
        {
            if (target != null)
            {
                currentTarget = target;
                timerSinceLostTarget = 0f;
            }
        }
        else
        {
            if (target == null)
            {
                timerSinceLostTarget += Time.deltaTime;

                if (timerSinceLostTarget >= timeToStopFollowing)
                {
                    ClearTarget();
                }
            }
            else
            {
                timerSinceLostTarget = 0f;
            }
        }

        Task.current.Succeed();
    }

    [Task]
    private void ClearTarget()
    {
        currentTarget = null;
        Task.current.Succeed();
    }

    [Task]
    public void Stop()
    {
        navMeshAgent.isStopped = true;
        Task.current.Succeed();
    }

    [Task]
    public void Move()
    {
        navMeshAgent.isStopped = false;
        Task.current.Succeed();
    }

    [Task]
    private bool IsMelee()
    {
        return isMelee;
    }

    [Task]
    private bool IsRanged()
    {
        return isRanged;
    }

    [Task]
    public void AimAtTarget()
    {
        var targetDelta = (currentTarget.transform.position - transform.position);
        var targetDir = targetDelta.normalized;

        if (targetDelta.magnitude > 0.2f)
        {


            var axis = Vector3.up * Mathf.Sign(Vector3.Cross(this.transform.forward, targetDir).y);

            var rot = Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, axis);
            transform.rotation = rot * this.transform.rotation;

            var newAxis = Vector3.up * Mathf.Sign(Vector3.Cross(this.transform.forward, targetDir).y);

            var dot = Vector3.Dot(axis, newAxis);

            if (dot < 0.01f)
            {// We overshooted the target
                var snapRot = Quaternion.FromToRotation(this.transform.forward, targetDir);
                this.transform.rotation = snapRot * this.transform.rotation;
                Task.current.Succeed();
            }

            var straighUp = Quaternion.FromToRotation(this.transform.up, Vector3.up);
            this.transform.rotation = straighUp * this.transform.rotation;
        }
        else
        {
            Task.current.Succeed();
        }
    }

    [Task]
    public void Fire()
    {
        if (timerSinceLastAttack > timeBetweenRangedAttacks)
        {
            timerSinceLastAttack = 0f;

            animator.SetTrigger("Attack");

            var projectile = Instantiate(enemyProjectile, shootingPoint.transform.position, Quaternion.identity);

            projectile.GetComponent<EnemyProjectile>().damage = rangedAttackDamage;

            projectile.transform.forward = Vector3.Normalize(new Vector3(currentTarget.transform.position.x, shootingPoint.transform.position.y, currentTarget.transform.position.z) - shootingPoint.transform.position);

            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileSpeed;
        }

        Task.current.Succeed();
    }

    [Task]
    public void AttackCooldown()
    {
        timerSinceLastAttack += Time.deltaTime;

        Task.current.Succeed();
    }

    [Task]
    public void Hit()
    {
        if (timerSinceLastAttack > timeBetweenMeleeAttacks)
        {
            timerSinceLastAttack = 0f;

            if (currentTarget.GetComponent<PlayerHealth>().IsAlive())
            {
                animator.SetTrigger("Attack");

                currentTarget.GetComponent<PlayerHealth>().TakeDamage(meleeAttackDamage);
            }
        }

        Task.current.Succeed();
    }

    [Task]
    public bool TargetInMeleeRange()
    {
        return isMelee && enemySensor.PlayerInAttackRange(transform.position + Vector3.up * enemySensor.heightOffset, transform.forward, meleeDistance);
    }


    [Task]
    public bool TargetInShootingRange()
    {
        return isRanged && enemySensor.PlayerInAttackRange(shootingPoint.position, transform.forward, rangedDistance);
    }
}
