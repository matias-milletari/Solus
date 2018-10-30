using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    //public float radius;
    //public LayerMask enemyLayer;

    //public delegate void EnemySightedHandler(EnemyHealth enemyHealth, bool visible);

    //public static event EnemySightedHandler OnEnemySighted;

    //private Camera mainCamera;

    //private void Awake()
    //{
    //    mainCamera = Camera.main;
    //}

    //private void LateUpdate()
    //{
    //    CheckEnemyInSight();
    //}

    //private void CheckEnemyInSight()
    //{
    //    var nearbyColliders = Physics.OverlapSphere(gameObject.transform.position, radius, enemyLayer);

    //    foreach (var nearbyCollider in nearbyColliders)
    //    {
    //        var enemyHealth = nearbyCollider.transform.GetComponent<EnemyHealth>();

    //        if (enemyHealth == null) continue;

    //        if (OnEnemySighted != null) OnEnemySighted(enemyHealth, Vector3.Dot((nearbyCollider.transform.position - mainCamera.transform.position).normalized, mainCamera.transform.forward) >= 0);
    //    }
    //}
}
