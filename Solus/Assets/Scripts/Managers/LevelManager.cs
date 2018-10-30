using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int batsToDefeat;
    public int batSpawnTime;
    public GameObject batPrefab;
    public int ghostsToDefeat;
    public int ghostSpawnTime;
    public GameObject ghostPrefab;
    public int slimesToDefeat;
    public int slimeSpawnTime;
    public GameObject slimePrefab;
    public Transform[] spawnPoints;
    public Transform[] paths;
    public GameObject gameOverMenu;

    private EnemySpawner batSpawner;
    private EnemySpawner ghostSpawner;
    private EnemySpawner slimeSpawner;
    private int currentEnemies;

    public delegate void EnemyHandler();
    public static event EnemyHandler OnEnemiesDestroyed;

    private void Awake()
    {
        batSpawner = gameObject.AddComponent<EnemySpawner>();

        batSpawner.enemyPrefab = batPrefab;
        batSpawner.spawnPoints = spawnPoints;
        batSpawner.paths = paths;
        batSpawner.time = batSpawnTime;
        batSpawner.maxEnemiesOnScreen = batsToDefeat;

        ghostSpawner = gameObject.AddComponent<EnemySpawner>();

        ghostSpawner.enemyPrefab = ghostPrefab;
        ghostSpawner.spawnPoints = spawnPoints;
        ghostSpawner.paths = paths;
        ghostSpawner.time = ghostSpawnTime;
        ghostSpawner.maxEnemiesOnScreen = ghostsToDefeat;

        slimeSpawner = gameObject.AddComponent<EnemySpawner>();

        slimeSpawner.enemyPrefab = slimePrefab;
        slimeSpawner.spawnPoints = spawnPoints;
        slimeSpawner.paths = paths;
        slimeSpawner.time = slimeSpawnTime;
        slimeSpawner.maxEnemiesOnScreen = slimesToDefeat;

        EnemySpawner.OnUnitSpawned += IncreaseEnemyCount;
        EnemyHealth.OnEnemyDestroyed += DecreaseEnemyCount;

        PlayerHealth.OnPlayerDead += ShowGameOverMenu;
        SunRelicController.OnRelicObtained += ShowGameOverMenu;
    }

    private void OnDestroy()
    {
        EnemySpawner.OnUnitSpawned -= IncreaseEnemyCount;
        EnemyHealth.OnEnemyDestroyed -= DecreaseEnemyCount;

        PlayerHealth.OnPlayerDead -= ShowGameOverMenu;
        SunRelicController.OnRelicObtained -= ShowGameOverMenu;
    }

    private void IncreaseEnemyCount()
    {
        currentEnemies++;
    }

    private void DecreaseEnemyCount()
    {
        currentEnemies--;

        if (currentEnemies == 0 && OnEnemiesDestroyed != null)
        {
            OnEnemiesDestroyed();
        }        
    }

    private void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
