using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private float minCameraDistance;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private float positionOffset;

    private Canvas healthBarCanvas;
    private EnemyHealth enemyHealth;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        healthBarCanvas = GetComponent<Canvas>();
    }

    public void SetHealth(EnemyHealth health)
    {
        this.enemyHealth = health;
        enemyHealth.OnHealthChanged += ModifyEnemyHealth;
    }

    private void LateUpdate()
    {
        var barPosition = mainCamera.WorldToScreenPoint(enemyHealth.transform.position + Vector3.up * positionOffset);

        if (Vector3.Distance(enemyHealth.transform.position, mainCamera.transform.position) > minCameraDistance || barPosition.z < 0)
        {
            healthBarCanvas.enabled = false;
        }
        else
        {
            healthBarCanvas.enabled = true;
            transform.position = barPosition;
        }
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