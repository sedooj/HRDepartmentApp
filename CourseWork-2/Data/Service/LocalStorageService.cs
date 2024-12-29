using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Service;

public class LocalStorageService<T> : IStorage<T> where T : class
{
    private readonly JsonObjectSerializer _serializer = new();

    public async Task<IEnumerable<T>> LoadEntitiesAsync(string directoryPath)
    {
        List<T> entities = new List<T>();

        if (Directory.Exists(directoryPath))
        {
            var files = Directory.GetFiles(directoryPath, "*.json");
            foreach (var file in files)
            {
                var jsonString = await File.ReadAllTextAsync(file);
                var entity = _serializer.Deserialize<T>(jsonString);
                if (entity != null)
                {
                    entities.Add(entity);
                }
            }
        }

        return entities;
    }

    public async Task SaveEntityAsync(string directoryPath, T entity)
    {
        Directory.CreateDirectory(directoryPath);
        string filePath = Path.Combine(directoryPath, $"_userCreationData.json");
        var jsonString = _serializer.Serialize(entity);
        await File.WriteAllTextAsync(filePath, jsonString);
    }
}