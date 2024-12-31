using CourseWork_2.Domain.Models;
using CourseWork_2.Data.Service;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeeDetailsPage
    {
        private readonly LocalStorageService<Company> _companyService = new();

        public EmployeeDetailsPage(EmploymentHistoryRecord record)
        {
            InitializeComponent();

            var company = _companyService.LoadEntity(record.CompanyUuid);
            record.CompanyUuid = company == null ? "–" : company.Name;
            BindingContext = record;
        }
    }

}