using CourseWork_2.Data.ViewControllers;

namespace CourseWork_2.Presentation.Pages.UserCreate;

public partial class UserCreationPage
{
    private readonly UserCreationViewController _controller = new();

    public UserCreationPage()
    {
        InitializeComponent();
    }

    private async void OnCreateUserClicked(object sender, EventArgs e)
    {
        if (await _controller.CreateHuman())
        {
            await Navigation.PopAsync();
        }
    }

    private void OnPassportButtonClicked(object sender, EventArgs e)
    {
        var passportPage = new PassportPage(_controller.HumanData, _controller);
        Navigation.PushAsync(passportPage);
    }

    private void OnUserDefaultCredentialsButtonClicked(object sender, EventArgs e)
    {
        var userDefaultCredentialsPage = new UserDefaultCredentialsPage(_controller.HumanData, _controller);
        Navigation.PushAsync(userDefaultCredentialsPage);
    }

    private void OnEducationDocumentButtonClicked(object sender, EventArgs e)
    {
        var educationDocumentPage = new EducationDocumentPage(_controller.HumanData, _controller);
        Navigation.PushAsync(educationDocumentPage);
    }

    private async void OnLoadDataFromJson(object? sender, EventArgs e)
    {
        await _controller.LoadHumanDataFromFile("test.json");
    }
}