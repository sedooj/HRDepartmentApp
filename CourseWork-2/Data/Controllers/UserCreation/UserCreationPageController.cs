using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Models;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Controllers.UserCreation
{
    public class UserCreationPageController
    {

        private readonly IStorage<Human> _humanService = new LocalStorageService<Human>();
        public HumanDataHolder HumanData { get; set; } = new();

        private bool ValidateHuman()
        {
            return Validator.ValidateHuman(HumanData);
        }

        public bool CreateHuman()
        {
            try
            {
                if (!ValidateHuman())
                {
                    DisplayAlert("Ошибка валидации", "Некоторые поля заполнены неверно.", "OK");
                    return false;
                }

                if (HumanData.Passport == null || HumanData.UserDefaultCredentials == null ||
                    HumanData.EducationDocument == null) return false;

                Human human = new Human(
                    Guid.NewGuid(),
                    HumanData.Passport,
                    HumanData.UserDefaultCredentials,
                    new List<EmploymentHistoryRecord>(),
                    new Education(
                        Guid.NewGuid(),
                        HumanData.EducationDocument.Institution,
                        HumanData.EducationDocument.GraduatedDate,
                        HumanData.EducationDocument.Specialty
                    ),
                    HumanData.EducationDocument
                );
                _humanService.SaveEntity($"{Config.HumanStoragePath}{human.Uuid.ToString()}", human);
                // string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                // string directoryPath = Path.Combine(documentsPath, "humans");
                //
                // if (!Directory.Exists(directoryPath))
                // {
                //     Directory.CreateDirectory(directoryPath);
                // }
                //
                // string filePath = Path.Combine(directoryPath, $"{human.Uuid.ToString()}.json");
                // string json = new JsonObjectSerializer().Serialize(human);
                // File.WriteAllText(filePath, json);
                //
                // Console.WriteLine("Human entity created and saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Human entity: {ex}");
                return false;
            }
        }

        public void UpdatePassport(Passport passport)
        {
            HumanData.Passport = passport;
        }

        public void UpdateUserDefaultCredentials(UserDefaultCredentials userDefaultCredentials)
        {
            HumanData.UserDefaultCredentials = userDefaultCredentials;
        }

        public void UpdateEducationDocument(EducationDocument educationDocument)
        {
            HumanData.EducationDocument = educationDocument;
        }

        private Task DisplayAlert(string title, string message, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}