using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;

namespace CourseWork_2.Data.ViewModels
{
    public class EmployeeManagementPageViewModel
    {
        private readonly IStorage<Company> _companyStorageService = new LocalStorageService<Company>();
        private readonly IStorage<Human> _humanStorageService = new LocalStorageService<Human>();
        private readonly IHrDepartment _hrDepartmentService = new HrDepartmentService();
        private readonly ICompanyService _companyService = new LocalCompanyService();

        private List<Company>? _companies;
        private List<Human>? _humans;
        private Human? _selectedHuman;
        private Company? _selectedCompany;


        public List<Company>? Companies
        {
            get => _companies;
            set { _companies = value; }
        }

        public List<Human>? Humans
        {
            get => _humans;
            set { _humans = value; }
        }

        public Company? SelectedCompany
        {
            get => _selectedCompany;
            set { _selectedCompany = value; }
        }

        public Human? SelectedHuman
        {
            get => _selectedHuman;
            set { _selectedHuman = value; }
        }

        public void LoadData()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string companyDirectoryPath = Path.Combine(documentsPath, "companies");
            string humanDirectoryPath = Path.Combine(documentsPath, "humans");

            Companies = _companyStorageService.LoadEntities(companyDirectoryPath).ToList();
            Humans = _humanStorageService.LoadEntities(humanDirectoryPath).ToList();
        }

        public bool IsEmployee(Company company, string humanUuid)
        {
            return company.EmployeeUUIDs.Contains(humanUuid);
        }

        public void InviteEmployee(string position)
        {
            if (SelectedCompany != null && SelectedHuman != null && !IsEmployee(SelectedCompany, SelectedHuman.Uuid))
            {
                _hrDepartmentService.InviteEmployee(SelectedCompany, SelectedHuman.Uuid, position);
                LoadData();
            }
        }

        public void PromoteEmployee(string employeeUuid, string newPosition, string reason)
        {
            var promotion = _companyService.PromoteEmployee(employeeUuid, newPosition, reason);
            if (promotion) GiveReward(SelectedHuman!, new Reward(id: Guid.NewGuid().ToString(), Reward.RewardType.Promotion, DateTime.Now, reason));
            LoadData();
        }
        
        public void DemoteEmployee(string employeeUuid, string newPosition, string reason)
        {
            _companyService.DemoteEmployee(employeeUuid, newPosition, reason);
            LoadData();
        }

        public void FireEmployee(Company company, string employee, string fireReason)
        {
            if (!IsEmployee(company, employee)) return;
            _hrDepartmentService.FireEmployee(company, employee, fireReason);
            LoadData();
        }

        public void GiveReward(Human employee, Reward reward)
        {
            try
            {
                if (SelectedCompany != null)
                {
                    bool result = _companyService.RewardEmployee(employee.Uuid, reward);
                    if (result)
                    {
                        Console.WriteLine("Reward successfully given to employee.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to give reward to employee.");
                    }
                }
                else
                {
                    Console.WriteLine("SelectedCompany is null.");
                }

                LoadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GiveReward: {ex.Message}");
            }
        }

        public void PunishEmployee(Human employee, Punishment punishment)
        {
            try
            {
                if (SelectedCompany != null)
                {
                    if (punishment.Type == Punishment.PunishmentType.Fire)
                    {
                        FireEmployee(SelectedCompany, employee.Uuid, punishment.Reason);
                        return;
                    }
                    else
                    {
                        bool result = _companyService.PunishEmployee(employee.Uuid, punishment);
                        if (result)
                        {
                            Console.WriteLine("Punishment successfully given to employee.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to give punishment to employee.");
                        }
                    }

                    LoadData();
                }
                else
                {
                    Console.WriteLine("SelectedCompany is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PunishEmployee: {ex.Message}");
            }
        }
    }
}