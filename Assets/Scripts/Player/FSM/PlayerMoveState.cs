using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private static Vector2 _inputValue;
    private static float _verticalVelocity;

    private PlayerMovementModule _movementModule;


    public PlayerMoveState(StateMachine machine) : base(machine)
    {
        _movementModule = _machine.Owner.GetPlayerModule<PlayerMovementModule>();
    }

    public override void EnterState()
    {
        _inputReader.OnMovementEvent += SetInputValue;
        _inputReader.OnRClickEvent += ChangeToAim;
    }

    public override void ExitState()
    {
        _inputReader.OnMovementEvent -= SetInputValue;
        _inputReader.OnRClickEvent -= ChangeToAim;
    }

    public override void UpdateState()
    {
        Vector3 mousePos = CameraManager.Instance.GetMousePos(1 << Define.GroundLayer) - Transform.position;
        mousePos.y = 0f;
        
        
        Transform.rotation = Quaternion.LookRotation(mousePos);
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        
        if(!_movementModule.IsGrounded)
        {
            _verticalVelocity = _movementModule.CalculateGravity(_verticalVelocity * 0.1f);
        }
        Vector3 movement = _movementModule.CalculateMovement(_inputValue) + Vector3.up * _verticalVelocity;
        _movementModule.CharacterController.Move(movement);
    }

    private void SetInputValue(Vector2 velocity)
    {
        _inputValue = velocity;
    }

    private void ChangeToAim(bool isDown)
    {
        if(isDown)
        {
            _machine.ChangeState(typeof(PlayerAimState));
        }
    }
}
