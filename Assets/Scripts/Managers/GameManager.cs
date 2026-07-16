using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] 
    private GameObject player;

    private PlayerMovement playerMovement;
    private PlayerShoot PlayerShoot;


    private void Awake()
    {
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            PlayerShoot = player.GetComponent<PlayerShoot>();
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned in the GameManager.");
        }
    }
    public void StopPlayerInteraction()
    {
        
        // Implement logic to stop player interaction
        Debug.Log("Player interaction stopped.");
    }

    public void ResumePlayerInteraction()
    {
        // Implement logic to resume player interaction
        Debug.Log("Player interaction resumed.");
    }
}
