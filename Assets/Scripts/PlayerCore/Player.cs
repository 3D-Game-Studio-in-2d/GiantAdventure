using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IMovable, IGravitable, IJump, IRoll, IAttackable
{
    [field: Header("Move Stats")]
    public CharacterController CharacterController { get; private set; }
    public Transform Transform { get; set; }
    public float Speed { get; set; } = 5f;
    public bool FacingRight { get; set; } = true;
    public Vector3 Velocity { get; set; } = Vector3.zero;

    [field: Header("Gravity Stats")]
    public float Gravity { get; set; } = 10f;
    public bool IsGrounded { get; set; } = false;

    [field: Header("Jump Stats")]
    public float JumpForce { get; set; } = 10f;

    [field: Header("Roll Stats")]
    public float RollSpeed { get; set; } = 10f;
    public float RollDuration { get; set; } = 1f;
    public bool IsRolling { get; set; } = false;

    [field: Header("Attack Stats")]
    public float Damage { get; set; } = 1f;

    public float AttackDuration { get; set; } = 0.3f;
    public float AttackSlowdown { get; set; } = 0.5f;
    public bool IsAttacking { get; set; } = false;

    [field: Header("Radius Attack Stats")]
    public Vector3 BoxCenter { get; set; }
    public Vector3 BoxSize { get; set; }
    [field: Header("Radius Attack Stats")]
    public Health Health { get; set; }

    [Inject]
    public void Initialize(PlayerConfig config)
    {
        // Movable
        Speed = config.Speed;
        
        // Gravity
        Gravity = config.Gravity;
        
        // Jump
        JumpForce = config.JumpForce;
        
        // Roll
        RollSpeed = config.RollSpeed;
        RollDuration = config.RollDuration;
        
        // Attack
        Damage = config.Damage;
        AttackDuration = config.AttackDuration;
        AttackSlowdown = config.AttackSlowdown;
        
        // Radius Attack
        BoxCenter = config.BoxCenter;
        BoxSize = config.BoxSize;
        
        InitializeHealth(config.Health);
        
        Debug.Log("Игрок создан");
    }
    
    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Transform = transform;
    }

    private void InitializeHealth(int health)
    {
        Health = new Health(health);
        Health.OnDeath += OnDied;
    }
    
    private void OnDied()
    {
        // Instantiate(частицы, transform.position, Quat);
        // PlayAnim();
        Destroy(gameObject);
    }
    
    [ContextMenu("Тестирование Получения Урона 10")]
    private void ТестированиеПолученияУрона()
    {
        Health.TakeDamage(10);
    }
}