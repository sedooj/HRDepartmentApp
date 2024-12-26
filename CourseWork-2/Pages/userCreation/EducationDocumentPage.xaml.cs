using CourseWork_2.entity;
using CourseWork_2.Models;
using CourseWork_2.Util;
using CourseWork_2.ViewControllers;
using Microsoft.Maui.Controls;

namespace CourseWork_2.Pages.userCreation;

public partial class EducationDocumentPage
{
    private readonly EducationDocumentController _controller = new();

    public EducationDocumentPage()
    {
        InitializeComponent();
        LevelPicker.ItemsSource = _controller.GetEducationLevelTranslations().Keys.ToList();

        SerialEntry.TextChanged += OnSerialEntryTextChanged;
        NumberEntry.TextChanged += OnNumberEntryTextChanged;
    }

    private void OnSerialEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        bool needToChangeColor = _controller.ValidateSerial(e.NewTextValue).Item2;
        EntryUtil.ChangeEntryColor(SerialEntry, !needToChangeColor);
    }

    private void OnNumberEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        bool needToChangeColor = _controller.ValidateNumber(e.NewTextValue).Item2;
        EntryUtil.ChangeEntryColor(NumberEntry, !needToChangeColor);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!await _controller.ValidateInputs(LevelPicker, InstitutionEntry, SpecialtyEntry, SerialEntry, NumberEntry,
                DirectionEntry, DateOfIssueDatePicker))
        {
            return;
        }

        var selectedLevel = LevelPicker.SelectedItem.ToString();
        var level = _controller.GetEducationLevelTranslations()[selectedLevel];
        var educationDocument = new EducationDocument(
            0,
            InstitutionEntry.Text,
            GraduatedDatePicker.Date.ToString("yyyy-MM-dd"),
            SpecialtyEntry.Text,
            SerialEntry.Text,
            NumberEntry.Text,
            level,
            DirectionEntry.Text,
            DateOfIssueDatePicker.Date.ToString("yyyy-MM-dd")
        );
        // MessagingCenter.Send(this, "EducationDocumentSaved", educationDocument);
        await Navigation.PopAsync();
    }
}