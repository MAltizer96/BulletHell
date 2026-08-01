using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    GameObject Bullet;
    [SerializeField]
    TrackGuns trackGuns;
    [SerializeField]
    iGun currentGun;

    private PlayerHealth playerHealth;

    private void OnEnable()
    {
        PlayerEvents.OnGunChanged += HandleGunChanged;

    }
    private void OnDisable()
    {
        PlayerEvents.OnGunChanged -= HandleGunChanged;
    }
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        // Prefer an enabled implementation of iGun if multiple are attached.
        TrackGuns trackGuns = GetComponent<TrackGuns>();
        var guns = GetComponents<iGun>();
        //if (trackGuns != null)
        //    PlayerEvents.OnGunChanged += HandleGunChanged;

        currentGun = trackGuns.CurrentGun;

        if (currentGun == null && guns.Length > 0)
            currentGun = guns[0];

        if (currentGun == null)
            Debug.LogWarning("No iGun implementation found on player.");
    }
    private void Update()
    {
        bool shouldShoot = false;
        if(playerHealth.IsDead)
        {
            return; // Don't allow shooting if the player is dead
        }
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
        //Debug.Log("try to shoot");
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector2 playerPos = transform.position;
        currentGun.Shoot(playerPos, Bullet, mouseWorldPos);
    }

    private void HandleGunChanged(iGun newGun)
    {
        currentGun = newGun;
    }

    private void PlayerReset()
    {

    }

}


