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

    bool spawnEnemies = true;

    public bool SpawnEnemies { get => spawnEnemies; set => spawnEnemies = value; }

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
        if (spawnTimer <= 0f && maxEnemies >= enemies.Count && SpawnEnemies)
        {
            GameObject enemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
            //Debug.Log("Spawning enemy: " + enemy.name);
            SpawnEnemy(enemy);
            spawnTimer = Random.Range(3f, 5f);
        }
        else
        {
            //Debug.Log(maxEnemies <= enemies.Count);
            //Debug.Log("Total Enemies right now: " + enemies.Count);
            spawnTimer -= Time.deltaTime;
        }
    }
    void SpawnEnemy(GameObject enemy)
    {
        int spawnIndex = Random.Range(1, spawnPoints.Length);
        enemy = Instantiate(enemy, spawnPoints[spawnIndex].position, Quaternion.identity);
        enemies.Add(enemy);
        GameObject enemiesParent = GameObject.Find("Enemies");
        enemy.transform.parent = enemiesParent.transform;
    }

    void UpdateEnemies(Enemy enemy)
    {
        enemies.Remove(enemy.gameObject);
    }
}
