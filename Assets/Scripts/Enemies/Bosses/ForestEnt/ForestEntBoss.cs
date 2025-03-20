using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class ForestEntBoss : Enemy
{
    [SerializeField] private RootStrikeSpawner meleeRootStrike;
    [SerializeField] private RootStrikeSpawner distanceRootStrike; 
    
    private ForestEntAttackType _forestEntAttackType = ForestEntAttackType.MeleeRootStrike;
    private Player _player;
    private float _rangedAttackMeleeStrike = 1f;
    private float _rangedAttackMeleeRootStrike = 2f;
    private float _rangedAttackDistanceRootStrike = 5f;
    private float _attackCooldown = 3f;
    private float _speed = 3f;
    private ForestEntConfig _config;
    private int _damageMeleeStrike = 5;
    
    private CharacterController _controller;
    
    private float _attackTimer;
    
    [SerializeField]
    private LayerMask groundLayer;
    
    [Inject]
    public void Initialize(Player player, ForestEntConfig config)
    {
        _player = player;
        
        _config = config;
        
        _rangedAttackMeleeStrike = _config.rangedAttackMeleeStrike;
        _rangedAttackMeleeRootStrike = _config.rangedAttackMeleeRootStrike;
        _rangedAttackDistanceRootStrike = _config.rangedAttackDistanceRootStrike;
        _attackCooldown = _config.attackCooldown;
        _damageMeleeStrike = _config.damageMeleeStrike;
        _speed = _config.speed;

        _attackTimer = _attackCooldown;
        
        Health = new Health(config.health);
        Health.OnDeath += OnDeath;
    }

    protected override void Start()
    {
        base.Start();
        
        _controller = GetComponent<CharacterController>();
    }
    
    protected override void Update()
    {
        base.Update();
        
        _attackTimer -= Time.deltaTime;

        Move();
        TypeAttack();
        OnAttack();
    }

    private void Move()
    {
        if (_player == null) return;

        var inputVector = _player.transform.position - transform.position;
        
        _controller.Move(inputVector * _speed * Time.deltaTime);
    }
    
    private void TypeAttack()
    {
        if (_player == null) return;
        
        float distance = Vector3.Distance(_player.transform.position, transform.position);
        
        if (distance < _rangedAttackMeleeStrike)
        {
            _forestEntAttackType = ForestEntAttackType.MeleeStrike;
        }
        else if (distance < _rangedAttackMeleeRootStrike)
        {
            _forestEntAttackType = ForestEntAttackType.MeleeRootStrike;
        }
        else if (distance < _rangedAttackDistanceRootStrike)
        {
            _forestEntAttackType = ForestEntAttackType.DistanceRootStrike;
        }
        else
        {
            _forestEntAttackType = ForestEntAttackType.SkipAttack;
        }
    }

    private void OnAttack()
    {
        if (_player == null || _attackTimer > 0f) return;
        
        switch (_forestEntAttackType)
        {
            case ForestEntAttackType.MeleeStrike:
                MeleeStrike();
                _attackTimer = _attackCooldown;
                break;
            case ForestEntAttackType.MeleeRootStrike:
                MeleeRootStrike();
                _attackTimer = _attackCooldown;
                break;
            case ForestEntAttackType.DistanceRootStrike:
                DistanceRootStrike();
                _attackTimer = _attackCooldown;
                break;
            case ForestEntAttackType.SkipAttack:
                break;
            default:
                break;
        }
    }

    private void MeleeStrike()
    {
        _player.Health.TakeDamage(_damageMeleeStrike);
    }

    private void MeleeRootStrike()
    {
        SpawnRootStrike(meleeRootStrike, transform.position);
    }

    private void DistanceRootStrike()
    {
        SpawnRootStrike(distanceRootStrike, _player.transform.position);
    }

    private void SpawnRootStrike(RootStrikeSpawner rootStrike, Vector3 position)
    {
        if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            var rootStrikeInstantiate = Instantiate(rootStrike, hit.point, Quaternion.identity);
            rootStrikeInstantiate.Initialize(_config);
        }
        else
        {
            Debug.LogWarning("Не удалось найти землю для спавна RootStrike");
        }
    }

    protected override void OnDeath()
    {
        Destroy(gameObject);
    }
}