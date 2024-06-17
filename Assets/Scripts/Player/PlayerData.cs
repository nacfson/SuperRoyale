using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Player/Data")]
public class PlayerData : ScriptableObject
{
    public float MoveSpeed = 8f;

    public float RollTime = 0.3f;
    public float RollCoolTime = 3f;
    public float RollPower = 15f;
}
