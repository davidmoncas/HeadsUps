using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static bool _shuttingDown = false;
    private static object _lock = new object();

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_shuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (_lock)
            {

                if (_instance == null)
                {

                    _instance = (T)FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                    else {
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }

                return _instance;
            }
        }
    }


    protected void OnEnable()
    {
        _shuttingDown = false;
    }

}
