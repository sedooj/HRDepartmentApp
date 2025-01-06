using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.ViewModels.UserCreation;

public class UserDefaultCredentialsViewModel
{
    public bool ValidateFirstName(Entry entry)
    {
        bool isValid = !string.IsNullOrWhiteSpace(entry.Text);
        EntryUtil.ChangeEntryColor(entry, isValid);
        return isValid;
    }

    public bool ValidateLastName(Entry entry)
    {
        bool isValid = !string.IsNullOrWhiteSpace(entry.Text);
        EntryUtil.ChangeEntryColor(entry, isValid);
        return isValid;
    }

    public bool ValidateSecondName(Entry entry)
    {
        bool isValid = !string.IsNullOrWhiteSpace(entry.Text);
        EntryUtil.ChangeEntryColor(entry, isValid);
        return isValid;
    }

    public bool ValidateDateOfBirth(DatePicker datePicker)
    {
        bool isValid = datePicker.Date <= DateTime.Now;
        return isValid;
    }

    public bool ValidateHomeAddress(Entry entry)
    {
        bool isValid = !string.IsNullOrWhiteSpace(entry.Text);
        EntryUtil.ChangeEntryColor(entry, isValid);
        return isValid;
    }

    public bool ValidatePhoneNumber(Entry entry)
    {
        bool isValid = !string.IsNullOrWhiteSpace(entry.Text) && entry.Text.All(char.IsDigit);
        EntryUtil.ChangeEntryColor(entry, isValid);
        return isValid;
    }

    public async Task<bool> ValidateInputs(Entry firstNameEntry, Entry lastNameEntry, Entry secondNameEntry,
        DatePicker dateOfBirthDatePicker, Entry homeAddressEntry, Entry phoneNumberEntry)
    {
        if (!ValidateFirstName(firstNameEntry))
        {
            await DisplayAlert("Ошибка валидации", "Имя обязательно.", "OK");
            return false;
        }

        if (!ValidateLastName(lastNameEntry))
        {
            await DisplayAlert("Ошибка валидации", "Фамилия обязательна.", "OK");
            return false;
        }

        if (!ValidateSecondName(secondNameEntry))
        {
            await DisplayAlert("Ошибка валидации", "Отчество обязательно.", "OK");
            return false;
        }

        if (!ValidateDateOfBirth(dateOfBirthDatePicker))
        {
            await DisplayAlert("Ошибка валидации", "Дата рождения не может быть в будущем.", "OK");
            return false;
        }

        if (!ValidateHomeAddress(homeAddressEntry))
        {
            await DisplayAlert("Ошибка валидации", "Домашний адрес обязателен.", "OK");
            return false;
        }

        if (!ValidatePhoneNumber(phoneNumberEntry))
        {
            await DisplayAlert("Ошибка валидации", "Требуется действительный номер телефона.", "OK");
            return false;
        }

        return true;
    }

    private Task DisplayAlert(string title, string message, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}