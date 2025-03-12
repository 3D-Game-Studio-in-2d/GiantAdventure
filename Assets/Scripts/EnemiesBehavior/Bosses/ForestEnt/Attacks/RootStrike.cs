using System;
using UnityEngine;
using Zenject;

public class RootStrike : MonoBehaviour
{
    private int _damage = 5;
    private float _slowdownFactor = 0.5f;
    private float _timeCooldownAttack = 0.5f;
    private float _timeLiveRootStrike = 0.5f;

    private float _timerAttack = -1f;
    private Player _player;
    
    public void Initialize(ForestEntConfig config)
    {
        _damage = config.rootStrikeDamage;
        _slowdownFactor = config.slowdownFactorRootStrike;
        _timeCooldownAttack = config.timeCooldownAttackRootStrike;
        _timeLiveRootStrike = config.timeLiveRootStrike;
    }

    private void Start()
    {
        Invoke(nameof(Death), _timeLiveRootStrike);
    }

    private void Update()
    {
        _timerAttack -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _player = player;
            player.Speed = _player.DefaultSpeed * _slowdownFactor;
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player) && _timerAttack <= 0f)
        {
            player.Health.TakeDamage(_damage);
            _timerAttack = _timeCooldownAttack;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Speed = _player.DefaultSpeed;
        }
    }

    private void Death()
    {
        Destroy(gameObject);
        if (_player)
        {
            _player.Speed = _player.DefaultSpeed;
        }
    }
}