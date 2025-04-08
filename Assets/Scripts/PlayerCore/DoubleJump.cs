using System;
using UnityEngine;
using Zenject;

public class DoubleJump : MonoBehaviour
{
    private IInput _input;
    private IMovable _movable;
    private IGravitable _gravitable;
    private IJump _jump;
    private PlayerAnimatorController _animatorController;
    
    private bool _isTwoJumping;
    
    [Inject]
    public void Initialize(IInput input, IMovable movable, IGravitable gravitable, IJump jump,
        PlayerAnimatorController animatorController)
    {
        _input = input;
        _movable = movable;
        _gravitable = gravitable;
        _jump = jump;
        
        _animatorController = animatorController;
        
        _input.ClickJump += OnDoubleJump;
    }

    private void Update()
    {
        if (_movable.CharacterController.isGrounded)
        {
            _isTwoJumping = false;
        }
    }

    private void OnDoubleJump()
    {
        if (!_movable.CharacterController.isGrounded && !_isTwoJumping)
        {
            _isTwoJumping = true;
            float jumpVector = Mathf.Sqrt(_jump.JumpForce * -2f * _gravitable.Gravity);
            _movable.Velocity = new Vector3(_movable.Velocity.x, jumpVector, _movable.Velocity.z);
                        
            _animatorController.DoubleJump();
        }
    }
}