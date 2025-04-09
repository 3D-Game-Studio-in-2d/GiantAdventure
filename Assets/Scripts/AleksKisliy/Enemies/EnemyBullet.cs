using UnityEngine;

namespace Game.Enemies
{
    public class EnemyBullet : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private int damage = 1;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private LayerMask targetLayer;

        private Vector3 direction;

        public void Init(Vector3 shootDirection)
        {
            direction = shootDirection.normalized;
            Destroy(gameObject, lifeTime); // автоудаление
        }

        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            int otherLayerMask = 1 << other.gameObject.layer;
            if ((targetLayer.value & otherLayerMask) != 0)
            {
                if (other.TryGetComponent(out Player player))
                {
                    player.Health.TakeDamage(damage);
                }
            }

            Destroy(gameObject);
        }
    }
}