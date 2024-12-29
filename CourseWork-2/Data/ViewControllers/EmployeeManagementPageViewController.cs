using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.ViewControllers
{
    public class EmployeeManagementPageViewController
    {
        private readonly LocalStorageService<Company> _companyStorageService = new();
        private readonly LocalStorageService<Human> _humanStorageService = new();
        private readonly IHRDepartment _hrDepartmentService = new HRDepartmentService();

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
            return company.Employees.Any(e => e.UUID == human.UUID);
        }

        public void InviteEmployee(string position)
        {
            if (SelectedCompany != null && SelectedHuman != null && !IsEmployee(SelectedCompany, SelectedHuman))
            {
                _hrDepartmentService.InviteEmployee(SelectedCompany, SelectedHuman, position);
            }
        }

        public void FireEmployee(Company company, Employee employee)
        {
            if (IsEmployee(company, employee))
            {
                _hrDepartmentService.FireEmployee(company, employee);
            }
        }

        private void SaveCompany(Company company)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "companies");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, $"{company.Name}.json");

            string json = new JsonObjectSerializer().Serialize(company);

            File.WriteAllText(filePath, json);
        }
    }
}