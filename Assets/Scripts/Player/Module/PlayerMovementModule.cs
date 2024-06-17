using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementModule : PlayerModule
{
    public CharacterController CharacterController {get; private set;}
    [SerializeField] private float _gravity = -9.81f;

    public bool IsGrounded => CharacterController.isGrounded;
    #region Rolling
    private Vector2 _lastInput;

    private bool _isRoll;
    public bool IsRoll
    {
        get => _isRoll;
        set
        {
            if(value)
            {
                if (_isRoll)
                    return;
                StartCoroutine(RollCoroutine(_lastInput));
            }
            _isRoll = value;
        }
    }
    #endregion

    public override void Init(params object[] param)
    {
        base.Init(param);
        CharacterController = _owner.Controller;
    }

    public Vector3 CalculateMovement(Vector2 movementInput)
    {
        if(movementInput.magnitude > 0.1f)
        {
            _lastInput = movementInput;
        }
        if (IsRoll) return Vector3.zero;
        Vector3 movement;

        Vector2 inputValue = (movementInput) * Time.fixedDeltaTime;
        float moveSpeed = _owner.CurrentPlayerData.MoveSpeed;
        //Quaternion rotateQuat = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        Quaternion rotateQuat = Quaternion.Euler(Vector3.forward);

        movement = rotateQuat *
            new Vector3(inputValue.x * moveSpeed,
                0,
                inputValue.y * moveSpeed);

        return movement;
    }
    
    private IEnumerator RollCoroutine(Vector2 movementInput)
    {
        float timer = 0f;
        while(timer <= _owner.CurrentPlayerData.RollTime)
        {
            timer += Time.deltaTime;
            CharacterController.Move(new Vector3(movementInput.x, 0, movementInput.y).normalized
                * _owner.CurrentPlayerData.RollPower  * Time.deltaTime);
            yield return null;
        }
        IsRoll = false;
    }
    
    public float CalculateGravity(float yVelocity)
    {
        return yVelocity + _gravity * Time.fixedDeltaTime;
    }
}
