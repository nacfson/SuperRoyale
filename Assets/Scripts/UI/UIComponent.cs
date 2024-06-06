using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIComponent : MonoBehaviour, IInitable
{
    public abstract void Init(params object[] param);

    public abstract void Appear(Action Callback = null);
    public abstract void Disappear(Action Callback = null);
}
