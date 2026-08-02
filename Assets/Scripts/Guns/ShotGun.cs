using UnityEngine;

public class Shotgun : Gun
{
    public override GunType GunType => GunType.Shotgun;

    public float shotgunSpreadAngle = 10f; // degrees between each pellet

    public override void Shoot(Vector2 playerPos, GameObject Bullet, Vector2 MousePos)
    {
        if (!CanShoot)
        {
            return;
        }

        Vector2 baseDirection = (MousePos - playerPos).normalized;
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;

        // Offsets: base, 2 above, 2 below
        float[] angleOffsets = { 0f, shotgunSpreadAngle, -shotgunSpreadAngle, shotgunSpreadAngle * 2f, -shotgunSpreadAngle * 2f };

        foreach (float offset in angleOffsets)
        {
            FireBulletAtAngle(baseAngle + offset, Bullet, playerPos);
        }

        CanShoot = false;
    }

    void FireBulletAtAngle(float angleDegrees, GameObject bullet, Vector2 playerPos)
    {
        // Convert angle back into a direction vector
        float radians = angleDegrees * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        GameObject spawnedBullet = SpawnBullet(bullet, playerPos);
        spawnedBullet.transform.rotation = Quaternion.AngleAxis(angleDegrees, Vector3.forward);

        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            Vector2 bulletVelocity = direction * BulletSpeed;
            bulletRb.linearVelocity = bulletVelocity;
        }

        int timer = 5;
        StartCoroutine(DestroyTimer(timer, spawnedBullet));
    }
}
