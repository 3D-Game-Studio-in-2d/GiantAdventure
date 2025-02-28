using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float Health { get; set;  }
    float MaxHealth { get; set;  }

    event Action OnDeath;

    void TakeDamage(float damage);
    void Heal(float heal);
}
