using Pathfinding;
using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, iDamageable
{
    int health;
    float maxHealth;
    float speed;
    GameObject target;
    
    Rigidbody2D rb;
    public AILerp aiLerp;
    public int Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Speed { get => speed; set => speed = value; }
    public GameObject Target { get => target; set => target = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aiLerp = GetComponent<AILerp>();
        //Target = GameObject.FindGameObjectWithTag("Player");
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
        if (Health <= 0)
        {
            //Dead();
        }
        //implement knockback here
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
}
