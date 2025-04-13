using UnityEngine;

namespace Game.Enemies
{
    [CreateAssetMenu(fileName = "EnemyBehaviorConfig", menuName = "AleksKisliyEnemy/BehaviorConfig")]
    public class EnemyBehaviorConfig : ScriptableObject
    {
        public int health = 3;
        public float patrolSpeed = 2f;
        public float chaseSpeed = 3f;
        public float detectionRange = 5f;
        public float shootCooldown = 1f;
        public float shootRange = 4f;
    }
}