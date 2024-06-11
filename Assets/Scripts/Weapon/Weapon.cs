using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon<T> : PoolableMono where T : WeaponData
{
    [SerializeField] protected T _weaponData;

    public abstract void Attack();
}
