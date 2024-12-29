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

        public IEnumerable<Company> LoadCompanies()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "companies");
            return _companyStorageService.LoadEntities(directoryPath);
        }

        public IEnumerable<Human> LoadHumans()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "humans");
            return _humanStorageService.LoadEntities(directoryPath);
        }

        public bool IsEmployee(Company company, Human human)
        {
            return company.Employees.Any(e => e.UUID == human.UUID);
        }

        public void InviteEmployee(Company company, Human human, string position)
        {
            if (!IsEmployee(company, human))
            {
                _hrDepartmentService.InviteEmployee(company, human, position);
                SaveCompany(company);
            }
        }

        public void DismissEmployee(Company company, Human human)
        {
            if (IsEmployee(company, human))
            {
                _hrDepartmentService.FireEmployee(company, human);
                SaveCompany(company);
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