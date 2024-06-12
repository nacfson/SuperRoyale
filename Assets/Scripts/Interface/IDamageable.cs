using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int CurrentHP { get; set; }
    public void Damage(int damage);
}
