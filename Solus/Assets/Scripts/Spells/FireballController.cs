using UnityEngine;

public class FireballController : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float durationTime;
    [HideInInspector] public float damage;
    [HideInInspector] public GameObject explosionPrefab;
    [HideInInspector] public float radius;
    [HideInInspector] public LayerMask enemyLayer;
    [HideInInspector] public AudioClip fireballCastAudioClip;

    private void Awake()
    {
        Invoke("DestroySpell", durationTime);
    }

    public void CastFireballSpell(Vector3 startPosition, Vector3 rayOrigin, Vector3 rayDirection)
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
        var fireballClone = Instantiate(gameObject, startPosition, Quaternion.identity);

        fireballClone.transform.LookAt(endPosition);

        fireballClone.GetComponent<Rigidbody>().velocity = fireballClone.transform.forward * speed;

        fireballClone.GetComponent<AudioSource>().clip = fireballCastAudioClip;
        fireballClone.GetComponent<AudioSource>().time = 0.9f;
        fireballClone.GetComponent<AudioSource>().Play();
    }

    void OnTriggerEnter(Collider collider)
    {
        Explode();
    }

    private void Explode()
    {
        var nearbyColliders = Physics.OverlapSphere(gameObject.transform.position, radius, enemyLayer);

        foreach (var nearbyCollider in nearbyColliders)
        {
            var distance = Vector3.Distance(gameObject.transform.position, nearbyCollider.transform.position);

            ApplyDamage(nearbyCollider, distance);
        }

        AddExplosion();

        Destroy(gameObject);
    }

    public void ApplyDamage(Collider nearbyCollider, float distance)
    {
        var explosionDamage = damage / distance;

        nearbyCollider.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(explosionDamage);
    }

    public void AddExplosion()
    {
        Instantiate(explosionPrefab, gameObject.transform);
    }

    public void DestroySpell()
    {
        Explode();
    }
}