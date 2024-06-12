using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimState : PlayerMoveState
{
    private PlayerWeaponModule _weaponModule;
    private bool _enabled = false;


    public PlayerAimState(StateMachine machine) : base(machine)
    {
        _weaponModule = machine.Owner.GetPlayerModule<PlayerWeaponModule>();
    }

    public override void EnterState()
    {
        base.EnterState();
        _enabled = true;
        _inputReader.OnRClickEvent += ChangeToMove;
        _inputReader.OnShootEvent += Shoot;

        _machine.Owner.ChangePlayerData(EPLAYER_DATA.Aim);

        CameraManager.Instance.ZoomCamera(10f);
        //CameraManager.Instance.SetCameraTrack(true);
    }

    public override void ExitState()
    {
        base.ExitState();
        _enabled = false;
        _inputReader.OnRClickEvent -= ChangeToMove;
        _inputReader.OnShootEvent -= Shoot;


        _machine.Owner.ChangePlayerData(EPLAYER_DATA.Default);
        CameraManager.Instance.ZoomCamera(15f);
        //CameraManager.Instance.SetCameraTrack(false);
    }


    private void Shoot()
    {
        if (!_enabled) return;
        if (_weaponModule.CanShoot)
        {
            _weaponModule.Shoot();
        }
        else
        {
            _machine.ChangeState(typeof(PlayerReloadState));
        }
    }

    private void ChangeToMove(bool isFalse)
    {
        if(!isFalse)
        {
            _machine.ChangeState(typeof(PlayerMoveState));
        }
    }
}
