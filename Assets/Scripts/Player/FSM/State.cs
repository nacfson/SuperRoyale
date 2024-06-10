using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class State<T> : IState
{
    protected StateMachine<T> _machine;
    public State(StateMachine<T> machine)
    {
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public virtual void FixedUpdateState()
    { }
}
