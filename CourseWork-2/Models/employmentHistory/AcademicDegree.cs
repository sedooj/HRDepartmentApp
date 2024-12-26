namespace CourseWork.entity.employmentHistory;

public class AcademicDegree
{
    public int Id { get; }
    public string Name { get; }

    public AcademicDegree(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static AcademicDegree DoctorOfScience => new AcademicDegree(1, "Doctor of Science");
    public static AcademicDegree CandidateOfScience => new AcademicDegree(2, "Candidate of Science");
    public static AcademicDegree NoDegree => new AcademicDegree(3, "No Degree");
}