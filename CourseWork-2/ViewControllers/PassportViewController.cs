using CourseWork_2.Util;
using Microsoft.Maui.Controls;

namespace CourseWork_2.ViewControllers;

public class PassportController
{
    private const int MaxSerialLength = 4;
    private const int MaxNumberLength = 6;

    public bool ValidateSerial(Entry entry)
    {
        bool isValid = Validator.ValidateInt(entry.Text, out _) && entry.Text.Length == MaxSerialLength;
        EntryUtil.ChangeEntryColor(entry, isValid);
        return isValid;
    }

    public bool ValidateNumber(Entry entry)
    {
        bool isValid = Validator.ValidateInt(entry.Text, out _) && entry.Text.Length == MaxNumberLength;
        EntryUtil.ChangeEntryColor(entry, isValid);
        return isValid;
    }

    public bool ValidateDateOfIssue(DatePicker dateOfIssueEntry)
    {
        return dateOfIssueEntry.Date <= DateTime.Now;
    }

    public bool ValidateWhoIssued(Entry entry)
    {
        bool isValid = !string.IsNullOrWhiteSpace(entry.Text);
        EntryUtil.ChangeEntryColor(entry, !isValid);
        return isValid;
    }

    public async Task<bool> ValidateInputs(Entry serialEntry, Entry numberEntry, DatePicker dateOfIssueEntry, Entry whoIssuedEntry)
    {
        if (!ValidateSerial(serialEntry))
        {
            await DisplayAlert("Ошибка валидации", "Серия должна быть 4-значным числом.", "OK");
            return false;
        }

        if (!ValidateNumber(numberEntry))
        {
            await DisplayAlert("Ошибка валидации", "Номер должен быть 6-значным числом.", "OK");
            return false;
        }

        if (!ValidateDateOfIssue(dateOfIssueEntry))
        {
            await DisplayAlert("Ошибка валидации", "Дата выдачи не может быть в будущем.", "OK");
            return false;
        }

        if (!ValidateWhoIssued(whoIssuedEntry))
        {
            await DisplayAlert("Ошибкаа валидации", "Поле 'Кем выдан' обязательно.", "OK");
            return false;
        }

        return true;
    }

    private Task DisplayAlert(string title, string message, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}