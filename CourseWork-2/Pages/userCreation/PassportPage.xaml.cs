using CourseWork.entity;

namespace CourseWork_2.Pages.userCreation;

public partial class PassportPage
{
    public PassportPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var passport = new Passport(
            SerialEntry.Text,
            DateOfIssueEntry.Text,
            WhoIssuedEntry.Text
        );
        // MessagingCenter.Send(this, "PassportSaved", passport);
        await Navigation.PopAsync();
    }
}