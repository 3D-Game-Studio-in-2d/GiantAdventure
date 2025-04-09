using UnityEngine;

namespace Game.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimatorHandler : MonoBehaviour
    {
        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();

            var shoot = GetComponent<EnemyShoot>();
            var health = GetComponent<EnemyHealth>();
            var patrol = GetComponent<EnemyPatrol>();
            var chase = GetComponent<EnemyChasePlayer>();

            if (shoot != null) shoot.onShoot.AddListener(() => animator.SetTrigger("Shoot"));
            if (health != null)
            {
                health.onTakeDamage.AddListener(() => animator.SetTrigger("Hit"));
                health.onDie.AddListener(() => animator.SetTrigger("Die"));
            }
            if (patrol != null) patrol.onReachPoint.AddListener(() => animator.SetTrigger("Patrol"));
            if (chase != null) chase.onChaseStart.AddListener(() => animator.SetBool("Run", true));
        }
    }
}