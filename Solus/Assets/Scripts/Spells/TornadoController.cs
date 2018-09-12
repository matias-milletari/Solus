using UnityEngine;

public class TornadoController : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float durationTime;
    [HideInInspector] public float damage;
    [HideInInspector] public float maximumCastingDistance;
    [HideInInspector] public LayerMask enemyLayer;
    [HideInInspector] public float damageTimeInterval;

    private float timer;

    private void Awake()
    {
        Invoke("DestroySpell", durationTime);
    }

    public void CastTornadoSpell(Vector3 position, Quaternion rotation, Vector3 rayOrigin, Vector3 rayDirection)
    {
        RaycastHit rayCastHit;

        var layerMask = 1 << LayerMask.NameToLayer("Floor");

        if (Physics.Raycast(rayOrigin, rayDirection, out rayCastHit, maximumCastingDistance, layerMask))
        {
            Instantiate(gameObject, position, rotation);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void OnTriggerStay(Collider coll)
    {
        timer -= Time.deltaTime;

        if (coll.gameObject.layer != LayerMask.NameToLayer("Enemies")) return;

        if (timer <= 0f)
        {
            coll.transform.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

            timer = damageTimeInterval;
        }
    }

    public void OnTriggerExit(Collider col)
    {
        timer = 0f;
    }

    public void DestroySpell()
    {
        gameObject.GetComponent<ParticleSystem>().Stop();

        Destroy(gameObject, 2f);
    }
}
