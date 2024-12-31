using CourseWork_2.Domain.Models;

namespace CourseWork_2.Domain.Service;

public interface ICompanyService
{
    bool RewardEmployee(string employeeUuid, Reward reward);
    bool PunishEmployee(string employeeUuid, Punishment punishment);
}