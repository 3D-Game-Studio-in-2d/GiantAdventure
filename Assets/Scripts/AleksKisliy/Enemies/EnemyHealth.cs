using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        public int maxHealth = 3;
        private int currentHealth;

        public UnityEvent onTakeDamage;
        public UnityEvent onDie;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            onTakeDamage?.Invoke();

            if (currentHealth <= 0)
            {
                onDie?.Invoke();
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
