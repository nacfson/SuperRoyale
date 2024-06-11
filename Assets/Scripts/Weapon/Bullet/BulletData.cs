using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/Bullet")]
public class BulletData : ScriptableObject
{
    public float MoveSpeed;
    public float Damage;
    public float MaxDistance;
}
