using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : Weapon<GunData>
{
    [field: SerializeField] public GunData GunData {get; private set; }
    [SerializeField] private Transform _muzzleTrm;


    private int _currentBullet;
    public int CurrentBullet
    {
        get => _currentBullet;
        set
        {
            _currentBullet = Mathf.Clamp(value,0,GunData.MaxBullet);
        }
    }

    [field: SerializeField] public Bullet UseBullet { get; set; }


    public override void Init()
    {

    }

    public void Reload()
    {
        CurrentBullet = GunData.MaxBullet;

        var cntEvent = Events.BulletCntEvent;
        cntEvent.Setting(CurrentBullet, GunData.MaxBullet);
        EventManager.Broadcast(cntEvent);
    }
    public override void Setting(PlayerController controller)
    {
        base.Setting(controller);

        Reload();
        EventManager.AddListener<BulletShootingEvent>(ShootBullet);
        //controller.gameObject.SetActive(false);
    }

    public override void Attack()
    {
        if (!_owner.IsMine) return;

        string name = $"{EBullet.Default}Bullet";
        string methodName = nameof(NetworkManager.Instance.NetworkCreate_RPC);

        NetworkManager.Instance.RPCShooter(methodName, RpcTarget.All, name, _muzzleTrm.position);
        
        //can await this
        Vector3 mousePos = CameraManager.Singleton.GetMousePos(1 << Define.GroundLayer);
        Vector3 bulletDir = (mousePos - _owner.transform.position).normalized;

        NetworkManager.Instance.CreateEvent(RpcTarget.All,EventType.Bullet,_muzzleTrm.position, bulletDir);

        CurrentBullet--;

        var cntEvent = Events.BulletCntEvent;
        cntEvent.Setting(CurrentBullet, GunData.MaxBullet);
        EventManager.Broadcast(cntEvent);
    }

    private void ShootBullet(BulletShootingEvent Event)
    {
        Bullet bullet = ObjectManager.Singleton.CreatedMono as Bullet;
        bullet.Setting(Event.pos, Event.dir);
    }
}


