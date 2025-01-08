using CourseWork_2.Domain.Models;

namespace CourseWork_2.Domain.Service
{
    public interface ICompanyService
    {
        bool RewardEmployee(Guid employeeUuid, Reward reward);
        bool PunishEmployee(Guid employeeUuid, Punishment punishment);
        bool PromoteEmployee(Guid employeeUuid, string newPosition, string reason);
        bool DemoteEmployee(Guid employeeUuid, string newPosition, string reason);
    }
}