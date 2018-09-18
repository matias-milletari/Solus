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