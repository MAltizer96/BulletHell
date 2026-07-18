using UnityEngine;

public interface iDamageable
{
    float Health { get; set; }
    float MaxHealth { get; set; }
    float Speed { get; set; }
    GameObject Target { get; set; }

    void TakeDamage(int damage);
    void KnockBack(Vector2 direction, float force);
    void Dead();
}
