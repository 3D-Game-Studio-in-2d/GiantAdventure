using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies
{
    public class EnemyPatrol : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;
        private int currentPoint = 0;

        [HideInInspector] public float speed = 2f;
        public UnityEvent onReachPoint;

        private void Update()
        {
            if (patrolPoints.Length == 0) return;

            Transform target = patrolPoints[currentPoint];
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Поворот к следующей точке
            if ((target.position.x - transform.position.x) != 0)
            {
                float dir = Mathf.Sign(target.position.x - transform.position.x);
                transform.rotation = dir > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180f, 0);
            }

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                onReachPoint?.Invoke();
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
            }
        }
    }
}
