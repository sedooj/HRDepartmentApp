using CourseWork_2.Domain.Models;

namespace CourseWork_2.Domain.Service;

public interface IHrDepartment
{
    void InviteEmployee(Company company, string humanUuid, string position);

    void FireEmployee(Company company, string humanUuid, string fireReason);
    List<EmploymentHistoryRecord> GetEmployeeWorkbook(Human human);
}