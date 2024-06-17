using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHPModule : PlayerModule
{
    private int _currentHP;
    public int CurrentHP
    {
        get => _currentHP;
        set
        {
            _currentHP = Mathf.Clamp(value, 0, _maxHP);
        }
    }

    [SerializeField] private int _maxHP = 20;

    public UnityEvent OnPlayerDeadEvent;

    public override void Init(params object[] param)
    {
        base.Init(param);

        CurrentHP = _maxHP;
        EventManager.AddListener<DamageEvent>(Damage);
    }

    private void Damage(DamageEvent damageEvent)
    {
        if (damageEvent.actorNumber.Equals(_owner.ActorNumber) == false) return;

        CurrentHP -= damageEvent.damage;

        if (CurrentHP < 1)
        {
            OnPlayerDeadEvent?.Invoke();
        }
    }
}
