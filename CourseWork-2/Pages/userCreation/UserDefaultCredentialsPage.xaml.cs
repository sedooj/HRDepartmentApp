using Microsoft.Maui.Controls;
using CourseWork.entity;

namespace CourseWork_2.Pages.userCreation;

public partial class UserDefaultCredentialsPage : ContentPage
{
    public UserDefaultCredentialsPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var userDefaultCredentials = new UserDefaultCredentials(
            FirstNameEntry.Text,
            LastNameEntry.Text,
            SecondNameEntry.Text,
            int.Parse(AgeEntry.Text),
            HomeAddressEntry.Text,
            PhoneNumberEntry.Text,
            ""
        );
        MessagingCenter.Send(this, "UserDefaultCredentialsSaved", userDefaultCredentials);
        await Navigation.PopAsync();
    }
}