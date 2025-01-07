using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Domain.Models;
using System.Diagnostics;
using CourseWork_2.Data.Service;
using CourseWork_2.Data.ViewModels;
using CourseWork_2.Presentation.Util;
using Microsoft.Maui.Animations;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeeManagementPage
    {
        private readonly EmployeeManagementPageViewModel _controller = new();
        private readonly LocalStorageService<Human> _humanStorageService = new();

        public EmployeeManagementPage()
        {
            try
            {
                InitializeComponent();
                _controller.LoadData();
                LoadData();
                Debug.WriteLine("EmployeeManagementPage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing EmployeeManagementPage: {ex}");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                _controller.LoadData();
                LoadData();
                LoadEmployees();
                UpdateButtonsVisibility();
                Debug.WriteLine("OnAppearing executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnAppearing: {ex}");
            }
        }

        private void OnInviteClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("OnInviteClicked started.");
            try
            {
                string position = PositionEntry.Text;
                _controller.InviteEmployee(position);
                _controller.SelectedHuman = null;
                _controller.LoadData();
                LoadData();
                UpdateButtonsVisibility();
                LoadEmployees();
                HumanPicker.SelectedIndex = -1;
                Debug.WriteLine("OnInviteClicked executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnInviteClicked: {ex}");
            }
        }
        
        private void LoadData()
        {
            try
            {
                var companyNames = _controller.Companies?.Select(c => c.Name).ToList();
                CompanyPicker.ItemsSource = companyNames;
                Debug.WriteLine($"Loaded companies: {string.Join(", ", companyNames ?? new List<string>())}");

                HumanPicker.ItemsSource = _controller.Humans?.Select(h => h.UserDefaultCredentials.FirstName).ToList();
                
                Debug.WriteLine("LoadData executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in LoadData: {ex}");
            }
        }

        private void OnCompanySelected(object sender, EventArgs e)
        {
            try
            {
                _controller.SelectedCompany = _controller.Companies?[CompanyPicker.SelectedIndex];
                bool isCompanySelected = _controller.SelectedCompany != null;
                TableFrame.IsVisible = isCompanySelected;
                CompanyEmployeesLabel.IsVisible = isCompanySelected;
                LoadData();
                LoadEmployees();
                Debug.WriteLine("OnCompanySelected executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnCompanySelected: {ex}");
            }
        }

        private void OnHumanSelected(object sender, EventArgs e)
        {
            try
            {
                if (HumanPicker.SelectedIndex >= 0 && HumanPicker.SelectedIndex < _controller.Humans?.Count)
                {
                    _controller.SelectedHuman = _controller.Humans?[HumanPicker.SelectedIndex];
                    UpdateButtonsVisibility();
                    Debug.WriteLine("OnHumanSelected executed successfully.");
                }
                else
                {
                    Debug.WriteLine("Invalid HumanPicker.SelectedIndex.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnHumanSelected: {ex}");
            }
        }

        private void UpdateButtonsVisibility()
        {
            Debug.WriteLine("UpdateButtonsVisibility started.");
            try
            {
                bool isEmployee = _controller.SelectedHuman != null &&
                                  _controller.SelectedCompany != null &&
                                  !_controller.IsEmployee(_controller.SelectedCompany, _controller.SelectedHuman.Uuid);

                InviteButton.IsVisible = isEmployee;
                PositionEntry.IsVisible = isEmployee;

                Debug.WriteLine("UpdateButtonsVisibility executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateButtonsVisibility: {ex}");
            }
        }

        private void LoadEmployees()
        {
            Debug.WriteLine("LoadEmployees started.");
            try
            {
                if (_controller.SelectedCompany != null)
                {
                    var employees = _controller.SelectedCompany.EmployeeUUIDs.Select((id, index) =>
                    {
                        var employee = _humanStorageService.LoadEntity($"{Config.HumanStoragePath}{id}");
                        return new
                        {
                            Number = index + 1,
                            Name = employee?.UserDefaultCredentials.FirstName + " " +
                                   employee?.UserDefaultCredentials.LastName,
                            Position = employee?.EmploymentHistoryRecords.Last().PositionAtWork,
                            Id = id
                        };
                    }).ToList();
                    EmployeesCollectionView.ItemsSource = employees;
                }
                Debug.WriteLine("LoadEmployees executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in LoadEmployees: {ex}");
            }
        }
        
        private async void OnViewClicked(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("OnViewClicked started.");
                var button = sender as Button;
                if (button?.CommandParameter is string employeeUuid)
                {
                    _controller.SelectedHuman = _humanStorageService.LoadEntity($"{Config.HumanStoragePath}{employeeUuid}");

                    if (_controller.SelectedHuman == null)
                    {
                        Debug.WriteLine("Cant OnViewClicked, Loaded human is null.");
                        return;
                    }

                    await Navigation.PushAsync(new EmployeePage(_controller));
                    Debug.WriteLine("OnViewClicked executed successfully.");
                }
                else
                {
                    Debug.WriteLine("Cant OnViewClicked, Employee UUID is null or invalid.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnViewClicked: {ex}");
            }
        }
    }
}