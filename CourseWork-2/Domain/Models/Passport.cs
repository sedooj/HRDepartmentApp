namespace CourseWork_2.Domain.Models;

public sealed class Passport
{
    private string _serial;
    private string _number;
    private DateTime _dateOfIssue;
    private string _whoIssued;

    public Passport(string serial, string number, DateTime dateOfIssue, string whoIssued)
    {
        _serial = serial;
        _number = number;
        _dateOfIssue = dateOfIssue;
        _whoIssued = whoIssued;
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

    public DateTime DateOfIssue
    {
        get { return _dateOfIssue; }
        set { _dateOfIssue = value; }
    }

    public string WhoIssued
    {
        get { return _whoIssued; }
        set { _whoIssued = value; }
    }
}