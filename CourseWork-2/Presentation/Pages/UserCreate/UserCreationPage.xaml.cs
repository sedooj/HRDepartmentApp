using CourseWork_2.Data.ViewControllers.UserCreation;

namespace CourseWork_2.Presentation.Pages.UserCreate;

public partial class UserCreationPage
{
    private readonly UserCreationViewController _controller = new();

    public UserCreationPage()
    {
        InitializeComponent();
    }

    private void OnCreateUserClicked(object sender, EventArgs e)
    {
        try
        {
            if (_controller.CreateHuman())
            {
                Navigation.PopAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnCreateUserClicked: {ex.Message}");
            DisplayAlert("Error", "Failed to create user.", "OK");
        }
    }

    private void OnPassportButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var passportPage = new PassportPage(_controller.HumanData, _controller);
            Navigation.PushAsync(passportPage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnPassportButtonClicked: {ex.Message}");
            DisplayAlert("Error", "Failed to open Passport Page.", "OK");
        }
    }

    private void OnUserDefaultCredentialsButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var userDefaultCredentialsPage = new UserDefaultCredentialsPage(_controller.HumanData, _controller);
            Navigation.PushAsync(userDefaultCredentialsPage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnUserDefaultCredentialsButtonClicked: {ex.Message}");
            DisplayAlert("Error", "Failed to open User Default Credentials Page.", "OK");
        }
    }

    private void OnEducationDocumentButtonClicked(object sender, EventArgs e)
    {
        try
        {
            var educationDocumentPage = new EducationDocumentPage(_controller.HumanData, _controller);
            Navigation.PushAsync(educationDocumentPage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnEducationDocumentButtonClicked: {ex.Message}");
            DisplayAlert("Error", "Failed to open Education Document Page.", "OK");
        }
    }
}