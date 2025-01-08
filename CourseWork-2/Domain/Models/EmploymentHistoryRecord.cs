namespace CourseWork_2.Domain.Models;

public class EmploymentHistoryRecord
{
    private AcademicDegree _degree;
    private AcademicRank _rank;
    private DateTime _workingStartDate;
    private DateTime? _workingEndDate;
    private Guid _companyId;
    private string _companyName;
    private string _startEmploymentPosition;
    private string _positionAtWork;
    private string _fireReason;
    private List<CareerMove> _careerMoves;
    private List<Punishment> _punishments;
    private List<Reward> _rewards;

    public EmploymentHistoryRecord(AcademicDegree degree, AcademicRank rank, DateTime workingStartDate,
        DateTime? workingEndDate, Guid companyId, string companyName, string startEmploymentPosition, string positionAtWork,
        string fireReason, List<CareerMove> careerMoves, List<Punishment> punishments, List<Reward> rewards)
    {
        _degree = degree;
        _rank = rank;
        _workingStartDate = workingStartDate;
        _workingEndDate = workingEndDate;
        _companyId = companyId;
        _companyName = companyName;
        _startEmploymentPosition = startEmploymentPosition;
        _positionAtWork = positionAtWork;
        _fireReason = fireReason;
        _careerMoves = careerMoves;
        _punishments = punishments;
        _rewards = rewards;
    }

    public string CompanyName
    {
        get => _companyName;
        set => _companyName = value;
    }
    public AcademicDegree Degree
    {
        get => _degree;
        set => _degree = value;
    }

    public AcademicRank Rank
    {
        get => _rank;
        set => _rank = value;
    }

    public DateTime WorkingStartDate
    {
        get => _workingStartDate;
        set => _workingStartDate = value;
    }

    public DateTime? WorkingEndDate
    {
        get => _workingEndDate;
        set => _workingEndDate = value;
    }

    public Guid CompanyId
    {
        get => _companyId;
        set => _companyId = value;
    }

    public string StartEmploymentPosition
    {
        get => _startEmploymentPosition;
        set => _startEmploymentPosition = value;
    }

    public string PositionAtWork
    {
        get => _positionAtWork;
        set => _positionAtWork = value;
    }

    public string FireReason
    {
        get => _fireReason;
        set => _fireReason = value;
    }

    public List<CareerMove> CareerMoves
    {
        get => _careerMoves;
        set => _careerMoves = value;
    }

    public List<Punishment> Punishments
    {
        get => _punishments;
        set => _punishments = value;
    }

    public List<Reward> Rewards
    {
        get => _rewards;
        set => _rewards = value;
    }

    public enum AcademicDegree
    {
        DoctorOfScience = 1,
        CandidateOfScience = 2,
        NoDegree = 3
    }

    public enum AcademicRank
    {
        Docent = 1,
        Professor = 2,
        NoRank = 3
    }
}