using CourseWork_2.Models;
using System.Diagnostics;
using CourseWork.entity;
using System.IO;
using CourseWork_2.Storage;

namespace CourseWork_2.ViewControllers;

public class UserCreationViewController
{
    // public HumanDataHolder HumanData { get; set; } = new()
    // {
    //     Passport = new Passport("", "", DateTime.Now, ""),
    //     UserDefaultCredentials = new UserDefaultCredentials("", "", "", DateTime.Now, "", "", ""),
    //     EducationDocument = new EducationDocument(0, "", DateTime.Now, "", "", "",
    //         EducationDocument.EducationLevels.Doctorate, "", DateTime.Now)
    // };
    // public HumanDataHolder HumanData { get; set; } = new()
    // {
    //     Passport = new Passport("1233", "121212", DateTime.Now, "сын шлюхи"),
    //     UserDefaultCredentials = new UserDefaultCredentials("Пидор", "Гондон", "Иван", DateTime.Now, "залупа", "1231", ""),
    //     EducationDocument = new EducationDocument(228, "Хуета", DateTime.Now, "Пидорас", "123122", "1212121",
    //         EducationDocument.EducationLevels.Doctorate, "Залупа", DateTime.Now)
    // };
    public HumanDataHolder? HumanData { get; set; }

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
            Console.WriteLine($"Directory path: {directoryPath}");
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine("Directory created successfully.");

            string filePath = Path.Combine(directoryPath, $"{Guid.NewGuid()}.json");
            Console.WriteLine($"File path: {filePath}");

            JsonObjectSerializer serializer = new JsonObjectSerializer();
            string jsonString = serializer.Serialize(human);
            await File.WriteAllTextAsync(filePath, jsonString);
            Console.WriteLine("File written successfully.");

            Console.WriteLine("Human entity created and saved successfully.");
            Console.WriteLine("Success");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating Human entity: {ex}");
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
    
    public async Task<HumanDataHolder?> LoadHumanDataFromFile(string fileName)
    {
        try
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "SavedHumans");
            string filePath = Path.Combine(directoryPath, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return null;
            }

            string jsonString = await File.ReadAllTextAsync(filePath);
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            Human? human = serializer.Deserialize<Human>(jsonString);

            if (human == null)
            {
                Console.WriteLine("Failed to deserialize the file.");
                return null;
            }

            var loadedHumanData = HumanData = new HumanDataHolder
            {
                Passport = human.Passport,
                UserDefaultCredentials = human.UserDefaultCredentials,
                EducationDocument = human.EducationDocument
            };

            Console.WriteLine("Human data loaded successfully.");
            return loadedHumanData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading Human data: {ex}");
        }

        return null;
    }
}

public class HumanDataHolder
{
    public Passport Passport { get; set; }
    public UserDefaultCredentials UserDefaultCredentials { get; set; }
    public EducationDocument EducationDocument { get; set; }
}