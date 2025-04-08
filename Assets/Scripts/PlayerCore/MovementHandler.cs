using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;

public class MovementHandler
{
        private IMovable _movable;
        private IInput _input;
        private IGravitable _gravitable;
        private IJump _jump;
        private IRoll _roll;
        private ISound _sound;

        private float _rollTimer = 0f;
        
        private PlayerAnimatorController _animatorController;
        public MovementHandler(IInput input, IMovable movable, IGravitable gravitable, IJump jump, IRoll roll,
                ISound sound, PlayerAnimatorController animatorController)
        {
                _input = input;
                _movable = movable;
                _gravitable = gravitable;
                _jump = jump;
                _roll = roll;
                _sound = sound;

                _animatorController = animatorController;
                
                _input.ClickMove += OnMove;
                _input.ClickJump += OnJump;
                _input.ClickRoll += OnRoll;
        }

        private void OnMove(Vector3 moveInput)
        {
                if (!_movable.CharacterController) return;
                _gravitable.IsGrounded = _movable.CharacterController.isGrounded;
                
                UseGravity();
                FaceVector(moveInput);
                _animatorController.Grounded(_gravitable.IsGrounded);
                
                MoveSound(moveInput);
                
                _rollTimer += Time.deltaTime;
                
                if (!_roll.IsRolling)
                {    
                        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.z) * _movable.Speed;
                        
                        _animatorController.Idle(Mathf.Abs(movement.x));
                        
                        _movable.CharacterController.Move(movement * Time.deltaTime);
                }
                
                _movable.CharacterController.Move(_movable.Velocity * Time.deltaTime);
        }

        private void UseGravity()
        {
                if (_gravitable.IsGrounded && _movable.Velocity.y < 0)
                {
                        _movable.Velocity = new Vector3(_movable.Velocity.x, -2f, _movable.Velocity.z);
                }
                
                float verticalVelocity = _movable.Velocity.y + _gravitable.Gravity * Time.deltaTime;
                _movable.Velocity = new Vector3(_movable.Velocity.x, verticalVelocity, _movable.Velocity.z);
        }

        private void FaceVector(Vector3 moveInput)
        {
                if (moveInput.x < 0f)
                {
                        _movable.FacingRight = false;
                }
                else if (moveInput.x > 0f)
                {
                        _movable.FacingRight = true;
                }
        }

        private void OnJump()
        {
                if (_gravitable.IsGrounded)
                {
                        float jumpVector = Mathf.Sqrt(_jump.JumpForce * -2f * _gravitable.Gravity);
                        _movable.Velocity = new Vector3(_movable.Velocity.x, jumpVector, _movable.Velocity.z);
                        
                        if (!_sound.Sound.PlaySfx("jump"))
                        {
                                Debug.Log("Звук отсутствует");
                        }
                        
                        _animatorController.Jump();
                }
        }

        private void OnRoll()
        {
                if (_gravitable.IsGrounded && !_roll.IsRolling && _rollTimer >= _roll.RollCooldown)
                {
                        _roll.IsRolling = true;
                        _animatorController.Roll();
                        
                        Vector3 rollDirection = Vector3.left;
                        
                        if (_movable.FacingRight)
                        {
                                rollDirection = Vector3.right;
                        }
                                
                        
                        _movable.Velocity = rollDirection * _roll.RollSpeed;

                        Task.Run(async () => await EndRoll());

                        _rollTimer = 0f;
                }
        }
        
        private async Task EndRoll()
        {
                await Task.Delay((int)(_roll.RollDuration * 1000));
                
                _movable.Velocity = new Vector3(0, _movable.Velocity.y, 0);
                _roll.IsRolling = false;
        }
        
        private void MoveSound(Vector3 moveInput)
        {
                if (Math.Abs(moveInput.x) < 0.1f || !_movable.CharacterController.isGrounded || _sound == null) return;
                if (!_sound.Sound.PlaySfx("move", allowOverlap: false))
                {
                        Debug.Log("Звук отсутствует");
                }
        }
}