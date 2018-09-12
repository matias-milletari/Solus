using UnityEngine;

public class IcewallController : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float durationTime;
    [HideInInspector] public float damage;
    [HideInInspector] public float maximumCastingDistance;
    [HideInInspector] public LayerMask enemyLayer;
    [HideInInspector] public float damageTimeInterval;
    [HideInInspector] public Vector3 initialPosition;

    private float timer;
    private bool isSinking;

    public void Awake()
    {
        Invoke("DestroySpell", durationTime);
    }

    public void CastIcewallSpell(Vector3 previewPosition, Quaternion previewRotation, Vector3 rayOrigin, Vector3 rayDirection)
    {
        initialPosition = previewPosition;
        isSinking = false;

        RaycastHit rayCastHit;

        var layerMask = 1 << LayerMask.NameToLayer("Floor");

        if (Physics.Raycast(rayOrigin, rayDirection, out rayCastHit, maximumCastingDistance, layerMask))
        {
            Instantiate(gameObject, new Vector3(previewPosition.x, previewPosition.y - 2f, previewPosition.z), previewRotation);
        }
    }

    public void Update()
    {
        if (!isSinking)
        {
            if (Vector3.Distance(gameObject.transform.position, initialPosition) > 0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, 10f * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(-Vector3.up * 10f * Time.deltaTime);
        }
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
        isSinking = true;

        Destroy(gameObject, 2f);
    }
}
