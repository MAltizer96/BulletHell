using UnityEngine;
using System.Collections;
public abstract class Pickup : MonoBehaviour
{
    //[SerializeField]
    //float timerUntilDestroy;
    public virtual IEnumerator StartTimerUntilPickedUp(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
