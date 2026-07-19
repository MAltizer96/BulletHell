using UnityEngine;

public class DropManager : MonoBehaviour
{

    [SerializeField] private GameObject[] gunPickupPrefab;
    [SerializeField] private GameObject healthPickupPrefab;
    [SerializeField] private float gunDropChance = 0.1f;
    [SerializeField] private float healthDropChance = 0.2f;

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
        Enemy.OnEnemyDied += HandleEnemyDied;
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
            gunDropChance = 0.1f; // Reset the chance for the next drop
        }
        else
        {
            Debug.Log("No pickup dropped.");
            gunDropChance += 0.01f; // Increase the chance for the next drop
        }

        if (Random.value <= healthDropChance)
        {
            Debug.Log("Health pickup dropped.");
            
            //SpawnPickup(healthPickupPrefab, enemy.transform);

            healthDropChance = 0.2f; // Reset the chance for the next drop
        }
        else
        {
            Debug.Log("No health pickup dropped.");
            healthDropChance += 0.05f; // Increase the chance for the next drop
        }
    }
}
