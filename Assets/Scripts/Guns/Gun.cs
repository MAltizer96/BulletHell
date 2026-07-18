using System.Collections;
using UnityEngine;

public abstract class Gun : MonoBehaviour, iGun
{

    [SerializeField]
    private bool canShoot = true;

    public float shootCooldown;
    public float baseCooldown = .5f;

    public float bulletSpeed = 10f;
    private bool automatic = false;
    public float knockBack = 5f;
    public int bulletDamage = 1;

    public bool CanShoot { get => canShoot; set => canShoot = value; }
    public float ShootCooldown { get => shootCooldown; set => shootCooldown = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public bool IsAutomatic { get => automatic; set => automatic = value; }
    public float KnockBack { get => knockBack; set => knockBack = value; }
    public int BulletDamage { get => bulletDamage; set => bulletDamage = value; }

    private void Awake()
    {
        shootCooldown = baseCooldown;
    }
    private void Update()
    {
        if (!CanShoot)
        {
            if (ShootCooldown > 0)
            {
                ShootCooldown -= Time.deltaTime;
            }
            else
            {
                CanShoot = true; // reset when cooldown is done
                ShootCooldown = baseCooldown; // reset cooldown timer
            }
        } 
    }
    public IEnumerator DestroyTimer(int time, GameObject bullet)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }

    public void ResetToBaseGun()
    {
        Gun baseGun = gameObject.GetComponent<BaseGun>();
        if (baseGun == this)
        {
            return;
        }
        else
        {
            this.enabled = false; // Disable the current gun script
            if (baseGun == null)
            {
                gameObject.AddComponent<BaseGun>();
            }
            baseGun.enabled = true;
        }
    }

    //public void setBulletSpeed(float newSpeed)
    //{
    //    BulletSpeed = newSpeed;
    //}

    //public void setShootCooldown(float newCooldown)
    //{
    //    ShootCooldown = newCooldown;
    //}

    public abstract void Shoot(Vector2 playerPos, GameObject Bullet, Vector2 MousePos);

    public GameObject SpawnBullet(GameObject Bullet, Vector2 playerPos)
    {
        // Spawn
        GameObject bullet = Instantiate(Bullet, playerPos, Quaternion.identity);
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        bulletComponent.KnockBack = this.KnockBack;
        bulletComponent.BulletDamage = this.BulletDamage;
        return bullet;
    }



}
