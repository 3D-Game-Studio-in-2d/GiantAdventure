using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
        [SerializeField] private Image healthBar;
    
        private Health _health;

        private void Start()
        {
                if (healthBar == null)
                {
                        healthBar = GetComponent<Image>();
                }
        }

        public void Initialize(Health health)
        {
                _health = health;
                _health.OnDamageEffect += OnHealthView;
        }

        private void OnHealthView(int damage)
        {
                healthBar.fillAmount = _health.Value / (float)_health.MaxValue;
        }
}