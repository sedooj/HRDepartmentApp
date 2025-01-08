using CourseWork_2.Data.Controllers;

namespace CourseWork_2.Presentation.Pages.EmployeeManagement;

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
        _controller.InitComponents(CompanyPicker, HumanPicker, InviteButton, PositionEntry, EmployeesCollectionView, TableFrame, CompanyEmployeesLabel);
        _controller.LoadData();
        _controller.UpdatePickers();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _controller.OnAppearing();
    }

    private void OnInviteClicked(object sender, EventArgs e)
    {
        _controller.InviteEmployee();
    }

    private void OnCompanySelected(object sender, EventArgs e)
    {
        _controller.OnCompanySelect();
    }

    private void OnHumanSelected(object sender, EventArgs e)
    {
        _controller.OnHumanSelect();
    }
        
    private async void OnViewClicked(object sender, EventArgs e)
    {
        if (_controller.OnViewClicked(sender)) await Navigation.PushAsync(new EmployeePage(_controller));
    }
}