using CourseWork_2.entity;
using CourseWork_2.Pages.userCreation;
using Microsoft.Maui.Controls;
using CourseWork.entity;
using CourseWork.entity.employmentHistory;
using Microsoft.Maui.Graphics;

namespace CourseWork_2.Pages;

public partial class UserCreationPage : ContentPage
{
    private Passport _passport;
    private UserDefaultCredentials _userDefaultCredentials;
    private List<EmploymentHistoryRecord> _employmentHistoryRecords;
    private Education _education;
    private EducationDocument _educationDocument;

    public UserCreationPage()
    {
        InitializeComponent();
        MessagingCenter.Subscribe<UserDefaultCredentialsPage, UserDefaultCredentials>(this, "UserDefaultCredentialsSaved", (sender, userDefaultCredentials) =>
        {
            _userDefaultCredentials = userDefaultCredentials;
        });
        // Subscribe to other pages similarly
    }

    private async void OnPassportButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PassportPage());
    }

    private async void OnUserDefaultCredentialsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserDefaultCredentialsPage());
    }

    private async void OnEmploymentHistoryRecordsButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EmploymentHistoryRecordsPage());
    }

    private async void OnEducationButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EducationPage());
    }

    private async void OnEducationDocumentButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EducationDocumentPage());
    }

    private void OnCreateUserClicked(object? sender, EventArgs e)
    {
        // var newUser = new Human(_passport, _userDefaultCredentials, _employmentHistoryRecords, _education, _educationDocument);
        // Your user creation logic here
    }

    // private void OnAgeEntryTextChanged(object sender, TextChangedEventArgs e)
    // {
    //     if (!int.TryParse(e.NewTextValue, out var inputAge) || inputAge <= 0 || inputAge > 125)
    //     {
    //         AgeEntry.TextColor = Colors.Red;
    //     }
    //     else
    //     {
    //         AgeEntry.TextColor = Colors.White;
    //     }
    // }
}