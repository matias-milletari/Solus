using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public LayerMask playerLayerMask;
    public float heightOffset;
    public float maxHeightDifference = 1.0f;
    public float detectionRadius = 5f;
    [Range(0.0f, 360.0f)]
    public float detectionAngle = 180f;

    [HideInInspector] public Vector3 hitPosition;
    [HideInInspector] public Vector3 searchPosition;

    public bool PlayerDetected()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, detectionAngle, playerLayerMask);

        foreach (var colliderInRange in collidersInRange)
        {
            searchPosition = colliderInRange.transform.position;

            return true;
        }

        return false;
    }

    /// <summary>
    /// Devuelve la instancia del jugador si el mismo es visible.
    /// </summary>
    /// <param name="detector">El transform que está realizando la detección</param>
    /// <param name="useHeightDifference">Si se debe comparar la diferencia de altura con la diferencia de altura máima o no.</param>
    /// <returns>Retorna la instancia de PlayerController si está visible. De lo contrario, retorna null.</returns>
    public PlayerController PlayerSighted(Transform detector, bool useHeightDifference = true)
    {
        if (PlayerController.instance == null) return null;

        Vector3 eyePosition = detector.position + Vector3.up * heightOffset;
        Vector3 distanceToPlayer = PlayerController.instance.transform.position - eyePosition;
        Vector3 distanceToPlayerTop = PlayerController.instance.transform.position + Vector3.up * 2f - eyePosition;

        if (useHeightDifference && Mathf.Abs(distanceToPlayer.y + heightOffset) > maxHeightDifference)
        { 
            return null;
        }

        Vector3 distanceToPlayerFlat = distanceToPlayer;
        distanceToPlayerFlat.y = 0;

        if (distanceToPlayerFlat.sqrMagnitude <= detectionRadius * detectionRadius)
        {
            if (Vector3.Dot(distanceToPlayerFlat.normalized, detector.forward) > Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                bool isVisible = false;

                Debug.DrawRay(eyePosition, distanceToPlayer, Color.blue);
                Debug.DrawRay(eyePosition, distanceToPlayerTop, Color.blue);

                isVisible |= !Physics.Raycast(eyePosition, distanceToPlayer.normalized, detectionRadius, playerLayerMask);

                isVisible |= !Physics.Raycast(eyePosition, distanceToPlayerTop.normalized, detectionRadius, playerLayerMask);

                if (isVisible) return PlayerController.instance;
            }
        }

        return null;
    }

    public bool PlayerInAttackRange(Vector3 origin, Vector3 direction, float range)
    {
        RaycastHit rayCastHit;

        var hit = Physics.Raycast(origin, direction, out rayCastHit, range, playerLayerMask);

        hitPosition = rayCastHit.point;

        return hit;
    }

#if UNITY_EDITOR

    public void OnDrawGizmosSelected()
    {
        Color c = new Color(0, 0, 0.7f, 0.4f);

        UnityEditor.Handles.color = c;
        Vector3 rotatedForward = Quaternion.Euler(0, -detectionAngle * 0.5f, 0) * transform.forward;
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, detectionAngle, detectionRadius);

        Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        Gizmos.DrawWireSphere(transform.position + Vector3.up * heightOffset, 0.2f);
    }

#endif
}
