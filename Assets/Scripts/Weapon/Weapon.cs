using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon<T> : PoolableMono where T : WeaponData
{
    [SerializeField] protected T _weaponData;
    protected PlayerController _owner;

    public abstract void Attack();


    public virtual void Setting(PlayerController controller)
    {
        _owner = controller;

    }
}
