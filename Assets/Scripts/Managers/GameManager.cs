using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private float playedTimer = 0f;

    private TextMeshProUGUI timerText;

    private void Awake()
    {
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        playedTimer += Time.deltaTime;
        timerText.text = "Time Played: " + Mathf.FloorToInt(playedTimer).ToString() + " seconds";

    }
}
