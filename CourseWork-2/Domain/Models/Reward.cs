namespace CourseWork_2.Domain.Models;

public sealed class Reward
{
    private DateTime _date;
    private RewardType _type;

    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    public RewardType Type
    {
        get => _type;
        set => _type = value;
    }

    public Reward(RewardType type, DateTime date)
    {
        _type = type;
        _date = date;
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