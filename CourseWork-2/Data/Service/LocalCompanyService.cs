using System.Diagnostics;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Service
{
    public class LocalCompanyService : ICompanyService
    {
        private readonly IStorage<Human> _humanService = new LocalStorageService<Human>();

        public bool RewardEmployee(string employeeUuid, Reward reward)
        {
            var employee = _humanService.LoadEntity($"{Config.HumanStoragePath}{employeeUuid}");
            if (employee == null)
            {
                Debug.WriteLine("Can't give reward to employee: employee not found");
                return false;
            }

            employee.EmploymentHistoryRecords.Last().Rewards.Add(reward);
            _humanService.SaveEntity($"{Config.HumanStoragePath}{employee.Uuid}", employee);
            return true;
        }

        public bool PunishEmployee(string humanUuid, Punishment punishment)
        {
            var employee = _humanService.LoadEntity($"{Config.HumanStoragePath}{humanUuid}");
            if (employee == null)
            {
                Debug.WriteLine("Can't punish employee: employee not found");
                return false;
            }
            employee.EmploymentHistoryRecords.Last().Punishments.Add(punishment);
            _humanService.UpdateEntity($"{Config.HumanStoragePath}{humanUuid}", employee);
            return true;
        }

        public bool PromoteEmployee(string employeeUuid, string newPosition)
        {
            var employee = _humanService.LoadEntity($"{Config.HumanStoragePath}{employeeUuid}");
            if (employee == null)
            {
                Debug.WriteLine("Can't promote employee: employee not found");
                return false;
            }
            employee.EmploymentHistoryRecords.Last().PositionAtWork = newPosition;
            _humanService.UpdateEntity($"{Config.HumanStoragePath}{employee.Uuid}", employee);
            Debug.WriteLine("Promotion success");
            return true;
        }
        
        public bool DemoteEmployee(string employeeUuid, string newPosition, string reason)
        {
            var employee = _humanService.LoadEntity($"{Config.HumanStoragePath}{employeeUuid}");
            if (employee == null)
            {
                Debug.WriteLine("Can't demote employee: employee not found");
                return false;
            }

            var punishment = new Punishment(id: Guid.NewGuid().ToString(), Punishment.PunishmentType.Demotion, DateTime.Now, reason);
            employee.EmploymentHistoryRecords.Last().Punishments.Add(punishment);
            employee.EmploymentHistoryRecords.Last().PositionAtWork = newPosition;
            _humanService.SaveEntity($"{Config.HumanStoragePath}{employee.Uuid}", employee);
            return true;
        }
    }
}