namespace CourseWork_2.Domain.Models;

public sealed class Punishment
{
    private Guid _id;
    private PunishmentType _type;
    private DateTime _date;
    private string _reason;
    
    public Guid Id
    {
        get => _id;
        set => _id = value;
    }
    
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
    
    public Punishment(Guid id, PunishmentType type, DateTime date, string reason)
    {
        _id = id;
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