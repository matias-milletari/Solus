using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maximumHealth = 100;
    [SerializeField] private float sinkSpeed = 1.5f;
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private AudioSource hurtAudioSource;

    public delegate void EnemyDestroyedHandler();
    public static event EnemyDestroyedHandler OnEnemyDestroyed;

    public delegate void EnemyHealthAddedHandler(EnemyHealth enemyHealth);
    public static event EnemyHealthAddedHandler OnHealthAdded;

    public delegate void EnemyHealthRemovedHandler(EnemyHealth enemyHealth);
    public static event EnemyHealthRemovedHandler OnHealthRemoved;

    public delegate void EnemyHealthHandler(float health);
    public event EnemyHealthHandler OnHealthChanged;

    private float currentHealth;
    private Animator enemyAnimator;
    private AudioSource enemyAudioSource;
    private CapsuleCollider enemyCollider;
    private bool isDead;
    private bool isSinking;

    private void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyAudioSource = GetComponent<AudioSource>();
        enemyCollider = GetComponent<CapsuleCollider>();

        currentHealth = maximumHealth;

        OnHealthAdded(this);

        enemyAnimator.SetFloat("Health", maximumHealth);
    }

    private void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        currentHealth -= damage;

        if (OnHealthChanged != null) OnHealthChanged(currentHealth / maximumHealth);

        enemyAnimator.SetTrigger("IsDamaged");
        enemyAnimator.SetFloat("Health", currentHealth);

        hurtAudioSource.Play();

        if (!IsAlive())
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (OnEnemyDestroyed != null) OnEnemyDestroyed();

        enemyCollider.isTrigger = true;
        GetComponent<NavMeshAgent>().baseOffset = 0;

        enemyAudioSource.clip = deathAudioClip;
        enemyAudioSource.loop = false;
        enemyAudioSource.Play();

        if (OnHealthRemoved != null) OnHealthRemoved(this);

        Invoke("StartSinking", 2f);
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;

        GetComponent<Rigidbody>().isKinematic = true;

        isSinking = true;

        Destroy(gameObject, 2f);
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
