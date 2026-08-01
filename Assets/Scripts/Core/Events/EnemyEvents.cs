using System;
using UnityEngine;

public static class EnemyEvents 
{
    public static event Action<iEnemy> OnEnemyDied;
    public static event Action<iEnemy, Vector2, float> OnKnockBack;

    public static void EnemyDied(iEnemy iEnemy) => OnEnemyDied?.Invoke(iEnemy);
    public static void KnockBack(iEnemy enemy, Vector2 direction, float force) => OnKnockBack?.Invoke(enemy,direction, force);

}
