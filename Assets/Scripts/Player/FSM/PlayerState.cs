using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : State<PlayerController>
{
    public Transform Transform => _machine.Owner.transform;
    protected InputReader _inputReader;
    protected PlayerState(StateMachine<PlayerController> machine) : base(machine)
    {
        _inputReader = _machine.Owner.InputReader;
    }
}
