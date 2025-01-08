namespace CourseWork_2.Domain.Models;

public class Human
{
    private Guid _uuid;
    private Passport _passport;
    private UserDefaultCredentials _userDefaultCredentials;
    private List<EmploymentHistoryRecord> _employmentHistoryRecords;
    private Education _education;
    private EducationDocument _educationDocument;

    public Human(Guid uuid, Passport passport, UserDefaultCredentials userDefaultCredentials,
        List<EmploymentHistoryRecord> employmentHistoryRecords, Education education,
        EducationDocument educationDocument)
    {
        _uuid = uuid;
        _passport = passport;
        _userDefaultCredentials = userDefaultCredentials;
        _employmentHistoryRecords = employmentHistoryRecords;
        _education = education;
        _educationDocument = educationDocument;
    }

    public EmploymentHistoryRecord? LastEmploymentHistoryRecord
    {
        get { return _employmentHistoryRecords.LastOrDefault(); }
    }
    
    public Guid Uuid
    {
        get { return _uuid; }
        set { _uuid = value; }
    }

    public Passport Passport
    {
        get { return _passport; }
        set { _passport = value; }
    }

    public UserDefaultCredentials UserDefaultCredentials
    {
        get { return _userDefaultCredentials; }
        set { _userDefaultCredentials = value; }
    }

    public List<EmploymentHistoryRecord> EmploymentHistoryRecords
    {
        get { return _employmentHistoryRecords; }
        set => _employmentHistoryRecords = value;
    }

    public Education Education
    {
        get { return _education; }
        set { _education = value; }
    }

    public EducationDocument EducationDocument
    {
        get { return _educationDocument; }
        set { _educationDocument = value; }
    }
}

public class HumanDataHolder
{
    public Passport? Passport { get; set; }
    public UserDefaultCredentials? UserDefaultCredentials { get; set; }
    public EducationDocument? EducationDocument { get; set; }
}