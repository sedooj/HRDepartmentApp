namespace CourseWork_2.Domain.Models;

public class Education
{
    public Guid DocumentId { get; }
    public string Institution { get; }
    public DateTime GraduatedDate { get; }
    public string Specialty { get; }

    public Education(Guid documentId, string institution, DateTime graduatedDate, string specialty)
    {
        DocumentId = documentId;
        Institution = institution;
        GraduatedDate = graduatedDate;
        Specialty = specialty;
    }
}