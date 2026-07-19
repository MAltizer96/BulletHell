using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool isPaused;
    public void TogglePause()
    {
        OnApplicationPause(!isPaused);
    }
    

    public void PlayerDies()
    {
        OnApplicationPause(true);
        //game over screen logic
    }

    private void OnApplicationPause(bool pause)
    {
        isPaused = pause;
        if (pause)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            OnApplicationPause(true);
        }
        else
        {
            OnApplicationPause(false);
        }
    }
}
