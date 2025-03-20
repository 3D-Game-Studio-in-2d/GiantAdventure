using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TrapsConfig", menuName = "Configs/TrapsConfig")]
public class TrapsConfig : ScriptableObject
{
    [Header("ManTrap Stats"), Range(0f, 1f)]
    public float manTrapSpeedReduction;
    
    [Range(0, 30)]
    public int manTrapDamage;
    
    [Range(0, 5)]
    public float manTrapSpeedReductionTime;
}