using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerModule : MonoBehaviour, IInitable
{
    protected PlayerController _playerController;
    public virtual void Init(params object[] param)
    {
        if(param.Length > 0)
        {
            if (param[0] is PlayerController controller)
            {
                _playerController = controller;
            }
        }

        if (_playerController == null) Debug.LogError($"Controller is null! : param length: {param.Length}");
    }
}
