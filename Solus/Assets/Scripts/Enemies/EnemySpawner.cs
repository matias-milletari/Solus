using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Transform[] paths;
    public float time;
    public int maxEnemiesOnScreen;

    public delegate void EnemyHandler();
    public static event EnemyHandler OnUnitSpawned;

    private int enemiesOnScreen;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, time);
    }

    public void SpawnEnemy()
    {
        if (enemiesOnScreen < maxEnemiesOnScreen)
        {
            var i = Random.Range(0, spawnPoints.Length);

            var waypoints = new Transform[paths[i].transform.childCount];

            for (var j = 0; j < paths[i].transform.childCount; j++)
            {
                waypoints[j] = paths[i].GetChild(j).GetComponent<Transform>();
            }

            enemyPrefab.GetComponent<EnemyAI>().waypoints = waypoints;

            Instantiate(enemyPrefab, spawnPoints[i].transform.position, transform.rotation);

            if (OnUnitSpawned != null) OnUnitSpawned();

            enemiesOnScreen++;
        }
    }
}
