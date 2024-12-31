using CourseWork_2.Domain.Models;
using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Service;

namespace CourseWork_2.Data.ViewControllers
{
    public class EmployeeManagementPageViewController
    {
        private readonly LocalStorageService<Company> _companyStorageService = new();
        private readonly LocalStorageService<Human> _humanStorageService = new();
        private readonly IHrDepartment _hrDepartmentService = new HrDepartmentService();
        private readonly ICompanyService _companyService = new LocalCompanyService();

        public List<Company>? Companies { get; private set; }
        public List<Human>? Humans { get; private set; }
        public Company? SelectedCompany { get; set; }
        public Human? SelectedHuman { get; set; }

        public void LoadData()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string companyDirectoryPath = Path.Combine(documentsPath, "companies");
            string humanDirectoryPath = Path.Combine(documentsPath, "humans");

            Companies = _companyStorageService.LoadEntities(companyDirectoryPath).ToList();
            Humans = _humanStorageService.LoadEntities(humanDirectoryPath).ToList();
        }

        public bool IsEmployee(Company company, Human human)
        {
            return company.EmployeeUUIDs.Contains(human.UUID);
        }

        public void InviteEmployee(string position)
        {
            if (SelectedCompany != null && SelectedHuman != null && !IsEmployee(SelectedCompany, SelectedHuman))
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string employeeDirectoryPath = Path.Combine(documentsPath, "employee");

                // Ensure the directory exists
                if (!Directory.Exists(employeeDirectoryPath))
                {
                    Directory.CreateDirectory(employeeDirectoryPath);
                }

                _hrDepartmentService.InviteEmployee(SelectedCompany, SelectedHuman, position);
            }
        }

        public void FireEmployee(Company company, Employee employee, string fireReason)
        {
            if (IsEmployee(company, employee))
            {
                _hrDepartmentService.FireEmployee(company, employee, fireReason);
                LoadData();
            }
        }

        public void GiveReward(Employee employee, Reward reward)
        {
            try
            {
                if (SelectedCompany != null)
                {
                    bool result = _companyService.RewardEmployee(SelectedCompany, employee, reward);
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GiveReward: {ex.Message}");
            }
        }
    }
}