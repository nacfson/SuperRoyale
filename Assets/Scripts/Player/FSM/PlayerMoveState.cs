using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMoveState : PlayerState
{
    private Vector2 _inputValue;
    private PlayerMovementModule _movementModule;

    public bool RotateEnable { get; private set; }

    public PlayerMoveState(StateMachine<PlayerController> machine) : base(machine)
    {
        _movementModule = _machine.Owner.GetPlayerModule<PlayerMovementModule>();
    }

    public override void EnterState()
    {
        _inputReader.OnMovementEvent += SetInputValue;
        _inputReader.OnRClickEvent += CalculateRotation;
    }

    public override void ExitState()
    {
        _inputReader.OnMovementEvent -= SetInputValue;
        _inputReader.OnRClickEvent -= CalculateRotation;
    }

    public override void UpdateState()
    {
        if (RotateEnable)
        {
            Vector3 mousePos = GameManager.Instance.CurrentMousePos;
            Transform.rotation = Quaternion.LookRotation(mousePos);
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        Vector3 movement = _movementModule.CalculateMovement(_inputValue);
        _movementModule.CharacterController.Move(movement);
    }

    private void SetInputValue(Vector2 velocity)
    {
        _inputValue = velocity;
    }

    private void CalculateRotation(bool isDown)
    {
        RotateEnable = isDown;
    }
}
