using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGuns : MonoBehaviour
{


    private iGun currentGun;
    public List<iGun> allGuns;

    Coroutine gunTimerRoutine;
    public iGun CurrentGun
    {
        get => currentGun;
        set
        {
            if (currentGun == value) return;
            currentGun = value;
            //PlayerEvents.GunChanged(currentGun);
            //SetCurrentGun(currentGun);

        }
    }

    private void OnEnable()
    {
        PlayerEvents.OnGunChanged += SetCurrentGun;
        PlayerEvents.OnPlayerDied += (_) => ResetToBaseGun();
        //PlayerEvents.OnPlayerDied += ResetToBaseGun;
    }

    private void OnDisable()
    {
        PlayerEvents.OnGunChanged -= SetCurrentGun;
        PlayerEvents.OnPlayerDied -= (_) => ResetToBaseGun();
    }

    private void Awake()
    {


        // Ensure the list exists and is empty
        if (allGuns == null) allGuns = new List<iGun>();
        allGuns.Clear();

        var gunComponents = GetComponents<iGun>();

        // adds all guns to the list of all guns
        foreach (var gun in gunComponents)
        {
            allGuns.Add(gun);
        }
        
        iGun enabledGun = null;
        foreach (var g in gunComponents)
        {
            // Check if the gun is enabled and set it as the current gun
            Behaviour bg = g as Behaviour;

            if (bg.enabled)
            {
                Debug.Log("Found enabled gun: " + g.GetType().Name);
                enabledGun = g;
                break;
            }
        }

        if (enabledGun == null && allGuns.Count > 0)
            enabledGun = allGuns[0];
        // Sets the first gun in line to be the enabled gun if no other gun is enabled
        var behaviour = enabledGun as Behaviour;
        if (behaviour != null)
            behaviour.enabled = true; // Ensure the enabled gun is active

        //Debug.Log("Endabled Gun: "+ enabledGun);
        if (enabledGun != null)
            SetCurrentGun(enabledGun);


    }

    //public void SetCurrentGun(int index)
    //{
    //    if (index < 0 || index >= allGuns.Count) return;

    //    SetCurrentGun(allGuns[index]);
    //}
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
    public void SetCurrentGun(iGun gun)
    {

        if (gun == null || !allGuns.Contains(gun)) return;

        // Optionally enable/disable gun components so only the active is enabled

        foreach (var g in allGuns)
        {
            var mb = g as MonoBehaviour;
            if (mb != null) mb.enabled = (g == gun);
        }
        if (gun.GetType().Name == "BaseGun")
        {
            if (gunTimerRoutine != null)
            {
                StopCoroutine(gunTimerRoutine);
                gunTimerRoutine = null;
                currentGun = gun; // Set the current gun to BaseGun
                return;
            }
        }
        CurrentGun = gun;

        if (gunTimerRoutine != null)
        {
            StopCoroutine(gunTimerRoutine);
        }
        gunTimerRoutine = StartCoroutine(StartNewGun(CurrentGun.Timer));

    }

    public IEnumerator StartNewGun(float time)
    {
        Debug.Log("Starting new gun timer for: " + CurrentGun.GetType().Name + " for " + time + " seconds.");
        yield return new WaitForSeconds(time);
        Debug.Log("Gun timer ended for: " + CurrentGun.GetType().Name + ". Resetting to base gun.");
        ResetToBaseGun();
        gunTimerRoutine = null;
    }

    public void ResetToBaseGun()
    {
        iGun baseGun = gameObject.GetComponent<BaseGun>();
        Debug.Log("Resetting to base gun: " + baseGun.GetType().Name);
        if (baseGun == currentGun)
        {
            Debug.Log("Already using base gun. No action taken.");
            return;
        }
        else
        {
            //this.enabled = false; // Disable the current gun script
            var currentGunMono = currentGun as MonoBehaviour;
            currentGunMono.enabled = false; // Disable the current gun script
            //foreach (var gun in allGuns)
            //{
            //    var selectedGun = gun as MonoBehaviour;
            //    var 
            //    if (selectedGun == currentGun)
            //    {
            //        selectedGun.enabled = false; // Disable all gun scripts
            //    }               
            //}

            //currentGun = baseGun;
            PlayerEvents.GunChanged(baseGun);
            //var currentGunMono2 = currentGun as MonoBehaviour;
            //if (currentGunMono2.enabled == false)
            //{
            //    currentGunMono2.enabled = true; // Enable the base gun script
            //}
            ////if (baseGun == null)
            ////{
            ////    gameObject.AddComponent<BaseGun>();
            ////}
            //CurrentGun = baseGun;
        }
    }
}
