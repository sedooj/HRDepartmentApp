namespace CourseWork_2.Domain.Models;

public sealed class Reward
{
    public int Id { get; }
    public RewardType Type { get; }
    public long Date { get; }

    public Reward(int id, RewardType type, long date)
    {
        Id = id;
        Type = type;
        Date = date;
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