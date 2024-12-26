using CourseWork_2.Models;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using CourseWork_2.Models.employmentHistory;
using CourseWork.entity;

namespace CourseWork_2.ViewControllers;

public class UserCreationViewController
{
    public HumanDataHolder HumanData { get; set; } = new()
    {
        Passport = new Passport("", "", DateTime.Now, ""),
        UserDefaultCredentials = new UserDefaultCredentials("", "", "", DateTime.Now, "", "", ""),
        EducationDocument = new EducationDocument(0, "", DateTime.Now, "", "", "",
            EducationDocument.EducationLevels.Doctorate, "", DateTime.Now)
    };

    private bool ValidateHuman()
    {
        return Validator.ValidateHuman(HumanData);
    }

    public async Task<bool> CreateHuman()
    {
        try
        {
            if (!ValidateHuman())
            {
                await DisplayAlert("Ошибка валидации", "Некоторые поля заполнены неверно.", "OK");
                return false;
            }

            // Logic to create and save the Human instance
            Human human = new Human(
                HumanData.Passport,
                HumanData.UserDefaultCredentials,
                new List<EmploymentHistoryRecord>(),
                new Education(
                    0,
                    HumanData.EducationDocument.Institution,
                    HumanData.EducationDocument.GraduatedDate,
                    HumanData.EducationDocument.Specialty
                ),
                HumanData.EducationDocument
            );

            // Simulate saving the Human instance
            Debug.WriteLine("Human entity created successfully.");
            Console.WriteLine("Success");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating Human entity: {ex}");
            Console.WriteLine($"Error: {ex}");
            return false;
        }
    }

    public void UpdatePassport(Passport passport)
    {
        HumanData.Passport = passport;
    }

    public void UpdateUserDefaultCredentials(UserDefaultCredentials credentials)
    {
        HumanData.UserDefaultCredentials = credentials;
    }

    public void UpdateEducationDocument(EducationDocument document)
    {
        HumanData.EducationDocument = document;
    }

    private Task DisplayAlert(string title, string message, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, cancel);
    }
}

public class HumanDataHolder
{
    public Passport Passport { get; set; }
    public UserDefaultCredentials UserDefaultCredentials { get; set; }
    public EducationDocument EducationDocument { get; set; }
}