using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
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
        _health.OnDeath += Death;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
    
    private void OnHealthView(int damage)
    {
        healthBar.fillAmount = _health.Value / (float)_health.MaxValue;
    }
}