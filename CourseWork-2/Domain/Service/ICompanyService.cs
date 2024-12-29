using CourseWork_2.Domain.Models;

namespace CourseWork_2.Domain.Service;

public interface ICompanyService
{
    bool RewardEmployee(Company company, Human human, Reward reward);
    bool PunishEmployee(Company company, Human human, Punishment punishment);
}