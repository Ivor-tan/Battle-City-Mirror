using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Singleton<T> : Base where T :Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get => instance;
    }

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