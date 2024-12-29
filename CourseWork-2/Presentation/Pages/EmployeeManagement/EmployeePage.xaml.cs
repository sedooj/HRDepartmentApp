using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Domain.Models;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeePage
    {
        private readonly Company _company;
        private readonly Employee _employee;
        private readonly EmployeeManagementPageViewController _controller;

        public EmployeePage(Company company, Employee employee, EmployeeManagementPageViewController controller)
        {
            InitializeComponent();
            _company = company;
            _employee = employee;
            _controller = controller;
            BindingContext = this;

            CompanyTitle = $"Компания: {_company.Name}";
            FullNameLabel.Text = _employee.UserDefaultCredentials.FirstName + " " +
                                 _employee.UserDefaultCredentials.LastName;
            PositionLabel.Text = _employee.Position;
        }

        public string CompanyTitle { get; }

        private void OnRewardClicked(object sender, EventArgs e)
        {
            // Handle reward logic here
        }

        private void OnPunishClicked(object sender, EventArgs e)
        {
            // Handle punish logic here
        }

        private async void OnFireClicked(object sender, EventArgs e)
        {
            _controller.FireEmployee(_company, _employee);
            await Navigation.PopAsync();
            if (Navigation.NavigationStack.LastOrDefault() is EmployeeManagementPage employeeManagementPage)
            {
                employeeManagementPage.LoadEmployees();
                employeeManagementPage.LoadData();
            }
        }
    }
}