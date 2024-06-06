using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;
            }

            if (_instance == null)
            {
                Debug.LogError("Can't Find Instance!");
            }

            return _instance;
        }
    }

}
