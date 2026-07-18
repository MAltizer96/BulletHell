using UnityEngine;

public class PickUpSMG : MonoBehaviour
{
    [SerializeField]
    float timerWithGun;
    
    private MachineGun SMGgun;
    private TrackGuns trackGuns;

    private void Awake()
    {
        SMGgun = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MachineGun>();
        trackGuns = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TrackGuns>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger zone.");
            trackGuns.CurrentGun = SMGgun;
            Destroy(gameObject);
        }
    }
}
