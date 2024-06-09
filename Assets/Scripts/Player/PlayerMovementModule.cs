using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementModule : PlayerModule
{
    public CharacterController CharacterController {get; private set; }

    public override void Init(params object[] param)
    {
        base.Init(param);
        CharacterController = _playerController.Controller;
    }


    public Vector3 CalculateMovement(Vector2 movementInput)
    {
        Vector3 movement;

        Vector2 inputValue = (movementInput) * Time.fixedDeltaTime;
        float moveSpeed = 8f;
        //Quaternion rotateQuat = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        Quaternion rotateQuat = Quaternion.Euler(Vector3.forward);

        movement = rotateQuat *
            new Vector3(inputValue.x * moveSpeed,
                0,
                inputValue.y * moveSpeed);

        return movement;
    }


}
