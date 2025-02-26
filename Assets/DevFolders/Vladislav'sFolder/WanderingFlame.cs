using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderingFlame : MonoBehaviour, IHealth
{
    [field: Header("Health Stats")]
    public float Health { get; set;  }
    [field: SerializeField]
    public float MaxHealth { get; set;  }
    public event Action OnDeath;
    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
    public void Heal(float heal)
    {
        throw new NotImplementedException();
    }
    
    [field: Header("Range Stats")]
    [field: SerializeField]
    private float Width { get; set; }
    [field: SerializeField]
    private float Height { get; set; }

    private Vector3 destinationPoint;
    private Vector3 centerPoint;
    [field: SerializeField]
    private float Speed { get; set; }
    private bool isMoving = false;  // Индикатор движения
    [SerializeField] private float moveDuration = 2f; // Время движения к цели
    [SerializeField] private float stopDuration = 1f; // Время остановки перед следующим рывком
    private float startTime;
    private Vector3 startPoint;
    
    private void Start()
    {
        centerPoint = transform.parent.position;
        ChangeDestinationPoint();
        isMoving = true;
        startTime = Time.time; // Устанавливаем время начала движения
    }

    private void Update()
    {
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
        yield return new WaitForSeconds(stopDuration);
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

}
