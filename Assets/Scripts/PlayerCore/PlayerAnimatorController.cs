using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private readonly string idle = "speed";
    private readonly string move = "move";
    
    private readonly string grounded = "grounded"; 
    private readonly string jump = "jump"; 
    private readonly string roll = "roll"; 
    
    private readonly string mediate = "mediate"; 
    private readonly string prizem = "prizem"; 
    
    private readonly string attack = "attack";

    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Idle(float speed)
    {
        _animator.SetFloat(idle, speed);
    }

    public void Roll()
    {
        _animator.SetTrigger(roll);
    }
    
    public void Grounded(bool isGrounded)
    {
        _animator.SetBool(grounded, isGrounded);
    }
    
    public void Jump()
    {
        _animator.SetTrigger(jump);
    }
    
    public void Attack(bool isAttack)
    {
        _animator.SetBool(attack, isAttack);
    }

    public void DoubleJump()
    {
       // Анимация прыжка в воздухе
    }
}
