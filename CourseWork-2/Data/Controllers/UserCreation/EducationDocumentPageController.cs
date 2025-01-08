using CourseWork_2.Domain.Models;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Controllers.UserCreation;

public class EducationDocumentPageController
{
    private const int MaxSerialLength = 6;
    private const int MaxNumberLength = 7;

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

    public Dictionary<string, EducationDocument.EducationLevels> GetEducationLevelTranslations()
    {
        return _educationLevelTranslations;
    }

    public (bool, bool) ValidateSerial(string serial)
    {
        if (!Validator.ValidateInt(serial, out _) || serial.Length != MaxSerialLength) return (false, true);
        return (true, false);
    }

    public (bool, bool) ValidateNumber(string number)
    {   
        if (!Validator.ValidateInt(number, out _) || number.Length != MaxNumberLength) return (false, true);
        return (true, false);
    }

    public bool ValidateDateOfIssue(DateTime dateOfIssue)
    {
        return Validator.RequireDateIsNotGreaterThanNow(dateOfIssue);
    }

    public bool ValidateRequiredFields(params string[] fields)
    {
        return fields.All(Validator.ValidateName);
    }

    public async Task<bool> ValidateInputs(Picker levelPicker, Entry institutionEntry, Entry specialtyEntry,
        Entry serialEntry, Entry numberEntry, Entry directionEntry, DatePicker dateOfIssueDatePicker)
    {
        if (levelPicker.SelectedIndex == -1)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Пожалуйста, выберите уровень образования", "OK");
            return false;
        }

        if (!ValidateRequiredFields(institutionEntry.Text, specialtyEntry.Text, serialEntry.Text, numberEntry.Text,
                directionEntry.Text))
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Пожалуйста, заполните все обязательные поля",
                "OK");
            return false;
        }

        bool isSerialValid = ValidateSerial(serialEntry.Text).Item1;
        bool isNumberValid = ValidateNumber(numberEntry.Text).Item1;

        if (!isSerialValid || !isNumberValid)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", "Пожалуйста, исправьте ошибки ввода", "OK");
            return false;
        }

        return true;
    }
}