using UnityEngine;

[CreateAssetMenu(fileName = "WanderingFlameConfig", menuName = "Configs/Enemy/WanderingFlameConfig")]
public class WanderingFlameConfig : ScriptableObject
{
    [field: Header("Move Stats")]
    [field: SerializeField, Range(0, 30)] 
    public float Speed { get; set; } = 2f;
    
    [field: Header("Health Stats")]
    [field: SerializeField, Range(0, 999)] 
    public int Health { get; set; } = 20;
    
    [field: Header("Wandering zone")]
    [field: SerializeField, Range(0, 100)] 
    public float Width { get; set; } = 5f;
    [field: SerializeField, Range(0, 100)] 
    public float Height { get; set; } = 2f;
}