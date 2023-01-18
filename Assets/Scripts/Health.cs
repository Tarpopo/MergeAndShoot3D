using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Health
{
    public bool IsDeath => _currentHealth <= 0;
    public int CurrentHealth => _currentHealth;
    [SerializeField] protected int _health;
    [SerializeField] private UnityEvent _onTakeDamage;

    public event UnityAction OnTakeDamage
    {
        add => _onTakeDamage.AddListener(value);
        remove => _onTakeDamage.RemoveListener(value);
    }

    private int _currentHealth;

    public virtual void ReduceHealth(int value, Action onDead)
    {
        if (IsDeath || value <= 0) return;
        _currentHealth -= value;
        if (IsDeath) onDead?.Invoke();
        _onTakeDamage?.Invoke();
    }

    public virtual void ResetHealth() => _currentHealth = _health;
}