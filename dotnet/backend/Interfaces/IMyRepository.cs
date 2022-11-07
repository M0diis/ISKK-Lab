using System.Linq.Expressions;

namespace modkaz.Backend.Interfaces;

public interface IMyRepository<T>
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    
    List<T> FindAllList();
    
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}