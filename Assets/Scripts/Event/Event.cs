using System.Collections.Generic;
using UnityEngine;
public enum EventType
{
    Bullet,
    BulletCnt,
}

public static class Events
{
    public static BulletShootingEvent BulletShootingEvent = new BulletShootingEvent();
    public static BulletCntEvent BulletCntEvent = new BulletCntEvent();

    private static Dictionary<EventType, GameEvent> s_eventDictioanry = new Dictionary<EventType, GameEvent>()
    {
        {EventType.Bullet,BulletShootingEvent },
        {EventType.BulletCnt, BulletCntEvent},
    };

    public static GameEvent GetEvent(EventType type)
    {
        return s_eventDictioanry[type];
    }
}

public class BulletCntEvent : GameEvent
{
    public int currentCnt;
    public int maxCnt;

    public override void Setting(params object[] param)
    {
        currentCnt = (int)param[0];
        maxCnt = (int)param[1];
    }
}

public class DamageEvent : GameEvent
{
    public float damage;

    public override void Setting(params object[] param)
    {

    }
}

public class BulletShootingEvent : GameEvent
{
    public Vector3 pos;
    public Vector3 dir;
    public EBullet eBulletType;

    public override void Setting(params object[] param)
    {
        pos = (Vector3)param[0];
        dir = (Vector3)param[1];

        try
        {
            eBulletType = (EBullet)param[2];
        }
        catch
        {
            eBulletType = (EBullet)0;
        }
    }
}


