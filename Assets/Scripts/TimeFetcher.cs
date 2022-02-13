using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;


public class TimeFetcher : Singleton<TimeFetcher>
{
    public DateTime? currentDateTime;
    public bool autoTimeDisabled;
    public bool dontCheckAutoTime;
    [HideInInspector] public System.DateTime lastOpenedDate;
    [HideInInspector] public System.DateTime lastPausedDate;
    public void StopAutoTimeCheck()
    {
        dontCheckAutoTime = true;
    }
    private void OnEnable()
    {
        EventManager.StartListening("Everything Loaded", GetCurrentTime);
    }
    public void GetCurrentTime()
    {
#if !DEVELOPMENT_BUILD
        dontCheckAutoTime = false;
#endif
        //TODO
        //Remove Test lines
        if (dontCheckAutoTime)
        {
            currentDateTime = DateTime.UtcNow;
            EventManager.TriggerEvent("Date And Time Updated");
            return;
        }
        using (var client = new HttpClient())
        {
            try
            {
                client.Timeout = new TimeSpan(0, 0, 0, 1, 0);
                var result = client.GetAsync("https://google.com",
                      HttpCompletionOption.ResponseHeadersRead).Result;
                currentDateTime = result.Headers.Date.Value.UtcDateTime;
                EventManager.TriggerEvent("Date And Time Updated");
            }
            catch
            {
                if (CheckAutomaticTime())
                {
                    currentDateTime = DateTime.UtcNow;
                    EventManager.TriggerEvent("Date And Time Updated");
                }
                else
                {
                    FailedToGetAutomaticTime();
                }
            }
        }
    }
    public DateTime? GetCurrentTimeInternal()
    {
        if (CheckAutomaticTime())
        {
            currentDateTime = DateTime.UtcNow;
        }
        else
        {
            currentDateTime = lastOpenedDate;
            FailedToGetAutomaticTime();
        }
        return currentDateTime;
    }
    public bool CheckAutomaticTime()
    {
        //TODO
        //Remove Test line
        if (dontCheckAutoTime) return true;

#if !UNITY_EDITOR && UNITY_ANDROID
        bool setToAutomaticTime = false;
        using (var actClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            var context = actClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass systemGlobal = new AndroidJavaClass("android.provider.Settings$System");

            var autoTimeMode = systemGlobal.CallStatic<int>("getInt", context.Call<AndroidJavaObject>("getContentResolver"), "auto_time");
            setToAutomaticTime = Convert.ToBoolean(autoTimeMode);

        }
        return setToAutomaticTime;
#else
        return true;
#endif

    }
    public bool IsItANewDay()
    {

        var oldDateTime = lastOpenedDate;
        var currentDateTime = GetCurrentTimeInternal().Value;
        if (oldDateTime != default)
        {
            if (oldDateTime.Date != currentDateTime.Date)
                return true;
            else
                return false;
        }
        return true;
    }
    public double TimePassedSinceLastOpenInSeconds()
    {
        var lastPausedDateTime = lastPausedDate;
        var lastClosedDateTime = lastOpenedDate;

        DateTime lastDate;
        if (lastPausedDate == default)
            lastDate = lastClosedDateTime;
        else
            lastDate = lastPausedDateTime;


        var currentDateTime = GetCurrentTimeInternal();
        if (lastDate != default)
        {
            var timePassed = (currentDateTime - lastDate).Value;
            return timePassed.TotalSeconds;
        }
        else
        {
            return 0;
        }
    }



    public void FailedToGetAutomaticTime()
    {
        autoTimeDisabled = true;
        EventManager.TriggerEvent("Pause Game");
        EventManager.TriggerEvent("Automatic Date Time Popup Active", true);
    }
 
    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            if (CheckAutomaticTime())
            {
                currentDateTime = DateTime.UtcNow;
                EventManager.TriggerEvent("Date And Time Updated");
                lastOpenedDate = currentDateTime.Value;
                EventManager.TriggerEvent("Resume Game");
                EventManager.TriggerEvent("Automatic Date Time Popup Active", false);
                autoTimeDisabled = false;
            }
        }
        else lastPausedDate = GetCurrentTimeInternal().Value;


    }



}
