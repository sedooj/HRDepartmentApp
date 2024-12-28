using CourseWork_2.Domain.Models;

namespace CourseWork_2.Domain.Service;

public interface ICompanyService
{   
    public bool InviteEmployee(string serial, string number);
    public bool DismissEmployeeById(long employeeId);
    public Employee FindEmployeeById(long employeeId);
}