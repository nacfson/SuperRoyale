using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/Gun")]
public class GunData : WeaponData
{
    public EBullet eBulletType;
    public int MaxBullet;
    public int ShootBulletCnt = 1;
    public float ReloadTime;
}