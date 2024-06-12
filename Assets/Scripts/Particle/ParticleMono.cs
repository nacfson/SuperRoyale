using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMono : PoolableMono
{
    private ParticleSystem _particleSystem;
    public override void Init()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void Setting(Vector3 pos)
    {
        transform.position = pos;
        _particleSystem.Play();
    }
}
