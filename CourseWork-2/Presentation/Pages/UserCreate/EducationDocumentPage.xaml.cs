using CourseWork_2.Data.Controllers.UserCreation;
using CourseWork_2.Domain.Models;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Presentation.Pages.UserCreate;

public partial class EducationDocumentPage
{
    private readonly EducationDocumentPageController _pageController = new();
    private readonly UserCreationPageController _userCreationModel;

    public EducationDocumentPage(HumanDataHolder humanData, UserCreationPageController userCreationModel)
    {
        InitializeComponent();
        _userCreationModel = userCreationModel;
        Init(humanData);
    }

    private void Init(HumanDataHolder humanData)
    {
        LevelPicker.ItemsSource = _pageController.GetEducationLevelTranslations().Keys.ToList();
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
        LevelPicker.SelectedItem = _pageController.GetEducationLevelTranslations()
            .FirstOrDefault(x => x.Value == educationDocument.Level).Key;
    }

    private void OnSerialEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        bool needToChangeColor = _pageController.ValidateSerial(e.NewTextValue).Item2;
        EntryUtil.ChangeEntryColor(SerialEntry, !needToChangeColor);
    }

    private void OnNumberEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        bool needToChangeColor = _pageController.ValidateNumber(e.NewTextValue).Item2;
        EntryUtil.ChangeEntryColor(NumberEntry, !needToChangeColor);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!await _pageController.ValidateInputs(LevelPicker, InstitutionEntry, SpecialtyEntry, SerialEntry, NumberEntry,
                DirectionEntry, DateOfIssueDatePicker))
        {
            return;
        }

        var selectedLevel = LevelPicker.SelectedItem.ToString();
        var level = _pageController.GetEducationLevelTranslations()[selectedLevel];
        var educationDocument = new EducationDocument(
            Guid.NewGuid(),
            InstitutionEntry.Text,
            GraduatedDatePicker.Date,
            SpecialtyEntry.Text,
            SerialEntry.Text,
            NumberEntry.Text,
            level,
            DirectionEntry.Text,
            DateOfIssueDatePicker.Date
        );

        _userCreationModel.UpdateEducationDocument(educationDocument);
        await Navigation.PopAsync();
    }
}