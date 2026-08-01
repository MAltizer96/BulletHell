using UnityEngine;

public interface iEnemy : iDamageable
{
    EnemyType EnemyType { get; }
}
