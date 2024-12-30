using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Domain.Models;
using System.Diagnostics;
using CourseWork_2.Data.Service;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeeManagementPage
    {
        private readonly EmployeeManagementPageViewController _controller = new();
        private readonly LocalEmployeeService _employeeService = new();
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
                LoadEmployees();
                Debug.WriteLine("OnAppearing executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnAppearing: {ex}");
            }
        }

        public void LoadData()
        {
            try
            {
                var companyNames = _controller.Companies?.Select(c => c.Name).ToList();
                CompanyPicker.ItemsSource = companyNames;
                Debug.WriteLine($"Loaded companies: {string.Join(", ", companyNames ?? new List<string>())}");

                if (_controller.SelectedCompany != null)
                {
                    var employeeUUIDs = _controller.SelectedCompany.EmployeeUUIDs;
                    HumanPicker.ItemsSource = _controller.Humans?
                        .Where(h => !employeeUUIDs.Contains(h.UUID))
                        .Select(h => h.UserDefaultCredentials.FirstName)
                        .ToList();
                }
                else
                {
                    HumanPicker.ItemsSource =
                        _controller.Humans?.Select(h => h.UserDefaultCredentials.FirstName).ToList();
                }

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
                TableGrid.IsVisible = _controller.SelectedCompany != null;
                TableFrame.IsVisible = _controller.SelectedCompany != null;
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
                _controller.SelectedHuman = _controller.Humans?[HumanPicker.SelectedIndex];
                UpdateButtonsVisibility();
                Debug.WriteLine("OnHumanSelected executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnHumanSelected: {ex}");
            }
        }

        private void UpdateButtonsVisibility()
        {
            try
            {
                if (_controller.SelectedCompany != null && _controller.SelectedHuman != null)
                {
                    bool isEmployee = _controller.IsEmployee(_controller.SelectedCompany, _controller.SelectedHuman);
                    Console.WriteLine(isEmployee);
                    InviteButton.IsVisible = !isEmployee;
                    PositionEntry.IsVisible = !isEmployee;
                }

                Debug.WriteLine("UpdateButtonsVisibility executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in UpdateButtonsVisibility: {ex}");
            }
        }

        private void OnInviteClicked(object sender, EventArgs e)
        {
            try
            {
                string position = PositionEntry.Text;
                _controller.InviteEmployee(position);
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

        public void LoadEmployees()
        {
            try
            {
                if (_controller.SelectedCompany != null)
                {
                    var employees = _controller.SelectedCompany.EmployeeUUIDs.Select((uuid, index) =>
                    {
                        var human = _controller.Humans?.FirstOrDefault(h => h.UUID == uuid);
                        return new
                        {
                            Number = index + 1,
                            Name = human?.UserDefaultCredentials.FirstName,
                            Position = human is Employee employee ? employee.Position : null,
                            UUID = uuid
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

        private void OnViewClicked(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("OnViewClicked started.");
                var button = sender as Button;
                var employeeUuid = button?.CommandParameter as string;
                if (employeeUuid == null)
                {
                    Debug.WriteLine("Employee UUID is null.");
                    return;
                }

                Debug.WriteLine($"Employee UUID: {employeeUuid}");
                var employee = _employeeService.GetEmployeeByUUID(employeeUuid);
                if (employee == null)
                {
                    Debug.WriteLine("Employee not found.");
                    return;
                }

                Debug.WriteLine($"Employee found: {employee.UserDefaultCredentials.FirstName} {employee.UserDefaultCredentials.LastName}");
                _controller.SelectedHuman = employee;
                Navigation.PushAsync(new EmployeePage(_controller.SelectedCompany, employee, _controller, _controller.SelectedCompany.Name));
                Debug.WriteLine("OnViewClicked executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnViewClicked: {ex}");
            }
        }
    }
}