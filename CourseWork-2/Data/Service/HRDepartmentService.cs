using System.Diagnostics;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Service;

public class HrDepartmentService : IHrDepartment
{
    private readonly IStorage<Company> _companyStorageService = new LocalStorageService<Company>();
    private readonly IStorage<Human> _humanStorageService = new LocalStorageService<Human>();


    public void InviteEmployee(Company company, Guid humanUuid, string position)
    {
        company.EmployeeUUIDs.Add(humanUuid);
        _companyStorageService.UpdateEntity($"{Config.CompanyStoragePath}{company.Id.ToString()}", company);
        var human = _humanStorageService.LoadEntity($"{Config.HumanStoragePath}{humanUuid.ToString()}");
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
            companyId: company.Id,
            companyName: company.Name,
            fireReason: "",
            positionAtWork: position,
            rewards: new List<Reward>(),
            careerMoves: new List<CareerMove>
            {
                new(CareerMove.MoveType.Invite, "Устройство на работу", DateTime.Now, Guid.NewGuid(),
                    "", position)
            },
            startEmploymentPosition: position,
            punishments: new List<Punishment>()));
        human.EmploymentHistoryRecords = employmentHistory;
        _humanStorageService.UpdateEntity($"{Config.HumanStoragePath}{human.Uuid.ToString()}", human);
    }

    public void FireEmployee(Company company, Guid humanUuid, string fireReason)
    {
        company.EmployeeUUIDs.Remove(humanUuid);
        _companyStorageService.UpdateEntity($"{Config.CompanyStoragePath}{company.Id.ToString()}", company);
        var human = _humanStorageService.LoadEntity($"{Config.HumanStoragePath}{humanUuid.ToString()}");
        if (human == null)
        {
            Debug.WriteLine("FireEmployee > Human is null in ");
            return;
        }

        human.LastEmploymentHistoryRecord!.WorkingEndDate = DateTime.Now;
        human.LastEmploymentHistoryRecord!.FireReason = fireReason;
        human.LastEmploymentHistoryRecord!.CareerMoves.Append(new(CareerMove.MoveType.Invite, fireReason, DateTime.Now,
            Guid.NewGuid(),
            human.LastEmploymentHistoryRecord.PositionAtWork, ""));
        _humanStorageService.UpdateEntity($"{Config.HumanStoragePath}{humanUuid.ToString()}", human);
    }

    public List<EmploymentHistoryRecord> GetEmployeeWorkbook(Human human)
    {
        var companies = _companyStorageService.LoadEntities("SavedCompanies");
        foreach (var company in companies)
        {
            var employee = company.EmployeeUUIDs.FirstOrDefault(e => e == human.Uuid);
            var employeeEntity = _humanStorageService.LoadEntity(human.Uuid.ToString());
            if (employeeEntity == null) return new List<EmploymentHistoryRecord>();
            return employeeEntity.EmploymentHistoryRecords;
        }

        return new List<EmploymentHistoryRecord>();
    }
}