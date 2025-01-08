using CourseWork_2.Domain.Models;
using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeeDetailsPage
    {
        private readonly IStorage<Company> _companyService = new LocalStorageService<Company>();
    
        public List<Reward> Rewards { get; set; }
        public List<Punishment> Punishments { get; set; }

        public EmployeeDetailsPage(EmploymentHistoryRecord record)
        {
            InitializeComponent();
            var company = _companyService.LoadEntity($"{Config.CompanyStoragePath}{record.CompanyUuid}");
            record.CompanyUuid = company == null ? "–" : company.Name;
            Rewards = record.Rewards;
            Punishments = record.Punishments;
            BindingContext = record;
        }
    }

}