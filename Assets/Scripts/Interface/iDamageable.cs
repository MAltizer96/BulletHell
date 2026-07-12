using UnityEngine;

public interface iDamageable
{
    int Health { get; set; }
    float MaxHealth { get; set; }
    float Speed { get; set; }
    GameObject Target { get; set; }

    void TakeDamage(int damage);
    void KnockBack(Vector2 direction, float force);
    void Dead();
}
