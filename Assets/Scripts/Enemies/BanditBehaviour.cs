using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class BanditBehaviour : Enemy, IGravitable, IMovable
{
    [field: Header("Range Stats")]
    [field: SerializeField] private float radiusOfVision = 15f;
    [field: SerializeField] private float backVision = 2f;

    [field: Header("Attack stats")]
    [field: SerializeField] private int AttackDamage { get; set; } = 5;
    [field: SerializeField] private float AttackCooldown { get; set; } = 1f;
    [field: SerializeField] private float AttackRange { get; set; } = 1.5f;
    [field: SerializeField] private float PreAttackDelay { get; set; } = 0.25f;

    [field: Header("Movement stats")]
    [field: SerializeField] public float Speed { get; set; } = 3f;
    [field: SerializeField] public float WalkSpeed { get; set; } = 1f;
    [field: SerializeField] private Vector2 stopDuration;
    [field: SerializeField] public float Gravity { get; set; } = -10f;

    public bool IsGrounded { get; set; }
    public bool FacingRight { get; set; }
    public Vector3 Velocity { get; set; }
    public Transform Transform { get; set; }
    public CharacterController CharacterController { get; set; }

    [SerializeField] private bool isChasing = false;
    [SerializeField] private bool isStopped = false;

    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    private Vector3 destinationPoint;
    [SerializeField] private float stopRange = 0.2f;
    [SerializeField] private float walkStopRange = 0.2f;
    [SerializeField] private float attackStopRange = 1f;

    [Inject]
    public void Initialize(BanditConfig config)
    {
        Health = new Health(config.Health);
        Health.OnDeath += OnDeath;
        
        radiusOfVision = config.RadiusOfVision;
        backVision = config.BackVision;
        walkStopRange = config.WalkStopRange;
        attackStopRange = config.AttackStopRange;

        AttackDamage = config.AttackDamage;
        AttackCooldown = config.AttackCooldown;
        AttackRange = config.AttackRange;
        PreAttackDelay = config.PreAttackDelay;
        
        Speed = config.Speed;
        WalkSpeed = config.WalkSpeed;
        stopDuration = config.StopDurationRange;
        Gravity = config.Gravity;
    }

    protected override void Start()
    {
        base.Start();
        CharacterController = GetComponent<CharacterController>();
        isReadyToAttack = true;
        destinationPoint = Random.Range(0, 2) == 1 ? leftPoint.position : rightPoint.position;
    }

    protected override void Update()
    {
        base.Update();
        if (!isStopped)
        {
            CheckPlayer();
            Move();
        }
        else
        {
            Velocity = new Vector3(0f, Velocity.y, Velocity.z);
        }
        ApplyGravity();
        CharacterController.Move(Velocity * Time.deltaTime);
    }

    private void CheckPlayer()
    {
        float widthOfVision = backVision + radiusOfVision;
        Vector3 direction = (destinationPoint - transform.position).normalized;
        Vector3 center = transform.position + direction * (radiusOfVision / 2f) - direction * (backVision / 2);
        Vector3 halfExtents = new Vector3(widthOfVision / 2f, widthOfVision / 2, widthOfVision / 2);
        Collider[] colliders = Physics.OverlapBox(center, halfExtents);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                isChasing = true;
                destinationPoint = collider.transform.position;
                stopRange = attackStopRange;
                return;
            }
        }

        if (isChasing)
        {
            SetNearestDestinationPoint();
            stopRange = walkStopRange;
        }
        isChasing = false;
    }

    private void ApplyGravity()
    {
        if (IsGrounded && Velocity.y < 0)
        {
            Velocity = new Vector3(0, -2f, Velocity.z);
        }

        float verticalVelocity = Velocity.y + Gravity * Time.deltaTime;
        Velocity = new Vector3(Velocity.x, verticalVelocity, Velocity.z);
    }

    private void Move()
    {
        if (Mathf.Abs(destinationPoint.x - transform.position.x) > stopRange)
        {
            Vector3 direction = (destinationPoint - transform.position).normalized;
            float speed = isChasing ? Speed : WalkSpeed;
            Velocity = new Vector3(direction.x * speed, Velocity.y, Velocity.z);
        }
        else if (isChasing)
        {
            isStopped = true;
            Invoke("Attack", PreAttackDelay);
        }
        else
        {
            Stop(Random.Range(stopDuration.x, stopDuration.y));
            destinationPoint = destinationPoint == leftPoint.position ? rightPoint.position : leftPoint.position;
        }
    }
    
    private void Stop(float time)
    {
        isStopped = true;
        StartCoroutine(StopFor(time));
    }
    
    private IEnumerator StopFor(float duration)
    {
        Velocity = Vector3.zero;
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }

    private void SetNearestDestinationPoint()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftPoint.position);
        float distanceToRight = Vector3.Distance(transform.position, rightPoint.position);
        destinationPoint = distanceToLeft < distanceToRight ? leftPoint.position : rightPoint.position;
    }

    private void Attack()
    {
        if (!isReadyToAttack) return;
        Vector3 direction = (destinationPoint - transform.position).normalized;
        Vector3 center = transform.position + direction * (AttackRange / 2);
        Vector3 halfExtents = new Vector3(AttackRange / 2, AttackRange / 2, AttackRange / 2);
        Collider[] colliders = Physics.OverlapBox(center, halfExtents);
        
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (collider.TryGetComponent(out Player player))
                {
                    player.Health.TakeDamage(AttackDamage);
                    isReadyToAttack = false;
                }
            }
        }
        
        StartCoroutine(AttackCooldownCoroutine());
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(AttackCooldown);
        isReadyToAttack = true;
        isStopped = false;
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(destinationPoint, 0.2f);
        
        Gizmos.color = Color.yellow;
        float widthOfVision = backVision + radiusOfVision;
        Vector3 direction = (destinationPoint - transform.position).normalized;
        Vector3 center = transform.position + direction * (radiusOfVision / 2f) - direction * (backVision / 2);
        Vector3 halfExtents = new Vector3(widthOfVision / 2f, widthOfVision / 2, widthOfVision / 2);
        Gizmos.DrawWireCube(center, halfExtents * 2);
        
        Gizmos.color = Color.red;
        center = transform.position + direction * (AttackRange / 2) + transform.up * transform.localScale.y / 2;
        halfExtents = new Vector3(AttackRange / 2, AttackRange / 2, AttackRange / 2);
        Gizmos.DrawWireCube(center, halfExtents * 2);
    }
}