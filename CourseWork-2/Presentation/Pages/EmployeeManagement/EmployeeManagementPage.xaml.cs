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

        private async void LoadData()
        {
            _companies = (await _controller.LoadCompaniesAsync()).ToList();
            _humans = (await _controller.LoadHumansAsync()).ToList();

            CompanyPicker.ItemsSource = _companies.Select(c => c.Name).ToList();
            HumanPicker.ItemsSource = _humans.Select(h => h.UserDefaultCredentials.FirstName).ToList();
        }

        private void OnCompanySelected(object sender, EventArgs e)
        {
            _selectedCompany = _companies[CompanyPicker.SelectedIndex];
            UpdateButtonsVisibility();
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
                DismissButton.IsVisible = isEmployee;
            }
        }

        private async void OnInviteClicked(object sender, EventArgs e)
        {
            await _controller.InviteEmployeeAsync(_selectedCompany, _selectedHuman);
            UpdateButtonsVisibility();
        }

        private async void OnDismissClicked(object sender, EventArgs e)
        {
            await _controller.DismissEmployeeAsync(_selectedCompany, _selectedHuman);
            UpdateButtonsVisibility();
        }
    }
}