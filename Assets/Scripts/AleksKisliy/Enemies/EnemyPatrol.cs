using UnityEngine;

namespace Game.Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyPatrol : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private Transform visualTransform;
        [SerializeField] private float speed = 2f;

        private int currentPoint = 0;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        }

        private void FixedUpdate()
        {
            if (patrolPoints.Length == 0) return;

            Transform target = patrolPoints[currentPoint];
            Vector3 direction = (target.position - transform.position).normalized;

            if (direction.x != 0 && visualTransform != null)
            {
                float sign = Mathf.Sign(direction.x);
                Quaternion rot = Quaternion.Euler(0, sign > 0 ? 90f : -90f, 0);
                visualTransform.rotation = Quaternion.Lerp(visualTransform.rotation, rot, Time.fixedDeltaTime * 5f);
            }

            Vector3 move = direction * speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + new Vector3(move.x, 0f, 0f));

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
            }
        }
    }
}
