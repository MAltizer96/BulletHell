using System.Collections;
using UnityEngine;

public class BaseGun : Gun
{
    private void Awake()
    {
        IsAutomatic = false; // Base gun is semi-automatic
    }
    public override void Shoot(Vector2 playerPos, GameObject Bullet, Vector2 MousePos)
    {
        if (!CanShoot) {
            //Debug.Log("Gun is on cooldown. Please wait.");
            return;
        }
        // Calculate direction from firePoint to mouse
        Vector2 direction = (MousePos - playerPos).normalized;

        // Spawn the bullet at the fire point
        GameObject spawnedBullet = SpawnBullet(Bullet, playerPos);

        // Rotate bullet to face the direction of travel
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spawnedBullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Move the bullet using Rigidbody2D
        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D playerRb = player.GetComponentInParent<Rigidbody2D>();
        if (bulletRb != null)
        {
            Vector2 playerVelocity = Vector2.zero;
            playerVelocity = playerRb.linearVelocity;
            Vector2 bulletVelocity = direction * BulletSpeed;
            bulletRb.linearVelocity = bulletVelocity;
            //rb.linearVelocity = direction * BulletSpeed;
        }
        int timer = 5;
        CanShoot = false;
        StartCoroutine(DestroyTimer(timer, spawnedBullet)); // Destroy bullet after 5 seconds
    }
}
