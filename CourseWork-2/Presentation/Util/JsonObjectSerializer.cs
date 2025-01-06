using System.Text.Json;

namespace CourseWork_2.Presentation.Util;

public class JsonObjectSerializer 
{
    public T? Deserialize<T>(string data)
    {
        return JsonSerializer.Deserialize<T>(data, Options);
    }

    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }

    private static readonly JsonSerializerOptions Options = new()
    {
        IncludeFields = true,
    };
}