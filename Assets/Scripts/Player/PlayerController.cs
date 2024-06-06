using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputReader _inputReader;
    private CharacterController _controller;
    private Vector2 _inputValue;

    public bool RotateEnable { get; private set; }
    
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _inputReader = GameManager.Instance.InputReader;

        _controller = GetComponent<CharacterController>();
        _inputReader.OnMovementEvent += SetInputValue;
        _inputReader.OnRClickEvent += CalculateRotation;
    }

    private void Update()
    {
        if(RotateEnable)
        {
            Vector3 mousePos = GameManager.Instance.CurrentMousePos;
            transform.rotation = Quaternion.LookRotation(mousePos);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = CalculateMovement();
        _controller.Move(movement);
    }

    private void SetInputValue(Vector2 velocity)
    {
        _inputValue = velocity;
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement;

        Vector2 inputValue = (_inputValue) * Time.fixedDeltaTime;
        float moveSpeed = 8f;
        //Quaternion rotateQuat = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        Quaternion rotateQuat = Quaternion.Euler(Vector3.forward);

        movement = rotateQuat *
            new Vector3(inputValue.x * moveSpeed,
                0,
                inputValue.y * moveSpeed);

        return movement;
    }

    private void CalculateRotation(bool isDown)
    {
        RotateEnable = isDown;

    }
}

