using CourseWork_2.Domain.Models;

namespace CourseWork_2.Domain.Service;

public interface IHRDepartment
{
    void InviteEmployee(Company company, Human human, string position);
    void FireEmployee(Company company, Human human);
    List<EmploymentHistoryRecord> GetEmployeeWorkbook(Human human);
}