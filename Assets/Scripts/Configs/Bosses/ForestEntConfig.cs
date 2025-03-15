using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ForestEntConfig", menuName = "Configs/Boss/ForestEntConfig")]
public class ForestEntConfig : ScriptableObject
{
    public float rangedAttackMeleeStrike = 1f;
    public float rangedAttackMeleeRootStrike = 2f;
    public float rangedAttackDistanceRootStrike = 5f;
    public float attackCooldown = 3f;
    public float attackDelayRootStrike = 1f;
    public int rootStrikeDamage = 5;
    [Range(0,1)] public float slowdownFactorRootStrike = 0.5f;
    public float timeCooldownAttackRootStrike = 0.5f;
    public float timeLiveRootStrike = 2f;
    public int damageMeleeStrike = 10;
    public int health = 100;
}