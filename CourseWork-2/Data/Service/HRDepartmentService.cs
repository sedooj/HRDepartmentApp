using System.Diagnostics;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Service;

public class HrDepartmentService : IHrDepartment
{
    private readonly LocalStorageService<Company> _companyStorageService = new();
    private readonly LocalStorageService<Human> _humanStorageService = new();
    private readonly LocalEmployeeService _employeeService = new();


    public void InviteEmployee(Company company, Human human, string position)
    {
        company.EmployeeUUIDs.Add(human.UUID);
        _companyStorageService.UpdateEntity($"companies/{company.Name}", company);

        var employmentHistory = human.EmploymentHistoryRecords;
        employmentHistory.Add(new EmploymentHistoryRecord(
            degree: EmploymentHistoryRecord.AcademicDegree.NoDegree,
            rank: EmploymentHistoryRecord.AcademicRank.NoRank,
            workingEndDate: null,
            workingStartDate: DateTime.Now,
            companyUuid: company.UUID,
            fireReason: "",
            positionAtWork: position,
            rewards: new List<Reward>(),
            careerMoves: new List<CareerMove>(),
            startEmploymentPosition: position,
            punishments: new List<Punishment>()));
        _humanStorageService.UpdateEntity($"humans/{human.UUID}", human);
        
        Debug.WriteLine(employmentHistory.Count);

        var existingEmployee = _employeeService.GetEmployeeByUuid(human.UUID);
        if (existingEmployee != null)
        {
            Debug.WriteLine("Employee has been found, updating");
            existingEmployee.EmploymentHistoryRecords = employmentHistory;
            existingEmployee.Position = position;
            _employeeService.UpdateEmployee(existingEmployee);
        }
        else
        {
            Debug.WriteLine("Employee not found, creating new");
            _employeeService.SaveEmployee(new Employee(
                uuid: human.UUID,
                companyUuid: company.UUID,
                passport: human.Passport,
                userDefaultCredentials: human.UserDefaultCredentials,
                employmentHistoryRecords: employmentHistory,
                education: human.Education,
                educationDocument: human.EducationDocument,
                position: position));
        }
    }
    
    public void FireEmployee(Company company, Human human, string fireReason)
    {
        var employee = company.EmployeeUUIDs.FirstOrDefault(e => e == human.UUID);
        if (employee == null) return;
        company.EmployeeUUIDs.Remove(employee);
        Console.WriteLine("Firing employee");
        SaveCompany(company);
        var employeeByUuid = _employeeService.GetEmployeeByUuid(human.UUID);
        if (employeeByUuid == null) return;
        List<EmploymentHistoryRecord> historyRecords = employeeByUuid.EmploymentHistoryRecords;
        var employmentHistoryRecord = historyRecords.Last();
        employmentHistoryRecord.WorkingEndDate = DateTime.Now;
        employmentHistoryRecord.FireReason = fireReason;
        _employeeService.SaveEmployee(employeeByUuid);
    }

    public List<EmploymentHistoryRecord> GetEmployeeWorkbook(Human human)
    {
        var companies = _companyStorageService.LoadEntities("SavedCompanies");
        foreach (var company in companies)
        {
            var employee = company.EmployeeUUIDs.FirstOrDefault(e => e == human.UUID);
            if (employee != null)
            {
                var employeeEntity = _employeeService.GetEmployeeByUuid(human.UUID);
                if (employeeEntity == null) return new List<EmploymentHistoryRecord>();
                return employeeEntity.EmploymentHistoryRecords;
            }
        }

        return new List<EmploymentHistoryRecord>();
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