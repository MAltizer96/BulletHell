using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> healthIcons; // Array of health icon GameObjects
    public static event Action<PlayerHealth> OnPlayerDied;
    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
        SetHealtIcons();
        UpdateHealthIcons();
        
    }
    private void Start()
    {
        

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

        for (int i = 0; i < healthIcons.Count; i++)
        {
            healthIcons[i].SetActive(i < CurrentHealth);
        }
    }

    private void SetHealtIcons()
    {
        
        GameObject HealthPanel = GameObject.Find("Health_Panel");
        //healthIcons.Clear();
        foreach (var icon in healthIcons)
        {
            if (!healthIcons.Contains(icon))
            { 
                healthIcons.Add(icon);
            }
          
        }
    }

    private void Die()
    {
        // Handle player death logic here
        Debug.Log("Player has died.");
        OnPlayerDied?.Invoke(this);
        
    }

}
