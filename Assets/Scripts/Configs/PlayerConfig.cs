using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: Header("Move Stats")]
    [field: SerializeField, Range(0, 30)] 
    public float Speed { get; set; } = 10f;
    
    [field: Header("Gravity Stats")]
    [field: SerializeField, Range(-100, 0)] 
    public float Gravity { get; set; }= 20f;
    
    [field: Header("Jump Stats")]
    [field: SerializeField, Range(0, 30)] 
    public float JumpForce { get; set; }= 20f;
    
    [field: Header("Roll Stats")]
    [field: SerializeField, Range(0, 3)] 
    public float RollDuration { get; set; }= 1f;
    [field: SerializeField, Range(0, 60)] 
    public float RollSpeed { get; set; }= 10f;
    [field: SerializeField, Range(0, 10)] 
    public float RollCooldown { get; set; }
    
    [field: Space(50)]
    [field: Header("Attack Stats")]
    [field: SerializeField, Range(0, 1)] 
    public float AttackSlowdown { get; set; }= 0.6f;
    
    [field: Header("First Strike")]
    [field: SerializeField, Range(0, 100)]
    public int FirstStrikeDamage { get; private set; } = 0;
    [field: SerializeField, Range(0, 5)]
    public float FirstStrikeAttackDuration { get; private set; } = 0f;
    [field: SerializeField]
    public Vector3 FirstStrikeBoxSize { get; private set; } = Vector3.zero;
    [field: SerializeField]
    public Vector3 FirstStrikeBoxCenter { get; private set; } = Vector3.zero;
    
    [field: Header("Second Strike")]
    [field: SerializeField, Range(0, 100)]
    public int SecondStrikeDamage { get; private set; } = 0;
    [field: SerializeField, Range(0, 5)]
    public float SecondStrikeAttackDuration { get; private set; } = 0f;
    [field: SerializeField]
    public Vector3 SecondStrikeBoxSize { get; private set; } = Vector3.zero;
    [field: SerializeField]
    public Vector3 SecondStrikeBoxCenter { get; private set; } = Vector3.zero;
    
    [field: Header("Third Strike")]
    [field: SerializeField, Range(0, 100)]
    public int ThirdStrikeDamage { get; private set; } = 0;
    [field: SerializeField, Range(0, 5)]
    public float ThirdStrikeAttackDuration { get; private set; } = 0f;
    [field: SerializeField]
    public Vector3 ThirdStrikeBoxSize { get; private set; } = Vector3.zero;
    [field: SerializeField]
    public Vector3 ThirdStrikeBoxCenter { get; private set; } = Vector3.zero;
    
    [field: Space(50)]
    [field: Header("Health Stats")]
    [field: SerializeField, Range(0, 1000)] public int Health { get; set; } = 100;

}