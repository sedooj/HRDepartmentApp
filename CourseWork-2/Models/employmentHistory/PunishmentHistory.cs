namespace CourseWork.entity.employmentHistory;

public class PunishmentHistory
{
    private List<Punishment> _punishments;

    public List<Punishment> Punishments => _punishments;

    public PunishmentHistory(List<Punishment> punishments)
    {
        _punishments = punishments ?? new List<Punishment>();
    }

    public void AddPunishment(Punishment punishment)
    {
        _punishments.Add(punishment);
    }
}