using System.Diagnostics;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Service;

public class HrDepartmentService : IHrDepartment
{
    private readonly IStorage<Company> _companyStorageService = new LocalStorageService<Company>();
    private readonly IStorage<Human> _humanStorageService = new LocalStorageService<Human>();


    public void InviteEmployee(Company company, string humanUuid, string position)
    {
        company.EmployeeUUIDs.Add(humanUuid);
        _companyStorageService.UpdateEntity($"{Config.CompanyStoragePath}{company.Id}", company);
        var human = _humanStorageService.LoadEntity($"{Config.HumanStoragePath}{humanUuid}");
        if (human == null)
        {
            Debug.WriteLine("InviteEmployee > Human is null");
            return;
        }
        var employmentHistory = new List<EmploymentHistoryRecord>(human.EmploymentHistoryRecords);
        employmentHistory.Add(new EmploymentHistoryRecord(
            degree: EmploymentHistoryRecord.AcademicDegree.NoDegree,
            rank: EmploymentHistoryRecord.AcademicRank.NoRank,
            workingEndDate: null,
            workingStartDate: DateTime.Now,
            companyUuid: company.Id,
            fireReason: "",
            positionAtWork: position,
            rewards: new List<Reward>(),
            careerMoves: new List<CareerMove>{new(CareerMove.MoveType.Invite, "Устройство на работу", DateTime.Now, Guid.NewGuid().ToString(),
                "", position)},
            startEmploymentPosition: position,
            punishments: new List<Punishment>()));
        human.EmploymentHistoryRecords = employmentHistory;
        _humanStorageService.UpdateEntity($"{Config.HumanStoragePath}{human.Uuid}", human);
    }

    public void FireEmployee(Company company, string humanUuid, string fireReason)
    {
        company.EmployeeUUIDs.Remove(humanUuid);
        _companyStorageService.UpdateEntity($"{Config.CompanyStoragePath}{company.Id}", company);
        var human = _humanStorageService.LoadEntity($"{Config.HumanStoragePath}{humanUuid}");
        if (human == null)
        {
            Debug.WriteLine("FireEmployee > Human is null in ");
            return;
        }
        human.EmploymentHistoryRecords.Last().WorkingEndDate = DateTime.Now;
        human.EmploymentHistoryRecords.Last().FireReason = fireReason;
        human.EmploymentHistoryRecords.Last().CareerMoves = new List<CareerMove>(human.EmploymentHistoryRecords.Last().CareerMoves)
        {
            new(CareerMove.MoveType.Invite, fireReason, DateTime.Now, Guid.NewGuid().ToString(),
                human.EmploymentHistoryRecords.Last().PositionAtWork, "")
        };
        _humanStorageService.UpdateEntity($"{Config.HumanStoragePath}{humanUuid}", human);
    }

    public List<EmploymentHistoryRecord> GetEmployeeWorkbook(Human human)
    {
        var companies = _companyStorageService.LoadEntities("SavedCompanies");
        foreach (var company in companies)
        {
            var employee = company.EmployeeUUIDs.FirstOrDefault(e => e == human.Uuid);
            if (employee != null)
            {
                var employeeEntity = _humanStorageService.LoadEntity(human.Uuid);
                if (employeeEntity == null) return new List<EmploymentHistoryRecord>();
                return employeeEntity.EmploymentHistoryRecords;
            }
        }

        return new List<EmploymentHistoryRecord>();
    }
}