using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
//using GameAnalyticsSDK;


public enum GameAnalyticsEvents { LevelCompleted, NewLevelStarted, DeviceBuggedCanNotVibrate }
public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent<bool>> eventDictionaryBool;
    private Dictionary<string, UnityEvent<int>> eventDictionaryInt;
    private Dictionary<string, UnityEvent<int, float>> eventDictionaryIntFloat;
    private Dictionary<string, UnityEvent<float>> eventDictionaryFloat;
    private Dictionary<string, UnityEvent<string>> eventDictionaryString;
    private Dictionary<string, UnityEvent<Color>> eventDictionaryColor;
    private Dictionary<string, UnityEvent<UnityAction>> eventDictionaryUnityAction;
    private Dictionary<string, UnityEvent<Sprite>> eventDictionarySprite;
    private Dictionary<string, UnityEvent<object>> eventDictionaryObject;
    private Dictionary<string, UnityEvent<Vector3>> eventDictionaryVector3;
    private Dictionary<string, UnityEvent<int, Vector3>> eventDictionaryIntVector3;
    private Dictionary<string, UnityEvent> eventDictionaryNull;



    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {

        if (eventDictionaryBool == null)
        {
            eventDictionaryBool = new Dictionary<string, UnityEvent<bool>>();
        }
        if (eventDictionaryInt == null)
        {
            eventDictionaryInt = new Dictionary<string, UnityEvent<int>>();
        }
        if (eventDictionaryObject == null)
        {
            eventDictionaryObject = new Dictionary<string, UnityEvent<object>>();
        }
        if (eventDictionaryFloat == null)
        {
            eventDictionaryFloat = new Dictionary<string, UnityEvent<float>>();
        }
        if (eventDictionaryIntFloat == null)
        {
            eventDictionaryIntFloat = new Dictionary<string, UnityEvent<int, float>>();
        }
        if (eventDictionaryString == null)
        {
            eventDictionaryString = new Dictionary<string, UnityEvent<string>>();
        }
        if (eventDictionaryColor == null)
        {
            eventDictionaryColor = new Dictionary<string, UnityEvent<Color>>();
        }
        if (eventDictionarySprite == null)
        {
            eventDictionarySprite = new Dictionary<string, UnityEvent<Sprite>>();
        }
        if (eventDictionaryUnityAction == null)
        {
            eventDictionaryUnityAction = new Dictionary<string, UnityEvent<UnityAction>>();
        }
        if (eventDictionaryVector3 == null)
        {
            eventDictionaryVector3 = new Dictionary<string, UnityEvent<Vector3>>();
        }

        if (eventDictionaryNull == null)
        {
            eventDictionaryNull = new Dictionary<string, UnityEvent>();
        }


    }


    #region EventColor
    [System.Serializable]
    public class ColorEvent : UnityEvent<Color>
    {

    }

    public static void StartListening(string eventName, UnityAction<Color> listener)
    {
        UnityEvent<Color> thisEvent = null;
        if (instance.eventDictionaryColor.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ColorEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryColor.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<Color> listener)
    {
        if (eventManager == null) return;
        UnityEvent<Color> thisEvent = null;
        if (instance.eventDictionaryColor.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, Color toggle)
    {
        UnityEvent<Color> thisEvent = null;
        if (instance.eventDictionaryColor.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion         
    #region EventObject
    [System.Serializable]
    public class ObjectEvent : UnityEvent<object>
    {

    }

    public static void StartListening(string eventName, UnityAction<object> listener)
    {
        UnityEvent<object> thisEvent = null;
        if (instance.eventDictionaryObject.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ObjectEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryObject.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<object> listener)
    {
        if (eventManager == null) return;
        UnityEvent<object> thisEvent = null;
        if (instance.eventDictionaryObject.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, object toggle)
    {
        UnityEvent<object> thisEvent = null;
        if (instance.eventDictionaryObject.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion      
    #region EventBool
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {

    }

    public static void StartListening(string eventName, UnityAction<bool> listener)
    {
        UnityEvent<bool> thisEvent = null;
        if (instance.eventDictionaryBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new BoolEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryBool.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<bool> listener)
    {
        if (eventManager == null) return;
        UnityEvent<bool> thisEvent = null;
        if (instance.eventDictionaryBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, bool toggle)
    {
        UnityEvent<bool> thisEvent = null;
        if (instance.eventDictionaryBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion     
    #region EventSprite
    [System.Serializable]
    public class SpriteEvent : UnityEvent<Sprite>
    {

    }

    public static void StartListening(string eventName, UnityAction<Sprite> listener)
    {
        UnityEvent<Sprite> thisEvent = null;
        if (instance.eventDictionarySprite.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new SpriteEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionarySprite.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<Sprite> listener)
    {
        if (eventManager == null) return;
        UnityEvent<Sprite> thisEvent = null;
        if (instance.eventDictionarySprite.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, Sprite toggle)
    {
        UnityEvent<Sprite> thisEvent = null;
        if (instance.eventDictionarySprite.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion  
    #region EventInt
    [System.Serializable]
    public class IntEvent : UnityEvent<int>
    {

    }

    public static void StartListening(string eventName, UnityAction<int> listener)
    {
        UnityEvent<int> thisEvent = null;
        if (instance.eventDictionaryInt.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new IntEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryInt.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<int> listener)
    {
        if (eventManager == null) return;
        UnityEvent<int> thisEvent = null;
        if (instance.eventDictionaryInt.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, int toggle)
    {
        UnityEvent<int> thisEvent = null;
        if (instance.eventDictionaryInt.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion 
    #region EventFloat
    [System.Serializable]
    public class FloatEvent : UnityEvent<float>
    {

    }

    public static void StartListening(string eventName, UnityAction<float> listener)
    {
        UnityEvent<float> thisEvent = null;
        if (instance.eventDictionaryFloat.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new FloatEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryFloat.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<float> listener)
    {
        if (eventManager == null) return;
        UnityEvent<float> thisEvent = null;
        if (instance.eventDictionaryFloat.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, float toggle)
    {
        UnityEvent<float> thisEvent = null;
        if (instance.eventDictionaryFloat.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion   
    #region EventIntFloat
    [System.Serializable]
    public class IntFloatEvent : UnityEvent<int, float>
    {

    }

    public static void StartListening(string eventName, UnityAction<int, float> listener)
    {
        UnityEvent<int, float> thisEvent = null;
        if (instance.eventDictionaryIntFloat.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new IntFloatEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryIntFloat.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<int, float> listener)
    {
        if (eventManager == null) return;
        UnityEvent<int, float> thisEvent = null;
        if (instance.eventDictionaryIntFloat.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, int toggleOne, float toggleTwo)
    {
        UnityEvent<int, float> thisEvent = null;
        if (instance.eventDictionaryIntFloat.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggleOne, toggleTwo);
        }
    }
    #endregion  


    #region EventString
    [System.Serializable]
    public class StringEvent : UnityEvent<string>
    {

    }

    public static void StartListening(string eventName, UnityAction<string> listener)
    {
        UnityEvent<string> thisEvent = null;
        if (instance.eventDictionaryString.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new StringEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryString.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<string> listener)
    {
        if (eventManager == null) return;
        UnityEvent<string> thisEvent = null;
        if (instance.eventDictionaryString.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, string toggle)
    {
        UnityEvent<string> thisEvent = null;
        if (instance.eventDictionaryString.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion   
    #region EventUnityAction
    [System.Serializable]
    public class UnityActionEvent : UnityEvent<UnityAction>
    {

    }

    public static void StartListening(string eventName, UnityAction<UnityAction> listener)
    {
        UnityEvent<UnityAction> thisEvent = null;
        if (instance.eventDictionaryUnityAction.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityActionEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryUnityAction.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<UnityAction> listener)
    {
        if (eventManager == null) return;
        UnityEvent<UnityAction> thisEvent = null;
        if (instance.eventDictionaryUnityAction.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, UnityAction toggle)
    {
        UnityEvent<UnityAction> thisEvent = null;
        if (instance.eventDictionaryUnityAction.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion

    #region EventVector3
    [System.Serializable]
    public class Vector3Event : UnityEvent<Vector3>
    {

    }

    public static void StartListening(string eventName, UnityAction<Vector3> listener)
    {
        UnityEvent<Vector3> thisEvent = null;
        if (instance.eventDictionaryVector3.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new Vector3Event();
            thisEvent.AddListener(listener);
            instance.eventDictionaryVector3.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<Vector3> listener)
    {
        if (eventManager == null) return;
        UnityEvent<Vector3> thisEvent = null;
        if (instance.eventDictionaryVector3.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, Vector3 toggle)
    {
        UnityEvent<Vector3> thisEvent = null;
        if (instance.eventDictionaryVector3.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion

    #region EventIntVector3
    [System.Serializable]
    public class IntVector3Event : UnityEvent<int, Vector3>
    {

    }

    public static void StartListening(string eventName, UnityAction<int, Vector3> listener)
    {
        UnityEvent<int, Vector3> thisEvent = null;
        if (instance.eventDictionaryIntVector3.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new IntVector3Event();
            thisEvent.AddListener(listener);
            instance.eventDictionaryIntVector3.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<int, Vector3> listener)
    {
        if (eventManager == null) return;
        UnityEvent<int, Vector3> thisEvent = null;
        if (instance.eventDictionaryIntVector3.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, int toggle0, Vector3 toggle1)
    {
        UnityEvent<int, Vector3> thisEvent = null;
        if (instance.eventDictionaryIntVector3.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle0, toggle1);
        }
    }
    #endregion   

 

    #region EventNone
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionaryNull.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryNull.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionaryNull.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionaryNull.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
    #endregion
}