using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;
using Random = UnityEngine.Random;

public class WanderingFlame : Enemy
{
    [field: Header("Range Stats")]
    [field: SerializeField]
    private float Width { get; set; }
    [field: SerializeField]
    private float Height { get; set; }

    [field: Header("Attack stats")]
    [field: SerializeField]
    private int AttackDamage { get; set; } = 5;
    [field: SerializeField]
    private float AttackCooldown { get; set; } = 1f;
    
    [field: Header("Movement behavior")]
    [field: SerializeField]
    private float Speed { get; set; }
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float stopDuration = 1f;
    
    private float startTime;
    private bool isMoving = false;  // Индикатор движения
    
    private Vector3 startPoint;
    private Vector3 destinationPoint;
    private Vector3 centerPoint;

    [Inject]
    public void Initialize(WanderingFlameConfig config)
    {
        Health = new Health(config.Health);
        Health.OnDeath += OnDeath;
    }
    
    protected override void Start()
    {
        base.Start();
        centerPoint = transform.parent.position;
        ChangeDestinationPoint();
        isMoving = true;
        startTime = Time.time; // Устанавливаем время начала движения
        isReadyToAttack = true;
        
        
    }

    protected override void Update()
    {
        base.Update();
        if (isMoving)
        {
            float elapsedTime = (Time.time - startTime)/moveDuration;

            transform.position = new Vector3(Mathf.SmoothStep(startPoint.x, destinationPoint.x, elapsedTime), Mathf.SmoothStep(startPoint.y, destinationPoint.y, elapsedTime), 0);
            //transform.position = Vector3.Lerp(startPoint, destinationPoint, elapsedTime);

            if (Vector3.Distance(transform.position, destinationPoint) <= 0.1f)
            {
                isMoving = false;
                StartCoroutine(WaitBeforeNextMove());
            }
        }
    }

    private IEnumerator WaitBeforeNextMove()
    {
        // Останавливаемся на некоторое время перед новым движением
        yield return new WaitForSeconds(stopDuration + Random.Range(-0.2f, 0.2f));
        ChangeDestinationPoint();
        float journeyLength = Vector3.Distance(startPoint, destinationPoint);
        moveDuration = 1 / Speed * journeyLength;
        isMoving = true;
        startTime = Time.time; // Запоминаем время начала следующего движения
    }

    private void ChangeDestinationPoint()
    {
        startPoint = transform.position;
        destinationPoint = centerPoint + new Vector3(Random.Range(-Width / 2, Width / 2), Random.Range(-Height / 2, Height / 2), 0);
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
    
        // Определяем координаты углов рамки
        Vector3 topLeft = transform.parent.position + new Vector3(-Width / 2, Height / 2, 0);
        Vector3 topRight = transform.parent.position + new Vector3(Width / 2, Height / 2, 0);
        Vector3 bottomLeft = transform.parent.position + new Vector3(-Width / 2, -Height / 2, 0);
        Vector3 bottomRight = transform.parent.position + new Vector3(Width / 2, -Height / 2, 0);

        // Рисуем рамку
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
        
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere(destinationPoint, 0.1f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isReadyToAttack && other.transform.parent.TryGetComponent(out Player player)) // Проверяем, есть ли у объекта компонент Player
        {
            player.Health.TakeDamage(AttackDamage); // Доступ к полю Health
            isReadyToAttack = false;
            StartCoroutine(AttackCooldownTimer(AttackCooldown));
        }
    }

    [ContextMenu("Тестирование Получения Урона 10")]
    private void ТестированиеПолученияУрона()
    {
        Health.TakeDamage(10);
    }
}
