using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public int maxHealth = 3;
        private int currentHealth;

        public UnityEvent onTakeDamage;
        public UnityEvent onDie;

        void Start()
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
                Destroy(gameObject, 0.5f);
            }
        }
    }
}