using CourseWork_2.Data.Controllers.UserCreation;
using CourseWork_2.Domain.Models;

namespace CourseWork_2.Presentation.Pages.UserCreate;

public partial class UserDefaultCredentialsPage
{
    private readonly UserDefaultCredentialsPageController _pageController = new();
    private readonly UserCreationPageController _userCreationModel;

    public UserDefaultCredentialsPage(HumanDataHolder humanData, UserCreationPageController userCreationModel)
    {
        InitializeComponent();
        _userCreationModel = userCreationModel;
        Init(humanData);
    }

    private void Init(HumanDataHolder humanData)
    {
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
        _pageController.ValidateFirstName((Entry)sender);
    }

    private void OnLastNameTextChanged(object sender, TextChangedEventArgs e)
    {
        _pageController.ValidateLastName((Entry)sender);
    }

    private void OnSecondNameTextChanged(object sender, TextChangedEventArgs e)
    {
        _pageController.ValidateSecondName((Entry)sender);
    }

    private void OnDateOfBirthDateSelected(object sender, DateChangedEventArgs e)
    {
        _pageController.ValidateDateOfBirth((DatePicker)sender);
    }

    private void OnHomeAddressTextChanged(object sender, TextChangedEventArgs e)
    {
        _pageController.ValidateHomeAddress((Entry)sender);
    }

    private void OnPhoneNumberTextChanged(object sender, TextChangedEventArgs e)
    {
        _pageController.ValidatePhoneNumber((Entry)sender);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!await _pageController.ValidateInputs(FirstNameEntry, LastNameEntry, SecondNameEntry, DateOfBirthDatePicker, HomeAddressEntry, PhoneNumberEntry))
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