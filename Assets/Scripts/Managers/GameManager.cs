using System;
using TMPro;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    private float playedTimer = 0f;

    private TextMeshProUGUI timerText;

    private SpawnManager spawnManager;

    private PauseManager pauseManager;

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

        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }
    private void Update()
    {
        playedTimer += Time.deltaTime;
        timerText.text = "Time Played: " + Mathf.FloorToInt(playedTimer).ToString() + " seconds";

    }

    void HandlePlayerDied(PlayerHealth player)
    {
        Debug.Log("Player has died. Game Over!");

        pauseManager.PlayerDies(); // Pause the game
        spawnManager.SpawnEnemies = false; // Stop spawning enemies
    }
}
