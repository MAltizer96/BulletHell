using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{


    private float playedTimer = 0f;

    private TextMeshProUGUI timerText;

    private int totalEnemiesKilled;
    private TextMeshProUGUI totalEnemiesKilledText;
    [SerializeField]
    private GameObject gameOverPanel;

    private SpawnManager spawnManager;

    private PauseManager pauseManager;

    private GameObject playerGameObject;
    private Transform playerStartingPosition;

    public static event Action OnPlayerRestarts;


    void OnEnable()
    {
        PlayerHealth.OnPlayerDied += HandlePlayerDied;
        Enemy.OnEnemyDied += EnemyKilled;
    }

    void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= HandlePlayerDied;
        Enemy.OnEnemyDied -= EnemyKilled;
    }

    private void Awake()
    {
        timerText = GameObject.Find("Timer_Text_Number").GetComponent<TextMeshProUGUI>();       

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();

        playerStartingPosition = GameObject.Find("PlayerStartPosition").transform;
        playerGameObject = GameObject.Find("Player");

        // Set player position to starting position
        playerGameObject.transform.position = playerStartingPosition.position;

        totalEnemiesKilledText = GameObject.Find("Kills_Text_Number").GetComponent<TextMeshProUGUI>();
        totalEnemiesKilled = 0;
        totalEnemiesKilledText.text = totalEnemiesKilled.ToString();
    }
    private void Update()
    {
        playedTimer += Time.deltaTime;
        timerText.text = Mathf.FloorToInt(playedTimer).ToString() + " seconds";

    }

    void HandlePlayerDied(PlayerHealth player)
    {
        Debug.Log("Player has died. Game Over!");

        pauseManager.PlayerDies(); // Pause the game
        spawnManager.SpawnEnemies = false; // Stop spawning enemies

        gameOverPanel.SetActive(true); // Show the game over panel

        TextMeshProUGUI playerLastedForSeconds = gameOverPanel.transform.Find("Player_Lasted_For_Seconds_Text").GetComponent<TextMeshProUGUI>();
        playerLastedForSeconds.text = Mathf.FloorToInt(playedTimer).ToString() + " seconds";
    }

    public void PlayerRestarsGames()
    {
        // delete all current enemies
        GameObject enemies = GameObject.Find("Enemies");
        foreach (Transform enemy in enemies.transform)
        {
            Destroy(enemy.gameObject);         
        }

        playedTimer = 0f;
        spawnManager.SpawnEnemies = true; // Resume spawning enemies
        pauseManager.TogglePause(); // Resume the game

        gameOverPanel.SetActive(false); // Hide the game over panel

        playerGameObject.transform.position = playerStartingPosition.position; // Reset player position

        totalEnemiesKilled = 0;
        totalEnemiesKilledText.text = totalEnemiesKilled.ToString();

        OnPlayerRestarts?.Invoke();
    }

    private void EnemyKilled(Enemy enemy)
    {
        totalEnemiesKilled++;
        totalEnemiesKilledText.text = totalEnemiesKilled.ToString();
    }
}
