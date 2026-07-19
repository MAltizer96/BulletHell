using System;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private float playedTimer = 0f;

    private TextMeshProUGUI timerText;

    private SpawnManager spawnManager;

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
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    private void Update()
    {
        playedTimer += Time.deltaTime;
        timerText.text = "Time Played: " + Mathf.FloorToInt(playedTimer).ToString() + " seconds";

    }

    void HandlePlayerDied(PlayerHealth player)
    {
        Debug.Log("Player has died. Game Over!");
        // Implement game over logic here, such as showing a game over screen or restarting the level.

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false; // Disable player movement
            spawnManager.SpawnEnemies = false; // Stop spawning enemies
        }
    }
}
