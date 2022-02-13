using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{


    public bool watchAdvertisements;


    public bool audioOn = true, vibrationOn = true;


    public void ToggleAds()
    {
        watchAdvertisements = !watchAdvertisements;
    }
    private void Awake()
    {
        //GameAnalytics.Initialize();
        //GameManager singletonInstance = GameManager.Instance; // I do this to create the singleton object in the scene

#if UNITY_EDITOR
        watchAdvertisements = false;
#else
         watchAdvertisements = true;
#endif

    }


}
