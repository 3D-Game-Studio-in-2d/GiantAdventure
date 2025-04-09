using UnityEngine;

namespace Game.Enemies
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyChasePlayer), typeof(EnemyPatrol))]
    //[RequireComponent (typeof(EnemyShoot))]
    public class EnemyConfigurator : MonoBehaviour
    {
        [SerializeField] private EnemyBehaviorConfig config;

        private void Awake()
        {
            if (config == null) return;

            GetComponent<EnemyHealth>().maxHealth = config.health;

            var patrol = GetComponent<EnemyPatrol>();
            patrol.speed = config.patrolSpeed;

            var chase = GetComponent<EnemyChasePlayer>();
            chase.speed = config.chaseSpeed;
            chase.detectionRange = config.detectionRange;

            var shoot = GetComponent<EnemyShoot>();
            shoot.shootCooldown = config.shootCooldown;
            shoot.shootRange = config.shootRange;
        }
    }
}