using CourseWork_2.Domain.Models;

namespace CourseWork_2.Domain.Service;

public interface IHrDepartment
{
    void InviteEmployee(Company company, Guid humanUuid, string position);

    void FireEmployee(Company company, Guid humanUuid, string fireReason);
    List<EmploymentHistoryRecord> GetEmployeeWorkbook(Human human);
}