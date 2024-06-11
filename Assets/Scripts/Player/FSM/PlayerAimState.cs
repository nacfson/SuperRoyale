using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerMoveState
{
    private PlayerWeaponModule _weaponModule;
    public PlayerAimState(StateMachine<PlayerController> machine) : base(machine)
    {
        _weaponModule = machine.Owner.GetPlayerModule<PlayerWeaponModule>();
    }

    public override void EnterState()
    {
        base.EnterState();
        _inputReader.OnRClickEvent += ChangeToMove;
        _inputReader.OnShootEvent += Shoot;

        _machine.Owner.ChangePlayerData(EPLAYER_DATA.Aim);

        CameraManager.Instance.ZoomCamera(10f);
        //CameraManager.Instance.SetCameraTrack(true);
    }

    public override void ExitState()
    {
        base.ExitState();
        _inputReader.OnRClickEvent -= ChangeToMove;
        _inputReader.OnShootEvent -= Shoot;


        _machine.Owner.ChangePlayerData(EPLAYER_DATA.Default);
        CameraManager.Instance.ZoomCamera(15f);
        //CameraManager.Instance.SetCameraTrack(false);
    }

    private void Shoot()
    {
        _weaponModule.Shoot();
    }

    private void ChangeToMove(bool isFalse)
    {
        if(!isFalse)
        {
            _machine.ChangeState(typeof(PlayerMoveState));
        }
    }

}
