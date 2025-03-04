using UnityEngine;

public class AttackPlayerStats
{
        public float AttackSlowdown { get; private set; } = 1f;
        
        public Strike First { get; private set; }
        public Strike Second { get; private set; }
        public Strike Third { get; private set; }

        public AttackPlayerStats(PlayerConfig playerConfig)
        {
                AttackSlowdown = playerConfig.AttackSlowdown;
                
                First = new(
                        playerConfig.FirstStrikeDamage, 
                        playerConfig.FirstStrikeAttackDuration, 
                        playerConfig.FirstStrikeBoxSize, 
                        playerConfig.FirstStrikeBoxCenter);
                
                Second = new(
                        playerConfig.SecondStrikeDamage, 
                        playerConfig.SecondStrikeAttackDuration, 
                        playerConfig.SecondStrikeBoxSize, 
                        playerConfig.SecondStrikeBoxCenter);
                
                Third = new(
                        playerConfig.ThirdStrikeDamage, 
                        playerConfig.ThirdStrikeAttackDuration, 
                        playerConfig.ThirdStrikeBoxSize, 
                        playerConfig.ThirdStrikeBoxCenter);
                
        }
}