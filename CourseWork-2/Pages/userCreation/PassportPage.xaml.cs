using CourseWork_2.Models;
using CourseWork_2.Util;
using CourseWork_2.ViewControllers;
using Microsoft.Maui.Controls;

namespace CourseWork_2.Pages.userCreation;

public partial class PassportPage
{
    private readonly PassportController _controller = new();

    public PassportPage()
    {
        InitializeComponent();

        SerialEntry.TextChanged += OnSerialEntryTextChanged;
        NumberEntry.TextChanged += OnNumberEntryTextChanged;
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
            DateOfIssueDatePicker.Date.ToString("yyyy-MM-dd"),
            WhoIssuedEntry.Text
        );
        // MessagingCenter.Send(this, "PassportSaved", passport);
        await Navigation.PopAsync();
    }
}