using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReloadState : PlayerMoveState
{
    private PlayerWeaponModule _weaponModule;
    public PlayerReloadState(StateMachine machine) : base(machine)
    {
        _weaponModule = machine.Owner.GetPlayerModule<PlayerWeaponModule>();
    }

    public override void EnterState()
    {
        base.EnterState();

        _inputReader.OnRClickEvent -= ChangeToAim;

        float reloadTime = _weaponModule.GunData.ReloadTime;
        _machine.Owner.StartCoroutine(ReloadCoroutine(reloadTime));
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private IEnumerator ReloadCoroutine(float reloadTime)
    {
        float timer = 0f;
        while(timer <= reloadTime)
        {
            timer += Time.deltaTime;
            Debug.Log($"timer: {timer}");
            yield return null;
        }
        _weaponModule.Reload();
        _machine.ChangeState(typeof(PlayerMoveState));
    }
}
