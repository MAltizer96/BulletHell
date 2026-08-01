using UnityEngine;

public class DropManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] 
    private GameObject[] gunPickupPrefab;
    [SerializeField] 
    private GameObject healthPickupPrefab;

    [Space(10)]
    [Header("Percentages")]
    [SerializeField] 
    private float baseGunDropChance = 0.1f;
    [SerializeField] 
    private float baseHealthDropChance = 0.2f;
    [SerializeField]
    float gunDropChance;
    [SerializeField]
    float healthDropChance;
    [SerializeField]
    float gunDropChanceMultiplication;
    [SerializeField]
    float healthDropsChanceMultiplication;


    void OnEnable()
    {
        EnemyEvents.OnEnemyDied += HandleEnemyDied;
        PlayerEvents.OnPlayerRestarts += PlayerRestarts;
    }

    void OnDisable()
    {
        EnemyEvents.OnEnemyDied -= HandleEnemyDied;
        PlayerEvents.OnPlayerRestarts -= PlayerRestarts;
    }
    private void Awake()
    {
        gunDropChance = baseGunDropChance;
        healthDropChance = baseHealthDropChance;
    }
    void SpawnPickup(GameObject pickUpToSpawn, Transform location)
    {
        // spawn that bitch in!
        pickUpToSpawn = Instantiate(pickUpToSpawn, location.position, Quaternion.identity);
    }

    void HandleEnemyDied(iEnemy enemy)
    {
        var enemyBehaviour = enemy as MonoBehaviour;
        if (Random.value <= gunDropChance)
        {
            GameObject pickup = gunPickupPrefab[Random.Range(0, gunPickupPrefab.Length)];
            SpawnPickup(pickup, enemyBehaviour.transform);
            gunDropChance = baseGunDropChance; // Reset the chance for the next drop
            return;
        }
        else
        {
            //Debug.Log("No pickup dropped.");
            gunDropChance *= gunDropChanceMultiplication; // Increase the chance for the next drop
        }
        if (Random.value <= healthDropChance)
        {
            //Debug.Log("Health pickup dropped.");
            
            SpawnPickup(healthPickupPrefab, enemyBehaviour.transform);

            healthDropChance = baseHealthDropChance; // Reset the chance for the next drop
        }
        else
        {
            //Debug.Log("No health pickup dropped.");
            healthDropChance *= healthDropsChanceMultiplication; // Increase the chance for the next drop
        }
    }

    void PlayerRestarts()
    {
        healthDropChance = baseHealthDropChance;
        gunDropChance = baseGunDropChance;
    }
}