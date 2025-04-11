using UnityEngine;

namespace Game.Enemies
{
    public class EnemyBullet : MonoBehaviour
    {
        public float speed = 5f;
        public int damage = 1;
        public float lifeTime = 5f;
        public LayerMask targetLayer;

        private Vector3 direction;

        public void Init(Vector3 shootDirection)
        {
            direction = shootDirection.normalized;
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & targetLayer) != 0)
            {
                if (other.TryGetComponent(out Player player))
                {
                    player.Health.TakeDamage(damage);
                }

                Destroy(gameObject);
            }
        }
    }
}
