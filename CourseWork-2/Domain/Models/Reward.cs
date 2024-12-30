namespace CourseWork_2.Domain.Models;

public sealed class Reward
{
    public RewardType Type { get; }
    public Reward(RewardType type)
    {
        Type = type;
    }

    public enum RewardType
    {
        Bonuses,
        Recognition,
        Certificate,
        Promotion,
        PayRaise,
        AdditionalBenefits,
        FlexibleSchedule
    }
}