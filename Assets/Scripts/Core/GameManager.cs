using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{
    [field:SerializeField] public InputReader InputReader {get; private set; }

    private void Awake()
    {
        List<IInstanceable> instanceList = GetComponentsInChildren<IInstanceable>().ToList();
        instanceList.ForEach(i => i.CreateInstance());

        DontDestroyOnLoad(this.gameObject);
    }
}
