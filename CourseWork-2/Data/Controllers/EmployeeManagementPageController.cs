using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Controllers
{
    public class EmployeeManagementPageController
    {
        private readonly IStorage<Company> _companyStorageService = new LocalStorageService<Company>();
        private readonly IStorage<Human> _humanStorageService = new LocalStorageService<Human>();
        private readonly IHrDepartment _hrDepartmentService = new HrDepartmentService();
        private readonly ICompanyService _companyService = new LocalCompanyService();

        public required PageComponents Components { get; set; }

        private List<Company>? Companies { get; set; }

        private List<Human>? Humans { get; set; }

        private Company? SelectedCompany { get; set; }

        public Human? SelectedHuman { get; set; }

        public void LoadData()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string companyDirectoryPath = Path.Combine(documentsPath, "companies");
            string humanDirectoryPath = Path.Combine(documentsPath, "humans");

            Companies = _companyStorageService.LoadEntities(companyDirectoryPath).ToList();
            Humans = _humanStorageService.LoadEntities(humanDirectoryPath).ToList();
        }


        private static bool IsEmployee(Company company, Guid humanUuid)
        {
            return company.EmployeeUUIDs.Contains(humanUuid);
        }

        public void InviteEmployee()
        {
            if (SelectedCompany == null || SelectedHuman == null ||
                IsEmployee(SelectedCompany, SelectedHuman.Uuid)) return;
            _hrDepartmentService.InviteEmployee(SelectedCompany, SelectedHuman.Uuid, Components.PositionEntry.Text);
            LoadData();
            UpdateInviteButtonVisibility();
            LoadEmployees();
            SelectedHuman = null;
            Components.HumanPicker.SelectedIndex = -1;
        }

        public void PromoteEmployee(Guid employeeUuid, string newPosition, string reason)
        {
            var promotion = _companyService.PromoteEmployee(employeeUuid, newPosition, reason);
            if (promotion)
                GiveReward(SelectedHuman!,
                    new Reward(id: Guid.NewGuid().ToString(), Reward.RewardType.Promotion, DateTime.Now, reason));
            LoadData();
        }

        public void DemoteEmployee(Guid employeeUuid, string newPosition, string reason)
        {
            _companyService.DemoteEmployee(employeeUuid, newPosition, reason);
            LoadData();
        }

        private void FireEmployee(Company company, Guid employee, string fireReason)
        {
            if (IsEmployee(company, employee)) return;
            _hrDepartmentService.FireEmployee(company, employee, fireReason);
            SelectedHuman = null;
            Components.HumanPicker.SelectedIndex = -1;
            LoadData();
        }

        public void GiveReward(Human employee, Reward reward)
        {
            _companyService.RewardEmployee(employee.Uuid, reward);
        }

        public void PunishEmployee(Human employee, Punishment punishment)
        {
            if (SelectedCompany == null) return;
            if (punishment.Type == Punishment.PunishmentType.Fire)
            {
                FireEmployee(SelectedCompany, employee.Uuid, punishment.Reason);
                return;
            }

            _companyService.PunishEmployee(employee.Uuid, punishment);
            LoadData();
        }

        private void LoadHumans()
        {
            Components.HumanPicker.ItemsSource = Humans
                ?.Select(h => h.UserDefaultCredentials.FirstName + " " + h.UserDefaultCredentials.LastName).ToList();
        }

        private void LoadCompanies()
        {
            var companyNames = Companies?.Select(c => c.Name).ToList();
            Components.CompanyPicker.ItemsSource = companyNames;
        }

        public void UpdatePickers()
        {
            LoadCompanies();
            LoadHumans();
        }

        private void LoadEmployees()
        {
            if (SelectedCompany != null)
            {
                var employees = SelectedCompany.EmployeeUUIDs.Select((id, index) =>
                {
                    var employee = _humanStorageService.LoadEntity($"{Config.HumanStoragePath}{id.ToString()}");
                    return new
                    {
                        Number = index + 1,
                        Name = employee?.UserDefaultCredentials.FirstName + " " +
                               employee?.UserDefaultCredentials.LastName,
                        Position = employee?.LastEmploymentHistoryRecord?.PositionAtWork,
                        Id = id
                    };
                }).ToList();
                Components.EmployeesCollectionView.ItemsSource = employees;
            }
        }

        private void UpdateInviteButtonVisibility()
        {
            bool isVisible = SelectedHuman != null &&
                             SelectedCompany != null &&
                             !IsEmployee(SelectedCompany, SelectedHuman.Uuid);

            Components.InviteButton.IsVisible = isVisible;
            Components.PositionEntry.IsVisible = isVisible;
        }

        public void OnCompanySelect()
        {
            SelectedCompany = Companies?[Components.CompanyPicker.SelectedIndex];
            var isCompanySelected = SelectedCompany != null;
            Components.TableFrame.IsVisible = isCompanySelected;
            Components.CompanyEmployeesLabel.IsVisible = isCompanySelected;
            UpdateInviteButtonVisibility();
            UpdatePickers();
            LoadEmployees();
        }

        public void OnHumanSelect()
        {
            if (!(Components.HumanPicker.SelectedIndex >= 0) ||
                !(Components.HumanPicker.SelectedIndex < Humans?.Count)) return;
            SelectedHuman = Humans?[Components.HumanPicker.SelectedIndex];
            UpdateInviteButtonVisibility();
        }

        public void OnAppearing()
        {
            LoadData();
            UpdatePickers();
            LoadEmployees();
            UpdateInviteButtonVisibility();
        }

        public bool OnViewClicked(object sender)
        {
            var button = sender as Button;
            if (button?.CommandParameter is Guid employeeUuid)
            {
                SelectedHuman = _humanStorageService.LoadEntity($"{Config.HumanStoragePath}{employeeUuid.ToString()}");

                if (SelectedHuman == null)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public void InitComponents(Picker companyPicker, Picker humanPicker, Button inviteButton, Entry positionEntry,
            CollectionView employeesCollectionView
            , Frame tableFrame, Label companyEmployeesLabel)
        {
            Components = new PageComponents
            {
                CompanyPicker = companyPicker,
                HumanPicker = humanPicker,
                PositionEntry = positionEntry,
                EmployeesCollectionView = employeesCollectionView,
                TableFrame = tableFrame,
                CompanyEmployeesLabel = companyEmployeesLabel,
                InviteButton = inviteButton
            };
        }
    }

    public class PageComponents
    {
        public required Picker CompanyPicker { get; init; }
        public required Picker HumanPicker { get; init; }
        public required Button InviteButton { get; init; }
        public required Entry PositionEntry { get; init; }
        public required CollectionView EmployeesCollectionView { get; init; }
        public required Frame TableFrame { get; init; }
        public required Label CompanyEmployeesLabel { get; init; }
    }
}