namespace CourseWork.entity;

public class Education
{
    public long DocumentId { get; }
    public string Institution { get; }
    public string GraduatedDate { get; }
    public string Specialty { get; }

    public Education(long documentId, string institution, string graduatedDate, string specialty)
    {
        DocumentId = documentId;
        Institution = institution;
        GraduatedDate = graduatedDate;
        Specialty = specialty;
    }
}