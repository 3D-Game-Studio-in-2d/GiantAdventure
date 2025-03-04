using UnityEngine;

[CreateAssetMenu(fileName = "SlimeConfig", menuName = "Configs/Enemy/SlimeConfig")]
public class SlimeConfig : ScriptableObject
{
        [field: Header("Health Stats")]
        [field: SerializeField, Range(0f, 100)]
        public int Health { get; private set; } = 20;
}