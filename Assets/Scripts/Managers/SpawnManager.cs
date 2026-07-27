using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    GameObject spiderPrefab;
    [SerializeField]
    GameObject goblinPrefab;
    [SerializeField]
    GameObject impPrefab;



    [Space(10)]
    [Header ("Max Enemies")]
    [SerializeField]
    int baseMaxEnemies;
    int maxEnemies;
    [SerializeField]
    int maxSpiders;
    [SerializeField]
    int maxGoblins;
    [SerializeField]
    int maxImps;

    [Space(10)]
    [Header ("Current Total For Each")]
    [SerializeField]
    int totalSpider;
    [SerializeField]
    int totalGoblin;
    [SerializeField]
    int totalImp;

    [Space(10)]
    [Header ("Timers")]
    [SerializeField]
    float maxSpawnTimer;
    [SerializeField]
    float spawnTimerReduction =0.5f;
    [SerializeField]
    float minSpawnTimer;

    float spawnTimer = 0f;


    int totalSpawnedEnemies = 0;

    Transform[] spawnPoints;
    List<GameObject> enemies = new List<GameObject>();

    bool spawnEnemies = true;

    public bool SpawnEnemies { get => spawnEnemies; set => spawnEnemies = value; }

    void OnEnable()
    {
        Enemy.OnEnemyDied += UpdateEnemies;
        GameManager.OnPlayerRestarts += PlayerRestarts;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDied -= UpdateEnemies;
        GameManager.OnPlayerRestarts -= PlayerRestarts;
    }

    private void Awake()
    {
        maxEnemies = baseMaxEnemies;
        RecalculateEnemyCaps();
        spawnPoints = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        //Enemy.OnEnemyDied += UpdateEnemies;
        //Debug.Log("Number of spawnPoints = " + spawnPoints.Length);
    }

    private void Update()
    {
        if (spawnTimer <= 0f && maxEnemies > enemies.Count && SpawnEnemies)
        {
            GameObject enemy = decideEnemy();
            //Debug.Log("Spawning enemy: " + enemy.name);
            Debug.Log("Enemy: " +  enemy.name);
            SpawnEnemy(enemy);
            spawnTimer = Random.Range(maxSpawnTimer * spawnTimerReduction, maxSpawnTimer);
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
        int spawnPointIndex = Random.Range(1, spawnPoints.Length);
        
        enemy = Instantiate(enemy, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        enemies.Add(enemy);
        GameObject enemiesParent = GameObject.Find("Enemies");
        enemy.transform.parent = enemiesParent.transform;
        totalSpawnedEnemies++;

        // checks if the totalSpawned is divisable by 5, if it is increases max enemies allowed by 1
        // make the game harder as time goes on
        if(totalSpawnedEnemies % 5 == 0)
        {
            maxEnemies++;

            RecalculateEnemyCaps();

            if (maxSpawnTimer <= 1f)
            {
                return;
            }
            maxSpawnTimer *= 0.9f;
        }
    }

    GameObject decideEnemy()
    {
        if (totalImp < maxImps)
        {
            totalImp++;
            return impPrefab;
        }
        if (totalGoblin < maxGoblins)
        {
            totalGoblin++;
            return goblinPrefab;
        }
        if (totalSpider < maxSpiders)
        {
            totalSpider++;
            return spiderPrefab;
        }


        return null;
    }

    void RecalculateEnemyCaps()
    {
        maxSpiders = Mathf.RoundToInt(maxEnemies * 0.6f);
        maxGoblins = Mathf.RoundToInt(maxEnemies * 0.3f);

        // Imps get whatever's left, so the numbers always add up to maxEnemies
        maxImps = maxEnemies - maxSpiders - maxGoblins;
    }
    void UpdateEnemies(Enemy enemy)
    {
        //Debug.Log("Enemy: "+  enemy.name);
        if (enemy.name.Contains("Imp"))
        {
            //Debug.Log("was Imp");
            totalImp -= 1;
        }
        if (enemy.name.Contains("Goblin"))
        {
            totalGoblin -= 1;
        }
        if(enemy.name.Contains("Spider"))
        {
            totalSpider -= 1;
        }

        enemies.Remove(enemy.gameObject);
        Destroy(enemy.gameObject);
    }

    void PlayerRestarts()
    {
        maxEnemies = baseMaxEnemies;
        RecalculateEnemyCaps();
    }
}
