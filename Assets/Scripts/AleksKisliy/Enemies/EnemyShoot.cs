using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyShoot : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform shootPoint;

        [HideInInspector] public float shootCooldown = 1f;
        [HideInInspector] public float shootRange = 4f;

        public UnityEvent onShoot;

        private float lastShotTime = 0f;
        private Transform _player;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player")?.transform;

            if (_player == null)
                Debug.LogError("»грок с тегом 'Player' не найден!");
        }

        private void Update()
        {
            if (_player == null || bulletPrefab == null || shootPoint == null) return;

            float distance = Vector3.Distance(transform.position, _player.position);
            if (distance < shootRange && Time.time - lastShotTime > shootCooldown)
            {
                Shoot();
                lastShotTime = Time.time;
            }
        }

        private void Shoot()
        {
            onShoot?.Invoke();

            GameObject bulletObj = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            Vector3 direction = (_player.position - shootPoint.position + Vector3.up / 1.5f).normalized;

            if (bulletObj.TryGetComponent(out EnemyBullet bullet))
                bullet.Init(direction);
            else
                Debug.LogWarning("EnemyBullet component not found on bulletPrefab!");
        }
    }
}
