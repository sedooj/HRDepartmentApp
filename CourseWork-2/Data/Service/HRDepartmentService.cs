using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Service;

public class HRDepartmentService : IHRDepartment
{
    private readonly LocalStorageService<Company> _companyStorageService = new();

    public void InviteEmployee(Company company, Human human, string position)
    {
        if (!IsEmployee(company, human))
        {
            company.Employees.Add(new Employee(uuid: human.UUID,
                passport: human.Passport,
                userDefaultCredentials: human.UserDefaultCredentials,
                employmentHistoryRecords: new List<EmploymentHistoryRecord>(),
                education: human.Education,
                educationDocument: human.EducationDocument,
                position: position
            ));
            SaveCompany(company);
        }
    }

    public void FireEmployee(Company company, Human human)
    {
        var employee = company.Employees.FirstOrDefault(e => e.UUID == human.UUID);
        if (employee == null) return;
        company.Employees.Remove(employee);
        SaveCompany(company);
    }

    public List<EmploymentHistoryRecord> GetEmployeeWorkbook(Human human)
    {
        var companies = _companyStorageService.LoadEntities("SavedCompanies");
        foreach (var company in companies)
        {
            var employee = company.Employees.FirstOrDefault(e => e.UUID == human.UUID);
            if (employee != null)
            {
                return employee.EmploymentHistoryRecords;
            }
        }
        return new List<EmploymentHistoryRecord>();
    }

    private bool IsEmployee(Company company, Human human)
    {
        return company.Employees.Any(e => e.UUID == human.UUID);
    }

    private void SaveCompany(Company company)
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string directoryPath = Path.Combine(documentsPath, "companies");

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = Path.Combine(directoryPath, $"{company.Name}.json");

        string json = new JsonObjectSerializer().Serialize(company);

        File.WriteAllText(filePath, json);
    }
}