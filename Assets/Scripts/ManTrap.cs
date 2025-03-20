using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class ManTrap : MonoBehaviour
{
    private float _speedReduction = 0.5f;
    private float _speedReductionTime = 2f;
    private int _damage;

    private bool _isActive = false;
    private float _previsionSpeed;
    
    private Animator _animator;

    [Inject]
    public void Initialize(TrapsConfig config)
    {
        _speedReduction = config.manTrapSpeedReduction;
        _speedReductionTime = config.manTrapSpeedReductionTime;
        _damage = config.manTrapDamage;
    }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isActive) return;
        
        if (other.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(ActivateManTrap(player));
        }
    }

    private IEnumerator ActivateManTrap(Player player)
    {
        _previsionSpeed = player.Speed;
        player.Speed *= _speedReduction;
        player.Health.TakeDamage(_damage);
        
        _isActive = true;
        
        _animator?.SetTrigger("ActivateManTrap");
        
        yield return new WaitForSeconds(_speedReductionTime);

        player.Speed = _previsionSpeed;
    }
}