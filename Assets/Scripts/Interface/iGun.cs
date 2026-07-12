using System.Collections;
using UnityEngine;

public interface iGun
{

    bool IsAutomatic { get; set; }
    bool CanShoot { get; set; }
    public float ShootCooldown { get; set; }
    public float BulletSpeed { get; set; }
    public float KnockBack { get; set; }
    public int BulletDamage { get; set; }
    public void Shoot(Vector2 playerPos, GameObject Bullet, Vector2 MousePos);
    public IEnumerator DestroyTimer(int time, GameObject bullet);
    void ResetToBaseGun();

}
