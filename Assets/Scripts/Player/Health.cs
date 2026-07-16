using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private GameObject[] healthIcons; // Array of health icon GameObjects

    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthIcons();
    }
    public void TakeDmg()
    {

        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHealthIcons();
        }
        if (currentHealth <= 0)
        {
            // Handle player death here
            Die();
            Debug.Log("Player is dead!");
        }
    }

    public void Heal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            UpdateHealthIcons();
        }
    }
    private void UpdateHealthIcons()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].SetActive(i < currentHealth);
        }
    }

    private void Die()
    {
        // Handle player death logic here
        Debug.Log("Player has died.");
    }

}
