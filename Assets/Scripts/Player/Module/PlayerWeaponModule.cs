using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponModule : PlayerModule
{
    private Gun _currentWeapon;
    public GunData GunData => _currentWeapon.GunData;

    public override void Init(params object[] param)
    {
        base.Init(param);

        _currentWeapon = _playerController.GetComponentInChildren<Gun>();
        _currentWeapon.Setting(_playerController);
    }

    public void Shoot()
    {
        _currentWeapon.Attack();
    }
}
