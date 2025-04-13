using UnityEngine;

namespace Game.Enemies
{
    public class EnemyMeleeAttack : MonoBehaviour
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private LayerMask targetMask;

        private float lastAttackTime = 0f;

        public void TryAttack()
        {
            if (Time.time - lastAttackTime < attackCooldown) return;

            Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, targetMask);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out Player player))
                    player.Health.TakeDamage(damage);

            }

            lastAttackTime = Time.time;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}
