namespace CourseWork_2.Domain.Models;

public sealed class Punishment
{
    private PunishmentType _type;
    private DateTime _date;

    public PunishmentType Type
    {
        get => _type;
        set => _type = value;
    }

    public DateTime Date
    {
        get => _date;
        set => _date = value;
    }

    public Punishment(PunishmentType type, DateTime date)
    {
        _type = type;
        _date = date;
    }

    public enum PunishmentType
    {
        Warning,
        Reprimand,
        Fines,
        BonusesWithholding,
        Demotion,
        Dismissal
    }
}