namespace CourseWork_2.Domain.Models;

public sealed class CareerMove
{
    private MoveType _move;
    private string _reason;
    private DateTime _date;
    private string _id;
    private string _positionFrom;
    private string _positionTo;

    public CareerMove(MoveType move, string reason, DateTime date, string id, string positionFrom, string positionTo)
    {
        _move = move;
        _reason = reason;
        _date = date;
        _id = id;
        _positionFrom = positionFrom;
        _positionTo = positionTo;
    }

    public MoveType Move => _move;
    public string Reason => _reason;
    public DateTime Date => _date;
    public string Id => _id;
    public string PositionFrom => _positionFrom;
    public string PositionTo => _positionTo;

    public enum MoveType
    {
        None,
        Promotion,
        Demotion,
        Invite,
        Fire
    }
}