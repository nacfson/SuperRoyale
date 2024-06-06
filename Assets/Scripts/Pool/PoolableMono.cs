using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableMono : MonoBehaviour
{
    public int poolingCount = 5;
    public abstract void Init();

}
