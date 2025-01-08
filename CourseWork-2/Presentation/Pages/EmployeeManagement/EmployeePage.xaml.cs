using CourseWork_2.Data.Controllers;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement
{
    public partial class EmployeePage
    {
        private readonly EmployeeManagementPageController _controller;
        private readonly EmployeePageController _employeePageController = new();

        public EmployeePage(EmployeeManagementPageController controller)
        {
            InitializeComponent();
            _controller = controller;
            Init();
        }

        private void Init()
        {
            BindingContext = this;
            _employeePageController.InitPage(_controller, EmploymentHistoryCollectionView, EmployeeNameLabel, EmployeePositionLabel); 
        }

        private async void OnRewardClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RewardPage(_controller));
        }

        private async void OnPunishClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PunishmentPage(_controller));
        }

        private async void OnViewDetailsClicked(object sender, EventArgs e)
        {
            var record = _employeePageController.OnViewDetailsClicked(sender, _controller);
            if (record == null) return;
            await Navigation.PushAsync(new EmployeeDetailsPage(record));
        }
    }
}