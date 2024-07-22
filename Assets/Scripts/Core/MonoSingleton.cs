using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _singleton;
    public static T Singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = GameObject.FindObjectOfType(typeof(T)) as T;
            }

            if (_singleton == null)
            {
                Debug.LogError("Can't Find Instance!");
            }

            return _singleton;
        }
    }

}
