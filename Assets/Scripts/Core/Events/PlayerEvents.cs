using System;
using UnityEngine;

public static class PlayerEvents
{
    //PlayerEvents.OnPlayerDied -= HandlePlayerDied;

    public static event Action<iDamageable> OnPlayerDied;
    public static event Action OnPlayerRestarts;
    public static event Action<Vector2, float> OnKnockBack;

    public static event Action<iGun> OnGunChanged;
    public static void PlayerDied(iDamageable damageable) => OnPlayerDied?.Invoke(damageable);
    public static void PlayerRestarts() => OnPlayerRestarts?.Invoke();
    public static void KnockBack(Vector2 direction, float force) => OnKnockBack?.Invoke(direction, force);
    public static void GunChanged(iGun gun) => OnGunChanged?.Invoke(gun);
}
