using System;
using System.Collections.Generic;
using UnityEngine;

public class TrackGuns : MonoBehaviour
{
    // need to update this class to select the gun that the playyer is using and update all other instance to represent based off this class

    private Gun currentGun;
    public List<Gun> allGuns;

    public event Action<Gun> OnGunChanged;
    public Gun CurrentGun
    {
        get => currentGun;
        private set
        {
            if (currentGun == value) return;
            currentGun = value;
            OnGunChanged?.Invoke(currentGun);
        }
    }

    public int GetCurrentGunIndex()
    {
        for (int i = 0; i < allGuns.Count; i++)
        {
            if (allGuns[i] == CurrentGun)
            {
                return i;
            }
        }
        return 0; // Return null if the current gun is not found in the list
    }
    private void Awake()
    {
        // Ensure the list exists and is empty
        if (allGuns == null) allGuns = new List<Gun>();
        allGuns.Clear();

        var gunComponents = GetComponents<Gun>();

        // adds all guns to the list of all guns
        foreach (var gun in gunComponents)
        {
            allGuns.Add(gun);
        }
        
        Gun enabledGun = null;
        foreach (var g in gunComponents)
        {
            // Check if the gun is enabled and set it as the current gun
            if (g.enabled)
            {
                Debug.Log("Found enabled gun: " + g.GetType().Name);
                enabledGun = g;
                break;
            }
        }

        // Sets the first gun in line to be the enabled gun if no other gun is enabled
        if (enabledGun == null && allGuns.Count > 0)
            enabledGun = allGuns[0];
            enabledGun.enabled = true; // Ensure the enabled gun is active

        Debug.Log("Endabled Gun: "+ enabledGun);
        if (enabledGun != null)
            SetCurrentGun(enabledGun);
    }

    public void SetCurrentGun(int index)
    {
        if (index < 0 || index >= allGuns.Count) return;
        SetCurrentGun(allGuns[index]);
    }

    public void SetCurrentGun(Gun gun)
    {
        if (gun == null || !allGuns.Contains(gun)) return;

        // Optionally enable/disable gun components so only the active is enabled
        foreach (var g in allGuns)
        {
            var mb = g as MonoBehaviour;
            if (mb != null) mb.enabled = (g == gun);
        }

        CurrentGun = gun;
    }

}
