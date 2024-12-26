using CourseWork_2.Models.employmentHistory;

namespace CourseWork_2.Models;

public class EmploymentHistoryRecord
{
    private AcademicDegree _degree;
    private AcademicRank _rank;
    private long _workingStartDate;
    private long _workingEndDate;
    private Company _company;
    private string _startEmploymentPosition;
    private string _positionAtWork;
    private string _dismissReason;
    private List<CareerMove> _careerMoves;
    private List<Punishment> _punishments;
    private List<Reward> _rewards;

    public EmploymentHistoryRecord(AcademicDegree degree, AcademicRank rank, long workingStartDate, 
        long workingEndDate, Company company, string startEmploymentPosition, string positionAtWork, 
        string dismissReason, List<CareerMove> careerMoves, List<Punishment> punishments, List<Reward> rewards)
    {
        _degree = degree;
        _rank = rank;
        _workingStartDate = workingStartDate;
        _workingEndDate = workingEndDate;
        _company = company;
        _startEmploymentPosition = startEmploymentPosition;
        _positionAtWork = positionAtWork;
        _dismissReason = dismissReason;
        _careerMoves = careerMoves;
        _punishments = punishments;
        _rewards = rewards;
    }

    public AcademicDegree Degree => _degree;
    public AcademicRank Rank => _rank;
    public long WorkingStartDate => _workingStartDate;
    public long WorkingEndDate => _workingEndDate;
    public Company Company => _company;
    public string StartEmploymentPosition => _startEmploymentPosition;
    public string PositionAtWork => _positionAtWork;
    public string DismissReason => _dismissReason;
    public List<CareerMove> CareerMoves => _careerMoves;
    public List<Punishment> Punishments => _punishments;
    public List<Reward> Rewards => _rewards;

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