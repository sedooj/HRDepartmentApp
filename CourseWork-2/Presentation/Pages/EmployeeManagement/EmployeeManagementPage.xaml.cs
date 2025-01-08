using CourseWork_2.Data.Controllers;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeeManagementPage
    {
        private readonly EmployeeManagementPageController _controller = new();

        public EmployeeManagementPage()
        {
            InitializeComponent();
            InitPage();
        }

        private void InitPage()
        {
            _controller.LoadData();
            _controller.UpdatePickers(CompanyPicker, HumanPicker);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _controller.OnAppearing(CompanyPicker, HumanPicker, InviteButton, PositionEntry, EmployeesCollectionView);
        }

        private void OnInviteClicked(object sender, EventArgs e)
        {
            _controller.InviteEmployee(PositionEntry, InviteButton, PositionEntry, EmployeesCollectionView, HumanPicker);
        }

        private void OnCompanySelected(object sender, EventArgs e)
        {
            _controller.OnCompanySelect(CompanyPicker, HumanPicker, TableFrame, CompanyEmployeesLabel, InviteButton, PositionEntry, EmployeesCollectionView);
        }

        private void OnHumanSelected(object sender, EventArgs e)
        {
            _controller.OnHumanSelect(InviteButton, PositionEntry, HumanPicker);
        }
        
        private async void OnViewClicked(object sender, EventArgs e)
        {
            if (_controller.OnViewClicked(sender)) await Navigation.PushAsync(new EmployeePage(_controller));
        }
    }
}