using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage;
    public int timeBeforeDestroy;

    private AudioSource hitAudioSource;

    protected virtual void Awake()
    {
        Destroy(gameObject, timeBeforeDestroy);

        hitAudioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        hitAudioSource.Play();

        Destroy(gameObject);
    }
}
