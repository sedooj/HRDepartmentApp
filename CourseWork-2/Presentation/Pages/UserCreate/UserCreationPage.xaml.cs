using CourseWork_2.Data.Controllers.UserCreation;

namespace CourseWork_2.Presentation.Pages.UserCreate;

public partial class UserCreationPage
{
    private readonly UserCreationPageController _userCreationPageController = new();

    public UserCreationPage()
    {
        InitializeComponent();
    }

    private void OnCreateUserClicked(object sender, EventArgs e)
    {
        try
        {
            if (_userCreationPageController.CreateHuman())
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
            var passportPage = new PassportPage(_userCreationPageController.HumanData, _userCreationPageController);
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
            var userDefaultCredentialsPage = new UserDefaultCredentialsPage(_userCreationPageController.HumanData, _userCreationPageController);
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
            var educationDocumentPage = new EducationDocumentPage(_userCreationPageController.HumanData, _userCreationPageController);
            Navigation.PushAsync(educationDocumentPage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in OnEducationDocumentButtonClicked: {ex.Message}");
            DisplayAlert("Error", "Failed to open Education Document Page.", "OK");
        }
    }
}