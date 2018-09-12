using System;
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
    public GameObject entranceDoor;
    public Vector3 entranceDoorFinalPosition;
    public GameObject gameOverMenu;

    private EnemySpawner batSpawner;
    private EnemySpawner ghostSpawner;
    private EnemySpawner slimeSpawner;
    private int currentEnemies;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;

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

    void Update()
    {
        if (currentEnemies == 0 && Vector3.Distance(entranceDoor.transform.position, entranceDoorFinalPosition) > 1f)
        {
            entranceDoor.transform.position = Vector3.MoveTowards(entranceDoor.transform.position, entranceDoorFinalPosition, Time.deltaTime * 2);

            mainCamera.GetComponent<CameraShake>().ShakeCamera(1f);
        }
    }

    private void IncreaseEnemyCount()
    {
        currentEnemies++;
    }

    private void DecreaseEnemyCount()
    {
        currentEnemies--;
    }

    private void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
