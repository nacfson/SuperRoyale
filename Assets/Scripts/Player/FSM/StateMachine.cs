using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> : IInitable
{
    public T Owner { get; private set; }
    private Dictionary<Type,State<T>> _stateDictionary = new Dictionary<Type,State<T>>();
    private State<T> _currentState;

    public void Init(params object[] param)
    {
        _stateDictionary.Clear();

        _stateDictionary.Add(typeof(State<T>),new PlayerMoveState(this as StateMachine<PlayerController>));
    }

    public void UpdateState()
    {

    }

    public void FixedUpdateState()
    {

    }

    public void ChangeState(Type type)
    {
        _currentState?.ExitState();
        _currentState = _stateDictionary[type];
        _currentState?.EnterState();
    }
}
