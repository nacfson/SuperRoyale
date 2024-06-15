using System.Collections.Generic;
using UnityEngine;
public enum EventType
{
    Bullet,
    BulletCnt,
    Damage,
}

public static class Events
{
    public static BulletShootingEvent BulletShootingEvent = new BulletShootingEvent();
    public static BulletCntEvent BulletCntEvent           = new BulletCntEvent();
    public static DamageEvent DamageEvent                 = new DamageEvent();

    private static Dictionary<EventType, GameEvent> s_eventDictioanry = new Dictionary<EventType, GameEvent>()
    {
        {EventType.Bullet,BulletShootingEvent },
        {EventType.BulletCnt, BulletCntEvent},
        {EventType.Damage, DamageEvent},
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
    public int actorNumber;
    public int damage;

    public override void Setting(params object[] param)
    {
        actorNumber = (int)param[0];
        damage = (int)param[1];
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


