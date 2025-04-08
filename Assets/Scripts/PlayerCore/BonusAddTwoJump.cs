using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BonusAddTwoJump : MonoBehaviour
{
    private DiContainer _container;
    
    [Inject]
    public void Initialize(DiContainer container)
    {
        _container = container;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            var doubleJump = player.gameObject.AddComponent<DoubleJump>();
            _container.Inject(doubleJump);
            Destroy(gameObject);
        }
    }
}