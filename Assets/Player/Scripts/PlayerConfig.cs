using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: Header("Move Stats")]
    [field: SerializeField, Range(0, 30)] public float Speed { get; set; } = 10f;
    
    [field: Header("Gravity Stats")]
    [field: SerializeField, Range(-100, 0)] public float Gravity { get; set; }= 20f;
    
    [field: Header("Jump Stats")]
    [field: SerializeField, Range(0, 30)] public float JumpForce { get; set; }= 20f;
    
    [field: Header("Roll Stats")]
    [field: SerializeField, Range(0, 3)] public float RollDuration { get; set; }= 1f;
    [field: SerializeField, Range(0, 60)] public float RollSpeed { get; set; }= 10f;
    
    // Пока что, что ниже нет
    [field: Header("Attack Stats")]
        
    [field: SerializeField, Range(0, 5)] public float AttackDuration { get; set; }= 0.3f;
    /// <summary>
    /// Коэффициент замедления игрока во время атаки
    /// </summary>
    [field: SerializeField, Range(0, 1)] public float AttackSlowdown { get; set; }= 0.6f;
    [field: SerializeField, Range(0, 30)] public float Damage { get; set; }= 3f;
    
    
    [field: Header("Health Stats")]
    [field: SerializeField, Range(0, 1000)] public int Health { get; set; } = 100;
}