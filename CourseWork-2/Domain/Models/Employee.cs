namespace CourseWork_2.Domain.Models;

public sealed class Employee : Human
{
    private string _position;

    public Employee(string uuid, Passport passport, UserDefaultCredentials userDefaultCredentials,
        List<EmploymentHistoryRecord> employmentHistoryRecords, Education education,
        EducationDocument educationDocument, string position) 
        : base(uuid, passport, userDefaultCredentials, employmentHistoryRecords, education, educationDocument)
    {
        _position = position;
    }
    
    public string Position
    {
        get { return _position; }
        set { _position = value; }
    }
}