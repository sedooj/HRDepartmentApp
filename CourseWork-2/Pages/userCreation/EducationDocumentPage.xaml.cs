using Microsoft.Maui.Controls;
using CourseWork.entity;
using System.Linq;
using System.Collections.Generic;
using CourseWork_2.entity;

namespace CourseWork_2.Pages.userCreation;

public partial class EducationDocumentPage : ContentPage
{
    private readonly Dictionary<string, EducationDocument.EducationLevels> _educationLevelTranslations = new()
    {
        { "Бакалавр", EducationDocument.EducationLevels.Bachelor },
        { "Магистр", EducationDocument.EducationLevels.Master },
        { "Кандидат наук", EducationDocument.EducationLevels.PhD },
        { "Доктор наук", EducationDocument.EducationLevels.Doctorate },
        { "Специалист", EducationDocument.EducationLevels.Specialist },
        { "Ассоциированная степень", EducationDocument.EducationLevels.AssociateDegree },
        { "Постдок", EducationDocument.EducationLevels.Postdoc }
    };

    public EducationDocumentPage()
    {
        InitializeComponent();
        LevelPicker.ItemsSource = _educationLevelTranslations.Keys.ToList();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (LevelPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Ошибка", "Пожалуйста, выберите уровень образования", "OK");
            return;
        }

        var selectedLevel = LevelPicker.SelectedItem.ToString();
        var level = _educationLevelTranslations[selectedLevel];
        var educationDocument = new EducationDocument(
            0,
            InstitutionEntry.Text,
            GraduatedDatePicker.Date.ToString("yyyy-MM-dd"),
            SpecialtyEntry.Text,
            SerialEntry.Text,
            level,
            DirectionEntry.Text,
            DateOfIssueEntry.Text
        );
        MessagingCenter.Send(this, "EducationDocumentSaved", educationDocument);
        await Navigation.PopAsync();
    }
}