using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponModule : PlayerModule
{
    private Gun _currentWeapon;

    public override void Init(params object[] param)
    {
        base.Init(param);

        _currentWeapon = _playerController.GetComponentInChildren<Gun>();
        _currentWeapon.Setting(_playerController.transform);
    }

    public void Shoot()
    {
        _currentWeapon.Attack();
    }
}
