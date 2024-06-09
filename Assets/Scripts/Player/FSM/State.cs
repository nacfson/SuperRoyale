using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    protected StateMachine<T> _machine;
    public State(StateMachine<T> machine)
    {
        _machine = machine;
    }

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
    public virtual void FixedUpdateState()
    { }


}
