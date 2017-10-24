using UnityEngine;
using System.Collections;

public class ISingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static object _lock = new object();
    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log(singleton.name + " Created");
                    }
                }
            }

            if (instance == null)
            {
                Debug.LogError("Singleton is null!");
            }

            return instance;
        }
    }

    public static T Initialize() { return Instance; }
    public static bool IsCreated() { return (instance != null); }
}
