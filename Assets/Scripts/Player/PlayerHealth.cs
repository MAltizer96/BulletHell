using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, iDamageable
{

    Rigidbody2D rb;


    [SerializeField]
    private List<GameObject> healthIcons; // Array of health icon GameObjects
    //public static event Action<Vector2, float> OnKnockBack;
    //public static event Action<Vector2> OnKnockBackOffCoolDown;
    //public static event Action<PlayerHealth> OnPlayerDied;
    [SerializeField]
    private int currentHealth;
    //[SerializeField]
    private int maxHealth;
    [SerializeField]
    private bool isDead = false;


    //private IGameManager gameManager;
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    private void OnEnable()
    {//change these!!!
        PlayerEvents.OnPlayerRestarts += PlayerReset;
    }

    private void OnDisable()
    {
        PlayerEvents.OnPlayerRestarts -= PlayerReset;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        MaxHealth = healthIcons.Count;

        //Debug.Log("MaxHealth: " + MaxHealth);

        CurrentHealth = MaxHealth;
        SetHealtIcons();
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
            Dead();
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

    public void KnockBack(iDamageable damageable, Vector2 direction, float force)
    {
        PlayerEvents.KnockBack(direction, force);
    }
    public void Dead()
    {
        // Handle player death logic here
        Debug.Log("Player has died.");
        IsDead = true;
        PlayerEvents.PlayerDied(this);
        
    }

    public void PlayerReset()
    {
        IsDead = false;
        CurrentHealth = MaxHealth;
        UpdateHealthIcons();
    }
    
}
