namespace CourseWork_2.Domain.Models;

public sealed class Reward
{
    private string _id;
    private DateTime _date;
    private RewardType _type;
    
    public string Id
    {
        get => _id;
        set => _id = value;
    }

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

    public Reward(string id, RewardType type, DateTime date)
    {
        _id = id;
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