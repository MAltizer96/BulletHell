using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{


    private float playedTimer = 0f;

    private TextMeshProUGUI timerText;
    [SerializeField]
    private GameObject gameOverPanel;

    private SpawnManager spawnManager;

    private PauseManager pauseManager;

    private GameObject playerGameObject;
    private Transform playerStartingPosition;


    void OnEnable()
    {
        PlayerHealth.OnPlayerDied += HandlePlayerDied;
    }

    void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= HandlePlayerDied;
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

    void PlayerRestarsGames()
    {
        playedTimer = 0f;
        spawnManager.SpawnEnemies = true; // Resume spawning enemies
        pauseManager.TogglePause(); // Resume the game

        gameOverPanel.SetActive(false); // Hide the game over panel

        playerGameObject.transform.position = playerStartingPosition.position; // Reset player position


    }
}
