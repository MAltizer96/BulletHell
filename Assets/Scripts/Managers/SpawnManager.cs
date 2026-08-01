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
    [Header("Timers")]
    [SerializeField]
    float baseMaxSpawnTimer;

    [SerializeField]
    float maxSpawnTimer;
    //delete Serialize
    [SerializeField]
    float spawnTimerReduction;
    [SerializeField]
    float minSpawnTimer;

    //delete Serialize
    [SerializeField]
    float spawnTimer = 0f;


    int totalSpawnedEnemies = 0;

    Transform[] spawnPoints;
    List<GameObject> enemies = new List<GameObject>();

    bool spawnEnemies = true;

    public bool SpawnEnemies { get => spawnEnemies; set => spawnEnemies = value; }

    void OnEnable()
    {
        EnemyEvents.OnEnemyDied += UpdateEnemies;

        PlayerEvents.OnPlayerDied += PlayerDied;
        PlayerEvents.OnPlayerRestarts += PlayerRestarts;
    }

    void OnDisable()
    {
        EnemyEvents.OnEnemyDied -= UpdateEnemies;

        PlayerEvents.OnPlayerDied -= PlayerDied;
        PlayerEvents.OnPlayerRestarts -= PlayerRestarts;
    }

    private void Awake()
    {
        maxEnemies = baseMaxEnemies;
        RecalculateEnemyCaps();
        spawnPoints = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        maxSpawnTimer = baseMaxSpawnTimer; 

    }

    private void Update()
    {
        if (spawnTimer <= 0f && maxEnemies > enemies.Count && SpawnEnemies)
        {
            GameObject enemy = decideEnemy();

            SpawnEnemy(enemy);
            float timesReuduction = 0.9f;
            spawnTimer = Random.Range(maxSpawnTimer * timesReuduction, maxSpawnTimer);
        }
        else
        {
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
            if (maxSpawnTimer * spawnTimerReduction < minSpawnTimer)
            {
                maxSpawnTimer = minSpawnTimer;
                return;
            }
            
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
    void UpdateEnemies(iEnemy enemy)
    {

        //Debug.Log("Enemy: "+  enemy.name);
        if (enemy.EnemyType == EnemyType.Imp)
        {
            //Debug.Log("was Imp");
            totalImp -= 1;
        }
        if (enemy.EnemyType == EnemyType.Goblin)
        {
            totalGoblin -= 1;
        }
        if(enemy.EnemyType == EnemyType.Spider)
        {
            totalSpider -= 1;
        }
        var enemyBehavior = enemy as MonoBehaviour;
        enemies.Remove(enemyBehavior.gameObject);
        Destroy(enemyBehavior.gameObject);

    }

    void PlayerDied(iDamageable damageable)
    {
        SpawnEnemies = false;
    }
    void PlayerRestarts()
    {
        maxEnemies = baseMaxEnemies;
        maxSpawnTimer = baseMaxSpawnTimer;
        spawnTimer = maxSpawnTimer;
        SpawnEnemies = true;
        RecalculateEnemyCaps();
    }
}
