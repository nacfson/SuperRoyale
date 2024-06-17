using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEventSetting
{
    public void Setting(params object[] param);
}

public abstract class GameEvent : IEventSetting
{
    public abstract void Setting(params object[] param);
}

public static class EventManager
{
    private static readonly Dictionary<Type, Action<GameEvent>> s_Events = new Dictionary<Type, Action<GameEvent>>();

    private static readonly Dictionary<Delegate, Action<GameEvent>> s_EventLookups =
        new Dictionary<Delegate, Action<GameEvent>>();

    public static void AddListener<T>(Action<T> evt) where T : GameEvent
    {
        if (!s_EventLookups.ContainsKey(evt))
        {
            //T로 캐스팅해주는 Action을 만들어서 넣는다. 이를 통해 람다를 넣지 않아도 되도록
            Action<GameEvent> newAction = (e) => evt((T)e);
            s_EventLookups[evt] = newAction;

            if (s_Events.TryGetValue(typeof(T), out Action<GameEvent> internalAction))
                s_Events[typeof(T)] = internalAction += newAction;
            else
                s_Events[typeof(T)] = newAction;

        }

        Debug.LogError("AddListener");
    }

    public static void RemoveListener<T>(Action<T> evt) where T : GameEvent
    {
        if (s_EventLookups.TryGetValue(evt, out var action))
        {
            if (s_Events.TryGetValue(typeof(T), out var tempAction))
            {
                tempAction -= action;
                if (tempAction == null)
                    s_Events.Remove(typeof(T));
                else
                    s_Events[typeof(T)] = tempAction;
            }

            s_EventLookups.Remove(evt);
        }
    }

    public static void Broadcast(GameEvent evt)
    {
        if (s_Events.TryGetValue(evt.GetType(), out var action))
            action.Invoke(evt);

        Debug.Log($"BroadCast:{evt}");
    }

    public static void Clear()
    {
        s_Events.Clear();
        s_EventLookups.Clear();
    }
}