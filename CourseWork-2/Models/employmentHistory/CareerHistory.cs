namespace CourseWork.entity.employmentHistory;

public class CareerHistory
{
    private List<CareerMove> _careerMoves;

    public List<CareerMove> CareerMoves => _careerMoves;

    public CareerHistory(List<CareerMove> careerMoves)
    {
        _careerMoves = careerMoves ?? new List<CareerMove>();
    }

    public void AddCareerMove(CareerMove careerMove)
    {
        _careerMoves.Add(careerMove);
    }
}