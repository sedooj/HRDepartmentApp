using CourseWork_2.Pages;

namespace CourseWork_2;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnUserCreateClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserCreationPage());
    }

    private async void OnCompanyCreateClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CompanyCreatePage());
    }
}