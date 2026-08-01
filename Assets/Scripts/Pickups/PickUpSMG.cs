using System.Collections;
using UnityEngine;

public class PickUpSMG : Pickup
{
    [SerializeField]
    float timerWithGun;
    [SerializeField]
    float timerUntilDestroy;

    private MachineGun SMGgun;
    private TrackGuns trackGuns;

    private void Awake()
    {
        SMGgun = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MachineGun>();
        trackGuns = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TrackGuns>();
        StartCoroutine(StartTimerUntilPickedUp(timerUntilDestroy));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // need to fix pick up after gun type is already equip
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player entered the trigger zone.");
            PlayerEvents.GunChanged(SMGgun);
            //trackGuns.SetCurrentGun(SMGgun);
            Destroy(gameObject);
        }
    }

    //private IEnumerator StartTimerUntilPickedUp()
    //{
    //    yield return new WaitForSeconds(timerUntileGunDisappear);
    //    Destroy(gameObject);
    //}
}
