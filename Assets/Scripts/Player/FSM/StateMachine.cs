using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : IInitable
{
    public PlayerController Owner { get; private set; }
    private Dictionary<Type,IState> _stateDictionary = new Dictionary<Type,IState>();
    private IState _currentState;

    public StateMachine(PlayerController controller)
    {
        Init(controller);
    }

    public void Init(params object[] param)
    {
        if(param[0] is PlayerController controller)
        {
            Owner = controller;
        }

        _stateDictionary.Clear();

        _stateDictionary.Add(typeof(PlayerMoveState), new PlayerMoveState(this));
        _stateDictionary.Add(typeof(PlayerAimState), new PlayerAimState(this));
        _stateDictionary.Add(typeof(PlayerReloadState), new PlayerReloadState(this));

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
