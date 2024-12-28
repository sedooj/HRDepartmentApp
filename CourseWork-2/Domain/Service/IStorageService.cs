namespace CourseWork_2.Domain.Service;

public interface IStorage<T> where T : class
{
    public T? Load(string directoryPath);
    public bool Save(string directoryPath, T entity);
}