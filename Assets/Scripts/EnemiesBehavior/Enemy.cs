
using System.Collections;
using UnityEngine;

public abstract class Enemy : Entity
{
    private int AttackDamage;
    private float AttackCooldown;
    public bool isReadyToAttack;
    
    public IEnumerator AttackCooldownTimer(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        isReadyToAttack = true;
    }
}
