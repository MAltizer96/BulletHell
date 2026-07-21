using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private GameObject[] healthIcons; // Array of health icon GameObjects
    public static event Action<PlayerHealth> OnPlayerDied;
    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        UpdateHealthIcons();
    }
    public void TakeDamage()
    {

        if (CurrentHealth > 0)
        {
            CurrentHealth--;
            UpdateHealthIcons();
        }
        if (CurrentHealth <= 0)
        {
            // Handle player death here
            Die();
            Debug.Log("Player is dead!");
        }
    }

    public void Heal()
    {
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth++;
            UpdateHealthIcons();
        }
    }
    private void UpdateHealthIcons()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].SetActive(i < CurrentHealth);
        }
    }

    private void Die()
    {
        // Handle player death logic here
        Debug.Log("Player has died.");
        OnPlayerDied?.Invoke(this);
    }

}
