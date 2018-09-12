using UnityEngine;

public class SparkController : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float durationTime;
    [HideInInspector] public float damage;
    [HideInInspector] public GameObject explosionPrefab;
    [HideInInspector] public LayerMask enemyLayer;
    [HideInInspector] public GameObject stunParticle;
    [HideInInspector] public float stunDuration;
    [HideInInspector] public AudioClip sparkCastAudioClip;

    private void Awake()
    {
        Invoke("DestroySpell", durationTime);
    }

    public void CastSparkSpell(Vector3 startPosition, Vector3 rayOrigin, Vector3 rayDirection)
    {
        RaycastHit rayCastHit;

        var layerMask = 1 << LayerMask.NameToLayer("Player");

        layerMask = ~layerMask;

        if (Physics.Raycast(rayOrigin, rayDirection, out rayCastHit, Mathf.Infinity, layerMask))
        {
            Create(startPosition, rayCastHit.point);
        }
    }

    public void Create(Vector3 startPosition, Vector3 endPosition)
    {
        var sparkClone = Instantiate(gameObject, startPosition, Quaternion.identity);

        sparkClone.transform.LookAt(endPosition);

        sparkClone.GetComponent<Rigidbody>().velocity = sparkClone.transform.forward * speed;

        sparkClone.GetComponent<AudioSource>().clip = sparkCastAudioClip;
        sparkClone.GetComponent<AudioSource>().Play();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (enemyLayer == (enemyLayer | (1 << coll.gameObject.layer)))
        {
            coll.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

            var stunParticleClone = Instantiate(stunParticle, coll.gameObject.transform.position, coll.gameObject.transform.rotation, coll.gameObject.transform);

            Destroy(stunParticleClone, stunDuration);

            coll.gameObject.GetComponent<EnemyStatus>().SetStun(true, stunDuration, stunParticleClone);
        }

        AddExplosion();

        Destroy(gameObject);
    }

    private void AddExplosion()
    {
        Instantiate(explosionPrefab, gameObject.transform);
    }

    private void DestroySpell()
    {
        AddExplosion();

        Destroy(gameObject);
    }
}