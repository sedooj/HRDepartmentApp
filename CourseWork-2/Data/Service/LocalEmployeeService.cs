using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;

namespace CourseWork_2.Data.Service;

public class LocalEmployeeService : IEmployeeService
{
    private readonly LocalStorageService<Employee> _storage = new();

    public void SaveEmployee(Employee employee)
    {
        _storage.SaveEntity($"employee/{employee.UUID}", employee);
    }

    public Employee? GetEmployeeByUUID(string uuid)
    {
        return _storage.LoadEntity($"employee/{uuid}");
    }
    
}