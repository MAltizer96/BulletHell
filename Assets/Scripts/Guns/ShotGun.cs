using UnityEngine;

public class Shotgun : Gun
{
    public override GunType GunType => throw new System.NotImplementedException();

    public override void Shoot(Vector2 playerPos, GameObject Bullet, Vector2 MousePos)
    {
        throw new System.NotImplementedException();
    }
}
