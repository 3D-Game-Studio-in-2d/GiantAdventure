using System.Collections;
using UnityEngine;
using Zenject;

public class SlimeBehavior : Enemy, IJump, IGravitable, IMovable
{
    [field: Header("Range Stats")]
    [field: SerializeField] private float radiusOfVision = 5f;

    [field: Header("Attack stats")]
    [field: SerializeField] private int AttackDamage { get; set; } = 5;
    [field: SerializeField] 
    private float AttackCooldown { get; set; } = 1f;

    [field: Header("Movement behavior")]
    [field: SerializeField] public float JumpForce { get; set; } = 5f;
    [field: SerializeField] private float stopDuration = 1f;
    [field: SerializeField] public float Gravity { get; set; } = -10f;
    public bool IsGrounded { get; set; }
    [field: SerializeField] public float Speed { get; set; } = 3f;
    public bool FacingRight { get; set; }
    public Vector3 Velocity { get; set; }
    public Transform Transform { get; set; }
    public CharacterController CharacterController { get; set; }

    private bool isReadyToJump;
    private int sideOfJump; // 1 - вправо, -1 - влево

    [Inject]
    public void Initialize(SlimeConfig config)
    {
        Health = new Health(config.Health);
        Health.OnDeath += OnDeath;
    }
    
    protected override void Start()
    {
        base.Start();
        CharacterController = GetComponent<CharacterController>();

        isReadyToAttack = true;
        isReadyToJump = false;

        // Выбираем начальное направление и начинаем прыжки
        ChangeJumpSide();
        Invoke(nameof(PrepareForJump), stopDuration + Random.Range(-1f, 2f));
    }

    protected override void Update()
    {
        if (CharacterController == null) return;
        base.Update();
        IsGrounded = CharacterController.isGrounded;
        
        ApplyGravity();
        CharacterController.Move(Velocity * Time.deltaTime);
    }

    private void PrepareForJump()
    {
        isReadyToJump = true;
        ChangeJumpSide();
        Jump();
    }

    private void Jump()
    {
        if (!isReadyToJump) return;

        float jumpVector = Mathf.Sqrt(JumpForce * -2f * Gravity);
        Velocity = new Vector3(Speed * sideOfJump, jumpVector, Velocity.z);
        isReadyToJump = false;

        // Ждем перед следующим прыжком
        Invoke(nameof(PrepareForJump), stopDuration);
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

    private void ChangeJumpSide()
    {
        if (!PlayerCheck())
        {
            // Выбираем случайное направление
            sideOfJump = Random.Range(0, 2) == 0 ? -1 : 1;
        }
    }

    private bool PlayerCheck()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiusOfVision);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                sideOfJump = transform.position.x >= collider.transform.position.x ? -1 : 1;
                return true;
            }
        }
        return false;
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReadyToAttack)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                player.Health.TakeDamage(AttackDamage);
                isReadyToAttack = false;
                StartCoroutine(AttackCooldownTimer(AttackCooldown));
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isReadyToAttack && other.transform.parent.TryGetComponent(out Player player))
        {
            player.Health.TakeDamage(AttackDamage);
            isReadyToAttack = false;
            StartCoroutine(AttackCooldownTimer(AttackCooldown));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusOfVision);
    }
}
