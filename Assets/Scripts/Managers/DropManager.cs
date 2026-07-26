using UnityEngine;

public class DropManager : MonoBehaviour
{

    [SerializeField] 
    private GameObject[] gunPickupPrefab;
    [SerializeField] 
    private GameObject healthPickupPrefab;
    [SerializeField] 
    private float baseGunDropChance = 0.1f;
    [SerializeField] 
    private float baseHealthDropChance = 0.2f;

    [SerializeField]
    float gunDropChance;
    [SerializeField]
    float healthDropChance;

    void OnEnable()
    {
        Enemy.OnEnemyDied += HandleEnemyDied;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDied -= HandleEnemyDied;
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

    void HandleEnemyDied(Enemy enemy)
    {
        if (Random.value <= gunDropChance)
        {
            GameObject pickup = gunPickupPrefab[Random.Range(0, gunPickupPrefab.Length)];
            SpawnPickup(pickup, enemy.transform);
            gunDropChance = baseGunDropChance; // Reset the chance for the next drop
            return;
        }
        else
        {
            Debug.Log("No pickup dropped.");
            gunDropChance *= 1.1f; // Increase the chance for the next drop
        }
        if (Random.value <= healthDropChance)
        {
            Debug.Log("Health pickup dropped.");
            
            SpawnPickup(healthPickupPrefab, enemy.transform);

            healthDropChance = baseHealthDropChance; // Reset the chance for the next drop
        }
        else
        {
            Debug.Log("No health pickup dropped.");
            healthDropChance *= 1.2f; // Increase the chance for the next drop
        }
    }
}
