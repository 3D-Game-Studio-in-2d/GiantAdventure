using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyChasePlayer : MonoBehaviour
    {
        [Header("Chase Settings")]
        [SerializeField] private float detectionRange = 5f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float minDistanceToPlayer = 1.5f;

        public UnityEvent onChaseStart;
        public UnityEvent onAttack;

        [Header("Attack")]
        [SerializeField] private float attackRate = 1f;
        private float lastAttackTime = 0f;

        [Header("Vision")]
        [SerializeField] private Transform visionOrigin;
        [SerializeField] private float visionDistance = 10f;
        [SerializeField] private LayerMask visionMask;

        [Header("Rotation")]
        [SerializeField] private float turnInterval = 1f;
        private float lastTurnTime = 0f;

        private Transform _player;
        private Rigidbody _rigidbody;
        private EnemyPatrol _patrol;
        private bool chasing = false;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            _patrol = GetComponent<EnemyPatrol>();
        }

        private void Start()
        {
            _player = GameObject.FindWithTag("Player")?.transform;
            if (_player == null)
                Debug.LogError("»грок с тегом 'Player' не найден!");
        }

        private void FixedUpdate()
        {
            if (_player == null) return;

            Vector3 toPlayer = _player.position - transform.position;
            float distance = toPlayer.magnitude;

            if (distance < detectionRange && CanSeePlayer())
            {
                if (!chasing)
                {
                    onChaseStart?.Invoke();
                    chasing = true;
                    if (_patrol != null) _patrol.enabled = false;
                }

                TryRotateToPlayer(toPlayer);

                if (distance > minDistanceToPlayer)
                {
                    Chase(toPlayer.normalized);
                }
                else if (Time.time - lastAttackTime >= attackRate)
                {
                    onAttack?.Invoke();
                    lastAttackTime = Time.time;
                }
            }
            else if (chasing)
            {
                chasing = false;
                if (_patrol != null) _patrol.enabled = true;
            }
        }

        private bool CanSeePlayer()
        {
            if (_player == null) return false;

            Vector3 origin = visionOrigin != null ? visionOrigin.position : transform.position;
            Vector3 direction = (_player.position - origin).normalized;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, visionDistance, visionMask))
            {
                return hit.transform.CompareTag("Player");
            }

            return false;
        }

        private void Chase(Vector3 direction)
        {
            float moveX = Mathf.Sign(direction.x);
            Vector3 velocity = new Vector3(moveX * speed, _rigidbody.velocity.y, 0f);
            _rigidbody.MovePosition(_rigidbody.position + velocity * Time.fixedDeltaTime);
        }

        private void TryRotateToPlayer(Vector3 direction)
        {
            if (Time.time - lastTurnTime >= turnInterval)
            {
                float moveX = Mathf.Sign(direction.x);
                if (moveX != 0)
                {
                    transform.rotation = moveX > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180f, 0);
                    lastTurnTime = Time.time;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (visionOrigin == null) return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(visionOrigin.position, transform.right * visionDistance);
        }
    }
}
