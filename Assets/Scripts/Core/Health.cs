using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public Action<int> OnDamageEffect;
    public Action<int> OnHealEffect;
    public Action OnDeath;
    
    public int Value { get; private set; }
    public int MaxValue { get; private set; }
    
    public Health(int value)
    { 
        Value = value;
        MaxValue = value;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 ) return;
        
        Value -= damage;
        OnDamageEffect?.Invoke(damage);
        
        if (Value <= 0)
            OnDeath?.Invoke();
    }

    public void Heal(int heal)
    {
        if (heal <= 0) return;
        
        Value += heal;
        OnHealEffect?.Invoke(heal);
        
        if (Value > MaxValue)
            Value = MaxValue;
    }
}
