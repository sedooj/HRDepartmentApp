using CourseWork_2.Data.Service;
using CourseWork_2.Domain.Models;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.ViewControllers
{
    public class UserCreationViewController
    {
        public HumanDataHolder? HumanData { get; set; }
        private readonly LocalStorageService<Human> _localStorageService = new();

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

                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoryPath = Path.Combine(documentsPath, "SavedHumans");

                await _localStorageService.SaveEntityAsync(directoryPath, human);

                Console.WriteLine("Human entity created and saved successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Human entity: {ex}");
                return false;
            }
        }

        public async Task<IEnumerable<Human>> LoadHumans()
        {
            try
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoryPath = Path.Combine(documentsPath, "SavedHumans");

                var humans = await _localStorageService.LoadEntitiesAsync(directoryPath);
                Console.WriteLine("Human entities loaded successfully.");
                return humans;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading Human entities: {ex}");
                return new List<Human>();
            }
        }

        private Task DisplayAlert(string title, string message, string cancel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
public class HumanDataHolder
{
    public Passport Passport { get; set; }
    public UserDefaultCredentials UserDefaultCredentials { get; set; }
    public EducationDocument EducationDocument { get; set; }
}