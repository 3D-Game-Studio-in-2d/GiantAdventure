using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IMovable, IGravitable, IJump, IRoll
{
    public float Speed { get; set; } = 5f;
    public float Gravity { get; set; } = 10f;
    public float JumpForce { get; set; } = 10f;
    public float RollSpeed { get; set; } = 10f;
    public float RollDuration { get; set; } = 1f;
    public bool IsRolling { get; set; } = false;

    public bool IsGrounded { get; set; } = false;
    public bool FacingRight { get; set; } = true;
    public Vector3 Velocity { get; set; } = Vector3.zero;
    public CharacterController CharacterController { get; private set; }

    [Inject]
    public void Initialize(PlayerConfig config)
    {
        Speed = config.Speed;
        Gravity = config.Gravity;
        JumpForce = config.JumpForce;
        RollSpeed = config.RollSpeed;
        RollDuration = config.RollDuration;
        Debug.Log("Игрок создан");
    }
    
    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Debug.Log("Игрок Создан");
    }

}