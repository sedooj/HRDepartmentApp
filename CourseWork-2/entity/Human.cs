using CourseWork.entity;
using CourseWork.entity.employmentHistory;

namespace CourseWork_2.entity;

public abstract class Human
{
    private Passport _passport;
    private UserDefaultCredentials _userDefaultCredentials;
    private List<EmploymentHistoryRecord> _employmentHistoryRecords;
    private Education _education;
    private EducationDocument _educationDocument;

    public Human(Passport passport, UserDefaultCredentials userDefaultCredentials,
        List<EmploymentHistoryRecord> employmentHistoryRecords, Education education,
        EducationDocument educationDocument)
    {
        _passport = passport;
        _userDefaultCredentials = userDefaultCredentials;
        _employmentHistoryRecords = employmentHistoryRecords;
        _education = education;
        _educationDocument = educationDocument;
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