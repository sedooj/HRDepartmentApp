namespace CourseWork_2.Domain.Service;

public interface IStorage<T> where T : class
{
    Task<IEnumerable<T>> LoadEntitiesAsync(string directoryPath);
    Task SaveEntityAsync(string directoryPath, T entity);
}