using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField]
    float timerUntilDestroy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Player
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                if (playerHealth.CurrentHealth >= playerHealth.MaxHealth)
                {
                    return;
                }
                playerHealth.Heal();
                Destroy(gameObject);
            }
        }
    }
    private void Start()
    {
        StartCoroutine(StartTimerUntilPickedUp(timerUntilDestroy));
    }
}
