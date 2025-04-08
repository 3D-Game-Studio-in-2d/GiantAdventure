using UnityEngine;

[CreateAssetMenu(fileName = "BanditConfig", menuName = "Configs/Enemy/BanditConfig")]
public class BanditConfig : ScriptableObject
{
        [field: Header("Health Stats")]
        [field: SerializeField, Range(0f, 100)] public int Health { get; private set; } = 20; 
        
        [field: Header("Range Stats")]
        [field: SerializeField, Range(1, 100)] public float RadiusOfVision { get; set; } = 15f;
        [field: SerializeField, Range(0, 100)] public float BackVision { get; set; } = 2f;
        [field: SerializeField, Range(0.01f, 0.5f)] public float WalkStopRange { get; set; } = 0.2f;
        [field: SerializeField, Range(0.5f, 5f)] public float AttackStopRange { get; set; } = 1f;

        [field: Header("Attack stats")]
        [field: SerializeField, Range(0, 1000)] public int AttackDamage { get; set; } = 5;
        [field: SerializeField, Range(0, 10)] public float AttackCooldown { get; set; } = 1f;
        [field: SerializeField, Range(0, 5)] public float AttackRange { get; set; } = 1.5f;
        [field: SerializeField, Range(0, 1)] public float PreAttackDelay { get; set; } = 0.25f;

        [field: Header("Movement stats")]
        [field: SerializeField, Range(0.1f, 100)] public float Speed { get; set; } = 3f;
        [field: SerializeField, Range(0.1f, 100)] public float WalkSpeed { get; set; } = 1f;
        [field: SerializeField] public Vector2 StopDurationRange { get; set; } = new Vector2(1f, 2f);
        [field: SerializeField, Range(-50f, 0f)] public float Gravity { get; set; } = -10f;
}