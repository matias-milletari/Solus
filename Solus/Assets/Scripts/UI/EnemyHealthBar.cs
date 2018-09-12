using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private float minCameraDistance;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private float positionOffset;

    private EnemyHealth enemyHealth;
    private Camera mainCamera;
    private float detectionAngle = 180f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void SetHealth(EnemyHealth health)
    {
        this.enemyHealth = health;
        enemyHealth.OnHealthChanged += ModifyEnemyHealth;
    }

    private void LateUpdate()
    {
        //transform.forward = mainCamera.transform.forward;

        //var distanceToPlayer = gameObject.transform.position - mainCamera.transform.position;
        //distanceToPlayer.y = 0f;

        //if (distanceToPlayer.sqrMagnitude <= minCameraDistance * minCameraDistance)
        //{
        //    if (Vector3.Dot(distanceToPlayer.normalized, mainCamera.transform.forward) > Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
        //    {
        //        gameObject.SetActive(true);
        //    }
        //}
        //else
        //{
        //    gameObject.SetActive(false);
        //}

        transform.position = mainCamera.WorldToScreenPoint(enemyHealth.transform.position + Vector3.up * positionOffset);
    }

    private void ModifyEnemyHealth(float health)
    {
        StartCoroutine(UpdateEnemyHealthBar(health));
    }

    private IEnumerator UpdateEnemyHealthBar(float healthPercentage)
    {
        healthBarImage.fillAmount = healthPercentage;

        yield return null;
    }

    private void OnDestroy()
    {
        enemyHealth.OnHealthChanged -= ModifyEnemyHealth;
    }
}