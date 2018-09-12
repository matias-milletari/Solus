using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maximumHealth = 100;
    public bool isDead;

    public delegate void PlayerDeadHandler();
    public static event PlayerDeadHandler OnPlayerDead;

    public delegate void PlayerHurtHandler(float healthPercentage);
    public static event PlayerHurtHandler OnPlayerHurt;

    private Animator animator;
    private Camera mainCamera;

    void Awake()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        currentHealth = maximumHealth;
        isDead = false;
    }

    public void TakeDamage(float damage)
    {
        if (IsAlive())
        {
            animator.SetTrigger("TakeDamage");

            currentHealth -= damage;

            if (OnPlayerHurt != null) OnPlayerHurt(currentHealth / maximumHealth);

            mainCamera.GetComponent<CameraShake>().ShakeCamera(0.1f);
        }

        if (IsAlive()) return;

        isDead = true;

        gameObject.GetComponent<InputManager>().enabled = false;
        gameObject.GetComponent<PlayerController>().enabled = false;
        gameObject.GetComponent<PlayerCasting>().enabled = false;

        animator.SetTrigger("Death");
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public void ShowGameOverMenu()
    {
        if (OnPlayerDead != null) OnPlayerDead();
    }
}
