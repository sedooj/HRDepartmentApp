using System.Diagnostics;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Service;

public class HrDepartmentService : IHrDepartment
{
    private readonly LocalStorageService<Company> _companyStorageService = new();
    private readonly LocalEmployeeService _employeeService = new();


    public void InviteEmployee(Company company, Human human, string position)
    {
        if (!IsEmployee(company, human))
        {
            company.EmployeeUUIDs.Add(human.UUID);
            SaveCompany(company);

            var employmentHistory = human.EmploymentHistoryRecords;
            Debug.WriteLine(employmentHistory.Count);

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
            Debug.WriteLine(employmentHistory.Count);
            human.EmploymentHistoryRecords = employmentHistory;

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
        var employeeByUuid = _employeeService.GetEmployeeByUUID(human.UUID);
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
                var employeeEntity = _employeeService.GetEmployeeByUUID(human.UUID);
                if (employeeEntity == null) return new List<EmploymentHistoryRecord>();
                return employeeEntity.EmploymentHistoryRecords;
            }
        }

        return new List<EmploymentHistoryRecord>();
    }

    private bool IsEmployee(Company company, Human human)
    {
        return company.EmployeeUUIDs.Any(e => e == human.UUID);
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