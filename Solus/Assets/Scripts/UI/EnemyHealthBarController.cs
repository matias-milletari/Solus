using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarController : MonoBehaviour
{
    [SerializeField] private EnemyHealthBar enemyHealthBarPrefab;

    private Dictionary<EnemyHealth, EnemyHealthBar> healthBars = new Dictionary<EnemyHealth, EnemyHealthBar>();

    private void Awake()
    {
        EnemyHealth.OnHealthAdded += AddEnemyHealthBar;
        EnemyHealth.OnHealthRemoved += DestroyEnemyHealthBar;
    }

    private void AddEnemyHealthBar(EnemyHealth enemyHealth)
    {
        if (healthBars.ContainsKey(enemyHealth)) return;

        var enemyHealthbar = Instantiate(enemyHealthBarPrefab, transform);
        healthBars.Add(enemyHealth, enemyHealthbar);
        enemyHealthbar.SetHealth(enemyHealth);
    }

    private void DestroyEnemyHealthBar(EnemyHealth enemyHealth)
    {
        if (!healthBars.ContainsKey(enemyHealth)) return;

        Destroy(healthBars[enemyHealth].gameObject);
        healthBars.Remove(enemyHealth);
    }
}
