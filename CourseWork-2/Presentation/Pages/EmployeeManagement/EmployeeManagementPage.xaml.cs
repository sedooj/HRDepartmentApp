using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Domain.Models;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeeManagementPage
    {
        private readonly EmployeeManagementPageViewController _controller = new();
        private List<Company>? _companies;
        private List<Human>? _humans;
        private Company? _selectedCompany;
        private Human? _selectedHuman;

        public EmployeeManagementPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _companies = _controller.LoadCompanies().ToList();
            _humans = _controller.LoadHumans().ToList();

            CompanyPicker.ItemsSource = _companies.Select(c => c.Name).ToList();
            HumanPicker.ItemsSource = _humans.Select(h => h.UserDefaultCredentials.FirstName).ToList();
        }

        private void OnCompanySelected(object sender, EventArgs e)
        {
            _selectedCompany = _companies[CompanyPicker.SelectedIndex];
            UpdateButtonsVisibility();
            LoadEmployees();
        }

        private void OnHumanSelected(object sender, EventArgs e)
        {
            _selectedHuman = _humans[HumanPicker.SelectedIndex];
            UpdateButtonsVisibility();
        }

        private void UpdateButtonsVisibility()
        {
            if (_selectedCompany != null && _selectedHuman != null)
            {
                bool isEmployee = _controller.IsEmployee(_selectedCompany, _selectedHuman);
                InviteButton.IsVisible = !isEmployee;
                PositionEntry.IsVisible = !isEmployee;
            }
        }

        private void OnInviteClicked(object sender, EventArgs e)
        {
            string position = PositionEntry.Text;
            _controller.InviteEmployee(_selectedCompany, _selectedHuman, position);
            UpdateButtonsVisibility();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            if (_selectedCompany != null)
            {
                var employees = _selectedCompany.Employees.Select((e, index) => new
                {
                    Number = index + 1,
                    Name = e.UserDefaultCredentials.FirstName,
                    Position = e.Position,
                    UUID = e.UUID
                }).ToList();

                EmployeesCollectionView.ItemsSource = employees;
            }
        }

        private void OnViewClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var employeeUUID = button?.CommandParameter as string;
            var employee = _selectedCompany?.Employees.FirstOrDefault(e => e.UUID == employeeUUID);
            if (employee != null)
            {
                // Handle view button click logic here
            }
        }
    }
}