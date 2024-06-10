using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> : IInitable
{
    public T Owner { get; private set; }
    private Dictionary<Type,IState> _stateDictionary = new Dictionary<Type,IState>();
    private IState _currentState;

    public StateMachine(T controller)
    {
        Init(controller);
    }

    public void Init(params object[] param)
    {
        if(param[0] is T controller)
        {
            Owner = controller;
        }

        _stateDictionary.Clear();

        _stateDictionary.Add(typeof(PlayerMoveState), new PlayerMoveState(this as StateMachine<PlayerController>));
        _stateDictionary.Add(typeof(PlayerAimState), new PlayerAimState(this as StateMachine<PlayerController>));


        ChangeState(typeof(PlayerMoveState));
    }

    public void UpdateState()
    {
        _currentState?.UpdateState();
    }

    public void FixedUpdateState()
    {
        _currentState?.FixedUpdateState();
    }

    public void ChangeState(Type type)
    {
        _currentState?.ExitState();
        _currentState = _stateDictionary[type];
        _currentState?.EnterState();
    }
}
