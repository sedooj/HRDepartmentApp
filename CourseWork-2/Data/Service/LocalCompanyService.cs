using System.Diagnostics;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Service;

public class LocalCompanyService : ICompanyService
{
    private readonly LocalStorageService<Company> _storage = new();
    private readonly LocalStorageService<Human> _humanService = new();

    public bool RewardEmployee(Company company, Human employee, Reward reward)
    {
        try
        {
            if (employee.EmploymentHistoryRecords.Count == 0)
            {
                employee.EmploymentHistoryRecords.Last().Rewards.Add(reward);
                Debug.Print("Employee has no employment history");
            }
            else
            {
                employee.EmploymentHistoryRecords.Last().Rewards.Add(reward);
                Debug.Print("Employee has employment history");
            }
            _humanService.SaveEntity($"{Config.HumanStoragePath}{employee.UUID}", employee);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in RewardEmployee: {ex.Message}");
            return false;
        }
    }

    public bool PunishEmployee(Company company, Human human, Punishment punishment)
    {
        var employee = GetEmployee(company, human.UUID);
        if (employee == null) return false;
        employee.EmploymentHistoryRecords.Last().Punishments.Add(punishment);
        SaveCompany(company);
        return true;
    }

    private Human? GetEmployee(Company company, string uuid)
    {
        if (company.EmployeeUUIDs.Exists(e => e == uuid))
        {
            return _humanService.LoadEntity($"{Config.HumanStoragePath}/{uuid}");
        }

        return null;
    }

    private void SaveCompany(Company company)
    {
        _storage.SaveEntity($"companies/{company.Name}", company);
    }
}