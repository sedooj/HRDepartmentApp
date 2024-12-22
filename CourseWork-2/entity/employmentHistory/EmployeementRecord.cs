namespace CourseWork.entity.employmentHistory;

public class EmploymentRecord
{
    private long _workingStartDate;
    private long _workingEndDate;
    private string _startEmploymentPosition;
    private string _positionAtWork;
    private string _dismissReason;
    
    public long WorkingStartDate { get => _workingStartDate; private set => _workingStartDate = value; }
    public long WorkingEndDate { get => _workingEndDate; private set => _workingEndDate = value; }
    public string StartEmploymentPosition { get => _startEmploymentPosition; private set => _startEmploymentPosition = value; }
    public string PositionAtWork { get => _positionAtWork; private set => _positionAtWork = value; }
    public string DismissReason { get => _dismissReason; private set => _dismissReason = value; }

    public EmploymentRecord(long workingStartDate, long workingEndDate, string startEmploymentPosition, string positionAtWork, string dismissReason)
    {
        _workingStartDate = workingStartDate;
        _workingEndDate = workingEndDate;
        _startEmploymentPosition = startEmploymentPosition;
        _positionAtWork = positionAtWork;
        _dismissReason = dismissReason;
    }
}