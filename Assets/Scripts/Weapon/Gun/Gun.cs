using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon<GunData>
{
    [SerializeField] private GunData _gunData;
    [SerializeField] private Transform _muzzleTrm;

    private Transform _ownerTrm;

    private int _currentBullet;
    public int CurrentBullet
    {
        get => _currentBullet;
        set
        {
            _currentBullet = Mathf.Clamp(value,0,_gunData.MaxBullet);
        }
    }

    [field: SerializeField] public Bullet UseBullet { get; set; }


    public override void Init()
    {
        CurrentBullet = _gunData.MaxBullet;
    }

    public void Setting(Transform agentTrm)
    {
        _ownerTrm = agentTrm;
    }

    public override void Attack()
    {
        string name = $"{EBullet.Default}Bullet";
        string methodName = nameof(NetworkManager.Instance.NetworkCreate_RPC);

        NetworkManager.Instance.RPCShooter(methodName, RpcTarget.All, name);

        StartCoroutine(Test());
        return;
        Bullet bullet = ObjectManager.Instance.CreatedMono as Bullet;

        Vector3 mousePos = CameraManager.Instance.GetMousePos(1 << Define.GroundLayer);
        Vector3 bulletDir = mousePos - _ownerTrm.position;
        bullet.Setting(_muzzleTrm.position,bulletDir);

        CurrentBullet--;
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(1f);
        Bullet bullet = ObjectManager.Instance.CreatedMono.GetComponent<Bullet>();

        Vector3 mousePos = CameraManager.Instance.GetMousePos(1 << Define.GroundLayer);
        Vector3 bulletDir = mousePos - _ownerTrm.position;
        bullet.Setting(_muzzleTrm.position, bulletDir);

        CurrentBullet--;
    }


}
