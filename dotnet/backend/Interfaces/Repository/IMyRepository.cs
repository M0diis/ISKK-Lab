using System.Linq.Expressions;

namespace modkaz.Backend.Interfaces.Repository;

public interface IMyRepository<T>
{
    IQueryable<T> FindAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);

    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}