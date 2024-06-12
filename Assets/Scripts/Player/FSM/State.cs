using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class State : IState
{
    protected StateMachine _machine;
    public State(StateMachine machine)
    {
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public virtual void FixedUpdateState()
    { }
}
