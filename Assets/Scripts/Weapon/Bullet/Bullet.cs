using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] protected BulletData _bulletData;
    [SerializeField] private LayerMask _layerMask;
    private SphereCollider _collider;

    private Vector3 _bulletDirection;

    public bool Enabled { get; private set; }

    public override void Init()
    {
        _collider = GetComponent<SphereCollider>();
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

            if(CollisionCheck(out RaycastHit hit))
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    if (hit.collider.TryGetComponent(out PlayerController controller))
                    {
                        NetworkManager.Instance.CreateEvent(RpcTarget.All,EventType.Damage, controller.ActorNumber, _bulletData.Damage);
                    }
                }
                ParticleMono testParticle = PoolManager.Singleton.Pop("TestParticle") as ParticleMono;
                testParticle.Setting(hit.point);
                PoolManager.Singleton.Push(this);
            }
        }
    }

    private bool CollisionCheck(out RaycastHit hit)
    {
        Vector3 pos = transform.position;
        float radius = _collider.radius;

        var result = Physics.SphereCast(pos - _bulletDirection, radius, _bulletDirection, out hit, _bulletDirection.magnitude, _layerMask);
        return result;
    }
}
