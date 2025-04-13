using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private int maxHealth = 3;
        private int currentHealth;

        public UnityEvent<int, int> onTakeDamage;
        public UnityEvent onDie;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            onTakeDamage?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                onDie?.Invoke();
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
