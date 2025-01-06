using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Data.ViewModels.UserCreation;
using CourseWork_2.Domain.Models;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Presentation.Pages.UserCreate;

public partial class PassportPage
{
    private readonly PassportController _controller = new();
    private readonly UserCreationViewModel _userCreationModel;

    public PassportPage(HumanDataHolder humanData, UserCreationViewModel userCreationModel)
    {
        InitializeComponent();
        _userCreationModel = userCreationModel;
        SerialEntry.TextChanged += OnSerialEntryTextChanged;
        NumberEntry.TextChanged += OnNumberEntryTextChanged;
        
        if (humanData.Passport == null)
        {
            Console.WriteLine("HumanData.Passport is null");
            return;
        }

        var passport = humanData.Passport;
        SerialEntry.Text = passport.Serial;
        NumberEntry.Text = passport.Number;
        DateOfIssueDatePicker.Date = passport.DateOfIssue;
        WhoIssuedEntry.Text = passport.WhoIssued;
    }

    private void OnSerialEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        bool needToChangeColor = _controller.ValidateSerial(SerialEntry);
        EntryUtil.ChangeEntryColor(SerialEntry, needToChangeColor);
    }

    private void OnNumberEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        bool needToChangeColor = _controller.ValidateNumber(NumberEntry);
        EntryUtil.ChangeEntryColor(NumberEntry, needToChangeColor);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!await _controller.ValidateInputs(SerialEntry, NumberEntry, DateOfIssueDatePicker, WhoIssuedEntry))
        {
            return;
        }

        var passport = new Passport(
            SerialEntry.Text,
            NumberEntry.Text,
            DateOfIssueDatePicker.Date,
            WhoIssuedEntry.Text
        );

        _userCreationModel.UpdatePassport(passport);
        await Navigation.PopAsync();
    }
}