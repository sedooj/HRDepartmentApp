using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Models;

namespace CourseWork_2.Data.ViewControllers
{
    public class EmployeeManagementPageViewController
    {
        private readonly LocalStorageService<Company> _companyStorageService = new();
        private readonly LocalStorageService<Human> _humanStorageService = new();

        public async Task<IEnumerable<Company>> LoadCompaniesAsync()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "SavedCompanies");
            return await _companyStorageService.LoadEntitiesAsync(directoryPath);
        }

        public async Task<IEnumerable<Human>> LoadHumansAsync()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "SavedHumans");
            return await _humanStorageService.LoadEntitiesAsync(directoryPath);
        }

        public bool IsEmployee(Company company, Human human)
        {
            // return company.Employees.Any(e => e.Id == human.Id); TODO()
            return false;
        }

        public async Task InviteEmployeeAsync(Company company, Human human)
        {
            // if (!IsEmployee(company, human))
            // {
            //     // company.Employees.Add(human);
            //     await SaveCompanyAsync(company);
            // }
        }

        public async Task DismissEmployeeAsync(Company company, Human human)
        {
            // if (IsEmployee(company, human))
            // {
                // company.Employees.RemoveAll(e => e.Id == human.Id);
                // await SaveCompanyAsync(company);
            // }
        }

        private async Task SaveCompanyAsync(Company company)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "SavedCompanies");
            await _companyStorageService.SaveEntityAsync(directoryPath, company);
        }
    }
}