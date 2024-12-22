namespace CourseWork.entity.employmentHistory;

public sealed class Punishment
{
    private PunishmentType _type;
    private long _id;
    private long _date;

    public PunishmentType Type => _type;
    public long Id => _id;
    public long Date => _date;

    public Punishment(PunishmentType type, long id, long date)
    {
        _type = type;
        _id = id;
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