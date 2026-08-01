using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, iEnemy
{
    int currentHealth;
    [SerializeField]
    int maxHealth;

    EnemyType enemyType;
    private EnemyHealthDisplay healthSlider;

    //public static event Action<EnemyHealth> OnEnemyDied;
    //public static event Action<Vector2, float> OnKnockBack;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public EnemyType EnemyType { get => enemyType;}

    private void Awake()
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;

        healthSlider = GetComponent<EnemyHealthDisplay>();

        healthSlider.setMaxFill(MaxHealth);
        healthSlider.updateSlider(CurrentHealth);


        if (gameObject.name.Contains("Imp"))
        {
            enemyType = EnemyType.Imp;
        }
        else if (gameObject.name.Contains("Goblin"))
        {
            enemyType = EnemyType.Goblin;
        }
        else if (gameObject.name.Contains("Spider"))
        {
            enemyType = EnemyType.Spider;
        }
    }

    public void Dead()
    {
        EnemyEvents.EnemyDied(this);

    }

    public void KnockBack(iDamageable enemy, Vector2 direction, float force)
    {
        EnemyEvents.KnockBack(this, direction, force);
    }

    public void TakeDamage()
    {
        CurrentHealth -= 1;
        healthSlider.updateSlider(CurrentHealth);
        if (CurrentHealth < 1)
        {
            Dead();
        }
    }

}
