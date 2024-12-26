namespace CourseWork.entity.employmentHistory;

public class AcademicRank
{
    public int Id { get; }
    public string Name { get; }

    public AcademicRank(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static AcademicRank Docent => new AcademicRank(1, "Docent");
    public static AcademicRank Professor => new AcademicRank(2, "Professor");
    public static AcademicRank NoRank => new AcademicRank(3, "No Rank");
}