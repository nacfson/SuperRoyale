using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoroutineManager : MonoSingleton<CoroutineManager>, IInstanceable
{
    private Dictionary<MonoBehaviour, HashSet<Coroutine>> _coroutineDictionary;


    public void CreateInstance()
    {
        _coroutineDictionary = new Dictionary<MonoBehaviour, HashSet<Coroutine>>();


    }

}
