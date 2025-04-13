using UnityEngine;
using UnityEngine.UI;

namespace Game.Enemies
{
    public class EnemyUI : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private EnemyHealth enemyHealth;

        private void Start()
        {
            if (enemyHealth != null)
            {
                enemyHealth.onTakeDamage.AddListener(UpdateHealthBar);
                enemyHealth.onDie.AddListener(OnEnemyDie);
                UpdateHealthBar(enemyHealth.CurrentHealth, enemyHealth.MaxHealth);
            }
        }

        private void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            if (healthBar != null)
            {
                healthBar.fillAmount = (float)currentHealth / maxHealth;
            }
        }

        private void OnEnemyDie()
        {
            Destroy(gameObject); // Удаляет UI при смерти врага
        }
    }
}
