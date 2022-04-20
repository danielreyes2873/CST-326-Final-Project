using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get => instance; }

    public static bool IsInitialized { get => instance != null; }

    protected virtual void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = (T)this;
        }
    }

    protected virtual void OnDestory()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
    
}
