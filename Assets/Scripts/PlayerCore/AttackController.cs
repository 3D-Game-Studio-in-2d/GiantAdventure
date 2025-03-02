using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class AttackController : MonoBehaviour
{
    private IInput _input;
    private IMovable _movable;
    private IAttackable _attackable;
    private float _previousSpeed;
    
    [Inject]
    public void Initialize(IInput input, IMovable movable, IAttackable attackable)
    {
        _input = input;
        _movable = movable;
        _attackable = attackable;

        _input.ClickAttack += OnAttack;
    }

    private void OnAttack()
    {            
        Debug.Log("Хочет атаковать");
        if (_attackable.IsAttacking) return;
        Debug.Log("Атакует");
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        _attackable.IsAttacking = true;
        _previousSpeed = _movable.Speed;
        _movable.Speed *= _attackable.AttackSlowdown;
        float timer = 0;
        while (timer <= _attackable.AttackDuration)
        {
            var playerPosition = _movable.Transform;
        
            Vector3 boxCenter = CorrectBoxCenter();
            Collider[] colliders = 
                Physics.OverlapBox(playerPosition.position + boxCenter, _attackable.BoxSize / 2);

            foreach (var collider in colliders)
            {
                yield return new NotImplementedException("Attack is not implemented");
            }
            
            yield return null;
            
            timer += Time.deltaTime;
        }
        
        _attackable.IsAttacking = false;
        _movable.Speed = _previousSpeed;
    }

    private Vector3 CorrectBoxCenter()
    {
        Vector3 result = _attackable.BoxCenter;
        
        if (!_movable.FacingRight)
        {
            result =  new Vector3(_attackable.BoxCenter.x * -1, _attackable.BoxCenter.y, _attackable.BoxCenter.z);
        }
        
        return result;
    }
    
    private void OnDrawGizmos()
    {
        if (_movable == null) return;
        Gizmos.color = Color.red;
        Vector3 boxCenter = CorrectBoxCenter();
        Gizmos.DrawWireCube(transform.position + boxCenter, _attackable.BoxSize);
    }
}