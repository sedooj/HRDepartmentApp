using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Domain.Models;
using System.Diagnostics;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeePage
    {
        private readonly Company? _company;
        private readonly Employee? _employee;
        private readonly EmployeeManagementPageViewController? _controller;

        public EmployeePage(Company company, Employee employee, EmployeeManagementPageViewController controller, string companyTitle)
        {
            CompanyTitle = companyTitle;
            try
            {
                Debug.WriteLine("Initializing EmployeePage...");
                InitializeComponent();
                _company = company;
                _employee = employee;
                _controller = controller;
                BindingContext = this;

                Debug.WriteLine($"Company: {_company.Name}");
                Debug.WriteLine($"Employee: {_employee.UserDefaultCredentials.FirstName} {_employee.UserDefaultCredentials.LastName}");
                Debug.WriteLine($"Position: {_employee.Position}");

                CompanyTitle = $"Компания: {_company.Name}";
                FullNameLabel.Text = _employee.UserDefaultCredentials.FirstName + " " +
                                     _employee.UserDefaultCredentials.LastName;
                PositionLabel.Text = _employee.Position;
                Debug.WriteLine("EmployeePage initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error initializing EmployeePage: {ex}");
            }
        }

        public string CompanyTitle { get; }

        private async void OnRewardClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new RewardPage(_employee, _controller));
                Debug.WriteLine("OnRewardClicked executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnRewardClicked: {ex}");
            }
        }

        private void OnPunishClicked(object sender, EventArgs e)
        {
            try
            {
                // Handle punish logic here
                Debug.WriteLine("OnPunishClicked executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnPunishClicked: {ex}");
            }
        }

        private async void OnFireClicked(object sender, EventArgs e)
        {
            try
            {
                _controller.FireEmployee(_company, _employee);
                await Navigation.PopAsync();
                if (Navigation.NavigationStack.LastOrDefault() is EmployeeManagementPage employeeManagementPage)
                {
                    employeeManagementPage.LoadData();
                }
                Debug.WriteLine("OnFireClicked executed successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in OnFireClicked: {ex}");
            }
        }
    }
}