namespace CourseWork_2.Domain.Models;

public sealed class Punishment
{
    private PunishmentType _type;
    private DateTime _date;
    private string _reason;
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

    public string Reason
    {
        get => _reason;
        set => _reason = value;
    }
    
    public Punishment(PunishmentType type, DateTime date, string reason)
    {
        _type = type;
        _date = date;
        _reason = reason;
    }
    

    public enum PunishmentType
    {
        Warning,
        Reprimand,
        Fines,
        BonusesWithholding,
        Demotion,
        Fire
    }
}