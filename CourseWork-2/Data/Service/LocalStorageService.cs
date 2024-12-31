using System.Diagnostics;
using CourseWork_2.Domain.Service;
using CourseWork_2.Presentation.Util;

namespace CourseWork_2.Data.Service;

public class LocalStorageService<T> : IStorage<T> where T : class
{
    private readonly JsonObjectSerializer _serializer = new();

    public IEnumerable<T> LoadEntities(string dir)
    {
        List<T> entities = new List<T>();

        if (Directory.Exists(dir))
        {
            var files = Directory.GetFiles(dir, "*.json");
            foreach (var file in files)
            {
                var jsonString = File.ReadAllText(file);
                var entity = _serializer.Deserialize<T>(jsonString);
                if (entity != null)
                {
                    entities.Add(entity);
                }
            }
        }

        return entities;
    }

    public T? LoadEntity(string dir)
    {
        string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(directoryPath, $"{dir}.json");

        if (File.Exists(filePath))
        {
            var jsonString = File.ReadAllText(filePath);
            return _serializer.Deserialize<T>(jsonString);
        }

        return null;
    }

    public void SaveEntity(string dir, T entity)
    {
        try
        {
            string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string fullPath = Path.Combine(directoryPath, dir);
            string directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string filePath = $"{fullPath}.json";
            var jsonString = _serializer.Serialize(entity);
            File.WriteAllText(filePath, jsonString);
            Debug.WriteLine($"Entity {entity} saved successfully.");
        }
        catch (Exception e)
        {
            Debug.WriteLine($"(LocalStorageService) Can't save entity. Reason: {e}");
            throw;
        }
    }

    public void UpdateEntity(string dir, T updatedEntity)
    {
        string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = Path.Combine(directoryPath, $"{dir}.json");

        if (File.Exists(filePath))
        {
            var jsonString = _serializer.Serialize(updatedEntity);
            File.WriteAllText(filePath, jsonString);
            Debug.WriteLine($"Entity {updatedEntity} updated successfully.");
        }
        else
        {
            SaveEntity(dir, updatedEntity);
        }
    }
}