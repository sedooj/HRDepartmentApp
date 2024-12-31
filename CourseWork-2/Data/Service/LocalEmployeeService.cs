using System.Diagnostics;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;

namespace CourseWork_2.Data.Service;

public class LocalEmployeeService : IEmployeeService
{
    private readonly LocalStorageService<Employee> _storage = new();

    public void SaveEmployee(Employee employee)
    {
        _storage.SaveEntity($"employee/{employee.UUID}", employee);
        Debug.WriteLine("Employee saved successfully.");
    }

    public Employee? GetEmployeeByUuid(string uuid)
    {
        try
        {
            return _storage.LoadEntity($"employee/{uuid}");
        }
        catch (Exception e)
        {
            Console.WriteLine("Can't find an employee, returning null.", e);
            return null;
        }
    }
    
    public void UpdateEmployee(Employee updatedEmployee)
    {
        var existingEmployee = GetEmployeeByUuid(updatedEmployee.UUID);
        if (existingEmployee == null)
        {
            SaveEmployee(updatedEmployee);
            return;
        }
        _storage.SaveEntity($"employee/{updatedEmployee.UUID}", updatedEmployee);
        Debug.WriteLine("Employee updated successfully.");

    }
}