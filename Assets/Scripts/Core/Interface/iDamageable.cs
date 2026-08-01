using UnityEngine;

public interface iDamageable
{
    int CurrentHealth { get; set; }
    int MaxHealth { get; set; }
    //float Speed { get; set; }
    //GameObject Target { get; set; }

    void TakeDamage();
    void KnockBack(iDamageable  damageable, Vector2 direction, float force);
    void Dead();
}
