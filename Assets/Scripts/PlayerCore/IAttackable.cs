using UnityEngine;

public interface IAttackable
{
        float Damage { get; set; }
        float AttackDuration { get; set; }
        float AttackSlowdown { get; set; }
        bool IsAttacking { get; set; }
        public Vector3 BoxCenter { get; set; }
        public Vector3 BoxSize { get; set; }
}