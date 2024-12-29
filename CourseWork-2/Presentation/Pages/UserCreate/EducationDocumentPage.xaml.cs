using CourseWork_2.Data.ViewControllers;
using CourseWork_2.Data.ViewControllers.UserCreation;
using CourseWork_2.Domain.Models;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Presentation.Pages.UserCreate;

public partial class EducationDocumentPage
{
    private readonly EducationDocumentController _controller = new();
    private readonly UserCreationViewController _userCreationController;

    public EducationDocumentPage(HumanDataHolder humanData, UserCreationViewController userCreationController)
    {
        InitializeComponent();
        _userCreationController = userCreationController;
        LevelPicker.ItemsSource = _controller.GetEducationLevelTranslations().Keys.ToList();
        SerialEntry.TextChanged += OnSerialEntryTextChanged;
        NumberEntry.TextChanged += OnNumberEntryTextChanged;
        
        if (humanData.EducationDocument == null) return;
        var educationDocument = humanData.EducationDocument;
        SerialEntry.Text = educationDocument.Serial;
        NumberEntry.Text = educationDocument.Number;
        DateOfIssueDatePicker.Date = educationDocument.DateOfIssue;
        InstitutionEntry.Text = educationDocument.Institution;
        SpecialtyEntry.Text = educationDocument.Specialty;
        DirectionEntry.Text = educationDocument.Direction;
        GraduatedDatePicker.Date = educationDocument.GraduatedDate;
        LevelPicker.SelectedItem = _controller.GetEducationLevelTranslations().FirstOrDefault(x => x.Value == educationDocument.Level).Key;
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