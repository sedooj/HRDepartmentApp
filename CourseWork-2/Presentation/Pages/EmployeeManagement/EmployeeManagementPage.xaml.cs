using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Domain.Models;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeeManagementPage
    {
        private readonly EmployeeManagementPageViewController _controller = new();

        public EmployeeManagementPage()
        {
            InitializeComponent();
            _controller.LoadData();
            LoadData();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadEmployees();
        }

        public void LoadData()
        {
            CompanyPicker.ItemsSource = _controller.Companies?.Select(c => c.Name).ToList();
            if (_controller.SelectedCompany != null)
            {
                var employeesUUIDs = _controller.SelectedCompany.Employees.Select(e => e.UUID).ToList();
                HumanPicker.ItemsSource = _controller.Humans?
                    .Where(h => !employeesUUIDs.Contains(h.UUID))
                    .Select(h => h.UserDefaultCredentials.FirstName)
                    .ToList();
            }
            else
            {
                HumanPicker.ItemsSource = _controller.Humans?.Select(h => h.UserDefaultCredentials.FirstName).ToList();
            }
        }

        private void OnCompanySelected(object sender, EventArgs e)
        {
            _controller.SelectedCompany = _controller.Companies?[CompanyPicker.SelectedIndex];
            UpdateButtonsVisibility();
            LoadData();
            LoadEmployees();
        }

        private void OnHumanSelected(object sender, EventArgs e)
        {
            _controller.SelectedHuman = _controller.Humans?[HumanPicker.SelectedIndex];
            UpdateButtonsVisibility();
        }

        private void UpdateButtonsVisibility()
        {
            if (_controller.SelectedCompany != null && _controller.SelectedHuman != null)
            {
                bool isEmployee = _controller.IsEmployee(_controller.SelectedCompany, _controller.SelectedHuman);
                Console.WriteLine(isEmployee);
                InviteButton.IsVisible = !isEmployee;
                PositionEntry.IsVisible = !isEmployee;
            }
        }

        private void OnInviteClicked(object sender, EventArgs e)
        {
            string position = PositionEntry.Text;
            _controller.InviteEmployee(position);
            UpdateButtonsVisibility();
            LoadEmployees();

            HumanPicker.SelectedIndex = -1;
        }

        public void LoadEmployees()
        {
            if (_controller.SelectedCompany != null)
            {
                var employees = _controller.SelectedCompany.Employees.Select((e, index) => new
                {
                    Number = index + 1,
                    Name = e.UserDefaultCredentials.FirstName,
                    Position = e.Position,
                    UUID = e.UUID
                }).ToList();

                EmployeesCollectionView.ItemsSource = employees;
            }
        }

        private async void OnViewClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var employeeUUID = button?.CommandParameter as string;
            var employee = _controller.SelectedCompany?.Employees.FirstOrDefault(e => e.UUID == employeeUUID);
            if (employee != null)
            {
                await Navigation.PushAsync(new EmployeePage(_controller.SelectedCompany, employee, _controller));
            }
        }
    }
}