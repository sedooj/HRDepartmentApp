using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;

namespace CourseWork_2.Data.Service;

public class LocalCompanyService : ICompanyService
{
    private readonly LocalStorageService<Company> _storage = new();

    public bool RewardEmployee(Company company, Human human, Reward reward)
    {
        var employee = GetEmployee(company, human.UUID);
        if (employee == null) return false;
        employee.EmploymentHistoryRecords.Last().Rewards.Add(reward);
        SaveCompany(company);
        return true;
    }

    public bool PunishEmployee(Company company, Human human, Punishment punishment)
    {
        var employee = GetEmployee(company, human.UUID);
        if (employee == null) return false;
        employee.EmploymentHistoryRecords.Last().Punishments.Add(punishment);
        SaveCompany(company);
        return true;
    }

    private Employee? GetEmployee(Company company, string uuid)
    {
        return company.Employees.FirstOrDefault(e => e.UUID == uuid);
    }

    private void SaveCompany(Company company)
    {
        _storage.SaveEntity($"companies/{company.Name}", company);
    }
}