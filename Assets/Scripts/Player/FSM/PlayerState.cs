using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    public Transform Transform => _machine.Owner.transform;
    protected InputReader _inputReader;

    public PlayerState(StateMachine machine) : base(machine)
    {
        _machine = machine;
        _inputReader = _machine.Owner.InputReader;
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}
