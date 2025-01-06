using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Data.ViewModels.UserCreation;
using CourseWork_2.Domain.Models;

namespace CourseWork_2.Presentation.Pages.UserCreate;

public partial class UserDefaultCredentialsPage
{
    private readonly UserDefaultCredentialsViewModel _viewModel = new();
    private readonly UserCreationViewModel _userCreationModel;

    public UserDefaultCredentialsPage(HumanDataHolder humanData, UserCreationViewModel userCreationModel)
    {
        InitializeComponent();
        _userCreationModel = userCreationModel;
        
        if (humanData.UserDefaultCredentials == null) return;
        var credentials = humanData.UserDefaultCredentials;
        FirstNameEntry.Text = credentials.FirstName;
        LastNameEntry.Text = credentials.LastName;
        SecondNameEntry.Text = credentials.SecondName;
        DateOfBirthDatePicker.Date = credentials.DateOfBirth;
        HomeAddressEntry.Text = credentials.HomeAddress;
        PhoneNumberEntry.Text = credentials.PhoneNumber;
    }

    private void OnFirstNameTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.ValidateFirstName((Entry)sender);
    }

    private void OnLastNameTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.ValidateLastName((Entry)sender);
    }

    private void OnSecondNameTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.ValidateSecondName((Entry)sender);
    }

    private void OnDateOfBirthDateSelected(object sender, DateChangedEventArgs e)
    {
        _viewModel.ValidateDateOfBirth((DatePicker)sender);
    }

    private void OnHomeAddressTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.ValidateHomeAddress((Entry)sender);
    }

    private void OnPhoneNumberTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.ValidatePhoneNumber((Entry)sender);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!await _viewModel.ValidateInputs(FirstNameEntry, LastNameEntry, SecondNameEntry, DateOfBirthDatePicker, HomeAddressEntry, PhoneNumberEntry))
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

        _userCreationModel.UpdateUserDefaultCredentials(userDefaultCredentials);
        await Navigation.PopAsync();
    }
}