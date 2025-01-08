using System.Diagnostics;
using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Pages.EmployeeManagement;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Controllers
{
    public class EmployeeManagementPageController
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


        public bool IsEmployee(Company company, Guid humanUuid)
        {
            return company.EmployeeUUIDs.Contains(humanUuid);
        }

        public void InviteEmployee(Entry position, Button inviteButton, Entry positionEntry,
            CollectionView employeesCollectionView, Picker humanPicker)
        {
            if (SelectedCompany == null || SelectedHuman == null ||
                IsEmployee(SelectedCompany, SelectedHuman.Uuid)) return;
            _hrDepartmentService.InviteEmployee(SelectedCompany, SelectedHuman.Uuid, position.Text);
            SelectedHuman = null;
            LoadData();
            UpdateInviteButtonVisibility(inviteButton, positionEntry);
            LoadEmployees(employeesCollectionView);
            humanPicker.SelectedIndex = -1;
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

        public void FireEmployee(Company company, Guid employee, string fireReason)
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

        private void LoadHumans(Picker humansPicker)
        {
            humansPicker.ItemsSource = Humans
                ?.Select(h => h.UserDefaultCredentials.FirstName + " " + h.UserDefaultCredentials.LastName).ToList();
        }

        private void LoadCompanies(Picker companyPicker)
        {
            var companyNames = Companies?.Select(c => c.Name).ToList();
            companyPicker.ItemsSource = companyNames;
        }

        public void UpdatePickers(Picker companyPicker, Picker humansPicker)
        {
            LoadCompanies(companyPicker);
            LoadHumans(humansPicker);
        }

        public void LoadEmployees(CollectionView employeesCollectionView)
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
                employeesCollectionView.ItemsSource = employees;
            }
        }

        public void UpdateInviteButtonVisibility(Button inviteButton, Entry positionEntry)
        {
            bool isVisible = SelectedHuman != null &&
                             SelectedCompany != null &&
                             !IsEmployee(SelectedCompany, SelectedHuman.Uuid);

            inviteButton.IsVisible = isVisible;
            positionEntry.IsVisible = isVisible;
        }

        public void OnCompanySelect(Picker companyPicker, Picker humanPicker, Frame tableFrame,
            Label companyEmployeesLabel, Button inviteButton, Entry positionEntry,
            CollectionView employeesCollectionView)
        {
            SelectedCompany = Companies?[companyPicker.SelectedIndex];
            bool isCompanySelected = SelectedCompany != null;
            tableFrame.IsVisible = isCompanySelected;
            companyEmployeesLabel.IsVisible = isCompanySelected;
            UpdateInviteButtonVisibility(inviteButton, positionEntry);
            UpdatePickers(companyPicker, humanPicker);
            LoadEmployees(employeesCollectionView);
        }

        public void OnHumanSelect(Button inviteButton, Entry positionEntry, Picker humanPicker)
        {
            if (humanPicker.SelectedIndex >= 0 && humanPicker.SelectedIndex < Humans?.Count)
            {
                SelectedHuman = Humans?[humanPicker.SelectedIndex];
                UpdateInviteButtonVisibility(inviteButton, positionEntry);
            }
        }

        public void OnAppearing(Picker companyPicker, Picker humanPicker, Button inviteButton, Entry positionEntry,
            CollectionView employeesCollectionView)
        {
            LoadData();
            UpdatePickers(companyPicker, humanPicker);
            LoadEmployees(employeesCollectionView);
            UpdateInviteButtonVisibility(inviteButton, positionEntry);
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
    }
}