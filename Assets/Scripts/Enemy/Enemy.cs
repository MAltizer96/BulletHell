using Pathfinding;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, iDamageable
{
    float health;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float speed;
    GameObject target;

    private EnemyHealthDisplay healthSlider;

    Rigidbody2D rb;
    public AILerp aiLerp;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Speed { get => speed; set => speed = value; }
    public GameObject Target { get => target; set => target = value; }

    private void Awake()
    {
        MaxHealth = maxHealth;
        Health = MaxHealth;        
        Speed = speed;
        

    }

    private void Start()
    {
        healthSlider = GetComponent<EnemyHealthDisplay>();
        rb = GetComponent<Rigidbody2D>();
        aiLerp = GetComponent<AILerp>();
        //Target = GameObject.FindGameObjectWithTag("Player");

        healthSlider.setMaxFill(MaxHealth);
        healthSlider.updateSlider(Health);
    }

    public void KnockBack(Vector2 direction, float force)
    {
        StartCoroutine(KnockBackRoutine(direction, force));
        //rb.linearVelocity = direction * force;
    }
    private IEnumerator KnockBackRoutine(Vector2 direction, float force)
    {
        // Disable AILerp so it stops fighting the knockback
        aiLerp.enabled = false;

        rb.linearVelocity = direction * force;

        // Wait for knockback to finish
        yield return new WaitForSeconds(0.2f);

        // Re-enable AILerp
        aiLerp.enabled = true;
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        healthSlider.updateSlider(Health);
        if (Health < 1)
        {
            Dead();
        }
        //implement knockback here
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
}
