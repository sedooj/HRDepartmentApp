using CourseWork_2.ViewControllers;
using CourseWork.entity;

namespace CourseWork_2.Pages.userCreation;

public partial class UserDefaultCredentialsPage
{
    private readonly UserDefaultCredentialsController _controller = new();

    public UserDefaultCredentialsPage()
    {
        InitializeComponent();
    }

    private void OnFirstNameTextChanged(object sender, TextChangedEventArgs e)
    {
        _controller.ValidateFirstName((Entry)sender);
    }

    private void OnLastNameTextChanged(object sender, TextChangedEventArgs e)
    {
        _controller.ValidateLastName((Entry)sender);
    }

    private void OnSecondNameTextChanged(object sender, TextChangedEventArgs e)
    {
        _controller.ValidateSecondName((Entry)sender);
    }

    private void OnDateOfBirthDateSelected(object sender, DateChangedEventArgs e)
    {
        _controller.ValidateDateOfBirth((DatePicker)sender);
    }

    private void OnHomeAddressTextChanged(object sender, TextChangedEventArgs e)
    {
        _controller.ValidateHomeAddress((Entry)sender);
    }

    private void OnPhoneNumberTextChanged(object sender, TextChangedEventArgs e)
    {
        _controller.ValidatePhoneNumber((Entry)sender);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!await _controller.ValidateInputs(FirstNameEntry, LastNameEntry, SecondNameEntry, DateOfBirthDatePicker, HomeAddressEntry, PhoneNumberEntry))
        {
            return;
        }

        var userDefaultCredentials = new UserDefaultCredentials(
            FirstNameEntry.Text,
            LastNameEntry.Text,
            SecondNameEntry.Text,
            DateOfBirthDatePicker.Date,
            HomeAddressEntry.Text,
            PhoneNumberEntry.Text,
            ""
        );
        await Navigation.PopAsync();
    }
}