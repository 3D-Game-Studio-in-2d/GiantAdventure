using UnityEngine;

namespace Game.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimatorHandler : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            var shoot = GetComponent<EnemyShoot>();
            var health = GetComponent<EnemyHealth>();
            var patrol = GetComponent<EnemyPatrol>();
            var chase = GetComponent<EnemyChasePlayer>();

            if (shoot != null) shoot.onShoot.AddListener(() => animator.SetTrigger("Shoot"));
            if (health != null)
            {
                //health.onTakeDamage.AddListener(() => animator.SetTrigger("Hit"));
                health.onDie.AddListener(() => animator.SetTrigger("Die"));
            }
            if (chase != null)
            {
                chase.onChaseStart.AddListener(() => animator.SetBool("Chase", true));
                chase.onChaseEnd.AddListener(() => animator.SetBool("Chase", false));
                chase.onAttack.AddListener(() => animator.SetTrigger("Attack"));
            }
        }

        public void CheseON()
        {
            Debug.Log("Начинаем приследование");
        }

        public void Attack()
        {
            Debug.Log("Начинаем атаковать");
        }
    }
}