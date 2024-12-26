using CourseWork_2.Models;
using CourseWork_2.Util;
using CourseWork_2.ViewControllers;
using Microsoft.Maui.Controls;

namespace CourseWork_2.Pages;

public partial class EducationDocumentPage
{
    private readonly EducationDocumentController _controller = new();
    private readonly UserCreationViewController _userCreationController;

    public EducationDocumentPage(HumanDataHolder humanData, UserCreationViewController userCreationController)
    {
        InitializeComponent();
        _userCreationController = userCreationController;
        LevelPicker.ItemsSource = _controller.GetEducationLevelTranslations().Keys.ToList();

        var educationDocument = humanData.EducationDocument;
        SerialEntry.Text = educationDocument.Serial;
        NumberEntry.Text = educationDocument.Number;
        DateOfIssueDatePicker.Date = educationDocument.DateOfIssue;
        InstitutionEntry.Text = educationDocument.Institution;
        SpecialtyEntry.Text = educationDocument.Specialty;
        DirectionEntry.Text = educationDocument.Direction;
        GraduatedDatePicker.Date = educationDocument.GraduatedDate;
        LevelPicker.SelectedItem = _controller.GetEducationLevelTranslations().FirstOrDefault(x => x.Value == educationDocument.Level).Key;

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
            GraduatedDatePicker.Date,
            SpecialtyEntry.Text,
            SerialEntry.Text,
            NumberEntry.Text,
            level,
            DirectionEntry.Text,
            DateOfIssueDatePicker.Date
        );

        _userCreationController.UpdateEducationDocument(educationDocument);
        await Navigation.PopAsync();
    }
}