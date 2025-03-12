using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IMovable, IGravitable, IJump, IRoll, ISound
{
    private IRoll rollImplementation;

    [field: Header("Move Stats")]
    public CharacterController CharacterController { get; private set; }
    public Transform Transform { get; set; }
    public float Speed { get; set; } = 5f;
    public float DefaultSpeed { get; private set; } = 5f;
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
    public float RollCooldown { get; set; } = 1f;

    public bool IsRolling { get; set; } = false;

    [field: Header("Attack Stats")]
    public AttackPlayerStats AttackPlayerStats{ get; private set; }
    
    [field: Header("Health Stats")]
    public Health Health { get; private set; }
    
    public Sound Sound { get; set; }

    [Inject]
    public void Initialize(PlayerConfig config, AttackPlayerStats stats)
    {
        // Movable
        Speed = config.Speed;
        DefaultSpeed = Speed;
        
        // Gravity
        Gravity = config.Gravity;
        
        // Jump
        JumpForce = config.JumpForce;
        
        // Roll
        RollSpeed = config.RollSpeed;
        RollDuration = config.RollDuration;
        RollCooldown = config.RollCooldown;
        
        AttackPlayerStats = stats;
        
        InitializeHealth(config.Health);
    }
    
    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Sound = GetComponent<Sound>();
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