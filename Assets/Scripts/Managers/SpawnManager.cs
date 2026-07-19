using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemyPrefab;
    [SerializeField]
    int maxEnemies = 10;
    [SerializeField]
    float spawnTimer = 0f;

    Transform[] spawnPoints;
    List<GameObject> enemies = new List<GameObject>();
    void OnEnable()
    {
        Enemy.OnEnemyDied += UpdateEnemies;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDied -= UpdateEnemies;
    }

    private void Awake()
    {
        spawnPoints = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        Enemy.OnEnemyDied += UpdateEnemies;
        Debug.Log("Number of spawnPoints = " + spawnPoints.Length);
    }

    private void Update()
    {
        if (spawnTimer <= 0f && maxEnemies >= enemies.Count)
        {
            GameObject enemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
            Debug.Log("Spawning enemy: " + enemy.name);
            SpawnEnemy(enemy);
            spawnTimer = Random.Range(3f, 5f);
        }
        else
        {
            //Debug.Log(maxEnemies <= enemies.Count);
            Debug.Log("Total Enemies right now: " + enemies.Count);
            spawnTimer -= Time.deltaTime;
        }
    }
    void SpawnEnemy(GameObject enemy)
    {
        int spawnIndex = Random.Range(1, spawnPoints.Length);
        enemy = Instantiate(enemy, spawnPoints[spawnIndex].position, Quaternion.identity);
        enemies.Add(enemy);
    }

    void UpdateEnemies(Enemy enemy)
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
                break; // remove only the first null found
            }
        }
    }
}
