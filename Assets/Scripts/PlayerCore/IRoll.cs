public interface IRoll
{
        float RollSpeed { get; set; }
        float RollDuration { get; set; }
        float RollCooldown { get; set; }
        bool IsRolling { get; set; }
}