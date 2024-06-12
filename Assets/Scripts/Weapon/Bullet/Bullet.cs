using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] protected BulletData _bulletData;
    private Vector3 _bulletDirection;

    public bool Enabled { get; private set; }

    public override void Init()
    {

    }

    public void Setting(Vector3 pos, Vector3 dir)
    {
        transform.position = pos;
        _bulletDirection = dir;
        Enabled = true;
    }

    protected virtual void Update()
    {
        if(Enabled)
        {
            transform.position += _bulletDirection * _bulletData.MoveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(_bulletData.Damage);
            PoolManager.Instance.Push(this);
        }
    }
}
