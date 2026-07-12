using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class GunHighlight : MonoBehaviour
{
    [SerializeField]
    private List<Image> gunImages = new List<Image>();

    private TrackGuns trackGuns;
    private void Start()
    {
        trackGuns = GameObject.FindGameObjectWithTag("Player").GetComponent<TrackGuns>();
        if (trackGuns != null)
            trackGuns.OnGunChanged += HandleGunChanged;

        gunImages.Clear();
        //look at this againe, have not tested
        var images = GetComponentsInChildren<Image>(true);
        foreach (var img in images)
        {
            gunImages.Add(img);
        }
        Debug.Log("CurrentGun Index: " + trackGuns.GetCurrentGunIndex());

        foreach (var g in gunImages)
        {
            Debug.Log("Gun Image: " + g.gameObject.name);
        }
    }


    public void HighlightGun(int gunIndex)
    {
        for (int i = 0; i < gunImages.Count; i++)
        {
            if (i == gunIndex)
            {
                gunImages[i].color = Color.yellow; // Highlight the selected gun
            }
            else
            {
                gunImages[i].color = Color.white; // Reset color for other guns
            }
        }

    }

    public void HandleGunChanged(Gun newGun)
    {
        int gunIndex = trackGuns.GetCurrentGunIndex();
        HighlightGun(gunIndex);
    }
}
    