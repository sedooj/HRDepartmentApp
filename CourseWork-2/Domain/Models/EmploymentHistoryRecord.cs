namespace CourseWork_2.Domain.Models;

public class EmploymentHistoryRecord
{
    private AcademicDegree _degree;
    private AcademicRank _rank;
    private DateTime _workingStartDate;
    private DateTime? _workingEndDate;
    private string _companyUuid;
    private string _startEmploymentPosition;
    private string _positionAtWork;
    private string _dismissReason;
    private List<CareerMove> _careerMoves;
    private List<Punishment> _punishments;
    private List<Reward> _rewards;

    public EmploymentHistoryRecord(AcademicDegree degree, AcademicRank rank, DateTime workingStartDate,
        DateTime? workingEndDate, string companyUuid, string startEmploymentPosition, string positionAtWork,
        string dismissReason, List<CareerMove> careerMoves, List<Punishment> punishments, List<Reward> rewards)
    {
        _degree = degree;
        _rank = rank;
        _workingStartDate = workingStartDate;
        _workingEndDate = workingEndDate;
        _companyUuid = companyUuid;
        _startEmploymentPosition = startEmploymentPosition;
        _positionAtWork = positionAtWork;
        _dismissReason = dismissReason;
        _careerMoves = careerMoves;
        _punishments = punishments;
        _rewards = rewards;
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

    public string CompanyUuid
    {
        get => _companyUuid;
        set => _companyUuid = value;
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

    public string DismissReason
    {
        get => _dismissReason;
        set => _dismissReason = value;
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