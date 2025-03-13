using System.Collections;
using UnityEngine;
using Zenject;

public abstract class Enemy : Entity
{
    private int AttackDamage;
    private float AttackCooldown;
    public bool isReadyToAttack;
    
    [SerializeField] private Vector3 offsetHealthUI = Vector3.up * 3;
    private HealthViewFactory _healthViewFactory;
    private HealthView _healthView;
    
    [Inject]
    public void Initialize(HealthViewFactory healthViewFactory)
    {
        _healthViewFactory = healthViewFactory;
    }

    protected virtual void Start()
    {
        _healthView = _healthViewFactory.Create();

        _healthView.Initialize(Health);
    }

    protected virtual void Update()
    {
        if (_healthView != null)
            _healthView.transform.position = transform.position + offsetHealthUI;
    }
    
    public IEnumerator AttackCooldownTimer(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        isReadyToAttack = true;
    }
}
