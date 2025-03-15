using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AttackController : MonoBehaviour
{
    private IInput _input;
    private IMovable _movable;
    private AttackPlayerStats _attackPlayerStats;
    private float _previousSpeed;

    private Strike[] _comboStrikes;
    private int _currentComboIndex = 0;
    private float _comboTimer = 0f;
    private bool _canContinueCombo = false;
    private bool _isAttacking = false;

    private List<Entity> _attackedEntities = new List<Entity>();
    [Inject]
    public void Initialize(IInput input, IMovable movable, AttackPlayerStats attackPlayerStats)
    {
        _input = input;
        _movable = movable;
        _attackPlayerStats = attackPlayerStats;

        _comboStrikes = new [] { _attackPlayerStats.First, _attackPlayerStats.Second, _attackPlayerStats.Third };

        _input.ClickAttack += OnAttack;
    }

    private void Update()
    {
        if (_comboTimer > 0)
        {
            _comboTimer -= Time.deltaTime;
        }
        else if (!_isAttacking)
        {
            ResetCombo();
        }
    }

    private void ResetCombo()
    {
        _currentComboIndex = 0;
        _canContinueCombo = false;
        _isAttacking = false;
    }
    
    private void OnAttack()
    {
        if (_isAttacking) 
        {
            _canContinueCombo = true;
            return;
        }

        if (_currentComboIndex < _comboStrikes.Length) 
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        _canContinueCombo = false;

        Strike currentStrike = _comboStrikes[_currentComboIndex];
        _comboTimer = currentStrike.AttackDuration;

        _previousSpeed = _movable.Speed;
        _movable.Speed *= _attackPlayerStats.AttackSlowdown;
        
        var playerPosition = _movable.Transform;
        Vector3 boxCenter = CorrectBoxCenter(currentStrike.BoxCenter);
        Collider[] colliders = Physics.OverlapBox(playerPosition.position + boxCenter, currentStrike.BoxSize / 2);

        foreach (var collider in colliders)
        {
            CurrentCollision(collider);
        }
        
        yield return new WaitForSeconds(currentStrike.AttackDuration);
        
        /*float timer = 0;
        while (timer <= currentStrike.AttackDuration)
        {
            var playerPosition = _movable.Transform;
            Vector3 boxCenter = CorrectBoxCenter(currentStrike.BoxCenter);
            Collider[] colliders = Physics.OverlapBox(playerPosition.position + boxCenter, currentStrike.BoxSize / 2);

            foreach (var collider in colliders)
            {
                CurrentCollision(collider);
            }

            yield return null;
            timer += Time.deltaTime;
        }*/

        _movable.Speed = _previousSpeed;
        _attackedEntities.Clear();
        
        if (_canContinueCombo && _currentComboIndex < _comboStrikes.Length - 1)
        {
            _currentComboIndex++;
            StartCoroutine(Attack());
        }
        else
        {
            ResetCombo();
        }
    }
    
    private Vector3 CorrectBoxCenter(Vector3 boxCenter)
    {
        return _movable.FacingRight ? boxCenter : new Vector3(-boxCenter.x, boxCenter.y, boxCenter.z);
    }
    
    private void OnDrawGizmos()
    {
        if (_movable == null || _comboStrikes == null || _currentComboIndex >= _comboStrikes.Length) return;

        Gizmos.color = Color.Lerp(Color.blue, Color.red, (float)_currentComboIndex / (_comboStrikes.Length - 1));
        Vector3 boxCenter = CorrectBoxCenter(_comboStrikes[_currentComboIndex].BoxCenter);
        Gizmos.DrawWireCube(transform.position + boxCenter, _comboStrikes[_currentComboIndex].BoxSize);
    }

    private void CurrentCollision(Collider other)
    {
        if (other == null)
        {
            Debug.LogError("❌ Ошибка! Переданный `Collider other` = null");
            return;
        }

        if (!other.TryGetComponent(out Entity entity))
        {
            return;
        }

        if (entity.Health == null)
        {
            Debug.LogError($"❌ Ошибка! У объекта {entity.name} отсутствует компонент Health.");
            return;
        }
        
        if (!_attackedEntities.Contains(entity))
        {
            entity.Health.TakeDamage(_comboStrikes[_currentComboIndex].Damage);
            _attackedEntities.Add(entity);
        }
    }
}