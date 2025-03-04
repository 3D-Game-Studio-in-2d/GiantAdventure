using UnityEngine;

public class Strike
{
    public int Damage { get; private set; } = 0;
    public float AttackDuration { get; private set; } = 0f;
    public Vector3 BoxSize { get; private set; } = Vector3.zero;
    public Vector3 BoxCenter { get; private set; } = Vector3.zero;

    public Strike(int damage, float attackDuration, Vector3 boxSize, Vector3 boxCenter)
    {
        Damage = damage;
        AttackDuration = attackDuration;
        BoxSize = boxSize;
        BoxCenter = boxCenter;
    }
}