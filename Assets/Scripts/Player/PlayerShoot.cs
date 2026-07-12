using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    TrackGuns trackGuns;

    iGun currentGun;
    void Start()
    {
        // Prefer an enabled implementation of iGun if multiple are attached.
        TrackGuns trackGuns = GetComponent<TrackGuns>();
        var guns = GetComponents<iGun>();
        if (trackGuns != null)
            trackGuns.OnGunChanged += HandleGunChanged;

        currentGun = trackGuns.CurrentGun;
        //foreach (var g in guns)
        //{
        //    var mb = g as MonoBehaviour;
        //    if (mb != null && mb.enabled)
        //    {
        //        currentGun = g;
        //        break;
        //    }
        //}

        // Fallback: if none enabled, use the first one found (if any)
        if (currentGun == null && guns.Length > 0)
            currentGun = guns[0];

        if (currentGun == null)
            Debug.LogWarning("No iGun implementation found on player.");
    }
    private void Update()
    {
        bool shouldShoot = false;

        if (currentGun.IsAutomatic)
        {
            // Automatic gun - fires every frame the button is held
            shouldShoot = Mouse.current.leftButton.isPressed;
        }
        else
        {
            // Manual gun - fires once per click
            shouldShoot = Mouse.current.leftButton.wasPressedThisFrame;
        }
        if (shouldShoot)
        {
            TryShoot();
        }
    }

    private void TryShoot()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector2 playerPos = transform.position;
        currentGun.Shoot(playerPos, Bullet, mouseWorldPos);
    }

    private void HandleGunChanged(iGun newGun)
    {
        currentGun = newGun;
    }
    private void OnDisable()
    {
        if (trackGuns != null)
            trackGuns.OnGunChanged -= HandleGunChanged;
    }

}
//private void OnShoot(InputValue input)
//    {

//        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
//        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

//        if(currentGun != null)
//        {
//            Vector2 playerPos = transform.position;
//            currentGun.Shoot(playerPos, Bullet, mouseWorldPos);
//        }
//        else
//        {
//            Debug.LogWarning("No gun component found on the player.");
//        }

//    }

