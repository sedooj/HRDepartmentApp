namespace CourseWork.entity;

public sealed class Passport
{
    public string Serial { get; }
    public string DateOfIssue { get; }
    public string WhoIssued { get; }

    public Passport(string serial, string dateOfIssue, string whoIssued)
    {
        Serial = serial;
        DateOfIssue = dateOfIssue;
        WhoIssued = whoIssued;
    }
}