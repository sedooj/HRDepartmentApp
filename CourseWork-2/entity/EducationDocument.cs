namespace CourseWork.entity;

public sealed class EducationDocument : Education
{
    public string Serial { get; }
    public EducationLevels Level { get; }
    public string Direction { get; }
    public string DateOfIssue { get; }

    public EducationDocument(long documentId, string institution, string graduatedDate, string specialty, string serial, EducationLevels level, string direction, string dateOfIssue)
        : base(documentId, institution, graduatedDate, specialty)
    {
        Serial = serial;
        Level = level;
        Direction = direction;
        DateOfIssue = dateOfIssue;
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