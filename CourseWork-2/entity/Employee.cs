using CourseWork.entity.employmentHistory;

namespace CourseWork.entity;

public sealed class Employee : Human
{
    private long _id;
    private string _position;

    public Employee(Passport passport, UserDefaultCredentials userDefaultCredentials,
        List<EmploymentHistoryRecord> employmentHistoryRecords, Education education,
        EducationDocument educationDocument, long id, string position) 
        : base(passport, userDefaultCredentials, employmentHistoryRecords, education, educationDocument)
    {
        _id = id;
        _position = position;
    }

    public long Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string Position
    {
        get { return _position; }
        set { _position = value; }
    }
}