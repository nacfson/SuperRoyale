using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoSingleton<ObjectManager>, IInstanceable
{
    public void CreateInstance()
    {

    }

    public PoolableMono CreatedMono { get; private set; }


    public void AddMono(PoolableMono mono)
    {
        CreatedMono = mono;
    }
}
