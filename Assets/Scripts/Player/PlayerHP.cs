using UnityEngine;
using UnityEngine.Events;

public class PlayerHP : MonoBehaviour, IDamageable
{
    private int _currentHP;
    public int CurrentHP
    {
        get => _currentHP;
        set
        {
            _currentHP = Mathf.Clamp(value,0,_maxHP);
        }
    }

    [SerializeField] private int _maxHP = 20;
    public UnityEvent OnPlayerDeadEvent;
     
    public void Damage(int damage)
    {
        CurrentHP -= damage;

        if(CurrentHP < 1)
        {
            OnPlayerDeadEvent?.Invoke();
        }
    }
}
