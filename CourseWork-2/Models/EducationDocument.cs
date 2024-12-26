using CourseWork.entity;

namespace CourseWork_2.Models;

public sealed class EducationDocument : Education
{
    private string _serial;
    private string _number;
    private EducationLevels _level;
    private string _direction;
    private string _dateOfIssue;

    public EducationDocument(long documentId, string institution, string graduatedDate, string specialty, string serial, string number, EducationLevels level, string direction, string dateOfIssue)
        : base(documentId, institution, graduatedDate, specialty)
    {
        _serial = serial;
        _number = number;
        _level = level;
        _direction = direction;
        _dateOfIssue = dateOfIssue;
    }

    public string Serial
    {
        get { return _serial; }
        set { _serial = value; }
    }

    public string Number
    {
        get { return _number; }
        set { _number = value; }
    }

    public EducationLevels Level
    {
        get { return _level; }
        set { _level = value; }
    }

    public string Direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public string DateOfIssue
    {
        get { return _dateOfIssue; }
        set { _dateOfIssue = value; }
    }

    public enum EducationLevels
    {
        Bachelor = 1,
        Master = 2,
        PhD = 3,
        Doctorate = 4,
        Specialist = 5,
        AssociateDegree = 6,
        Postdoc = 7
    }
}