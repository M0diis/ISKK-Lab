namespace modkaz.Backend.Interfaces;

public interface IMyService<T>
{
    Task<List<T>> GetAllAsync();
    
    Task<T> GetOneById(int id);
    
    void Create(T entity);
    void Delete(T entity);
}