using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using modkaz.Backend.Interfaces;
using modkaz.DBs;

namespace modkaz.Backend.Repositories;

public class BaseRepository<T> : IMyRepository<T> where T : class
{
    private readonly MyDatabase _context;

    public BaseRepository(MyDatabase context)
    {
        this._context = context;
    }

    public IQueryable<T> FindAll()
    {
        return this._context.Set<T>().AsNoTracking();
    }
    
    public List<T> FindAllList()
    {
        return this._context.Set<T>().AsNoTracking().ToList();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return this._context.Set<T>().Where(expression).AsNoTracking();
    }

    public void Create(T entity)
    {
        this._context.Set<T>().Add(entity);
        this._context.SaveChanges();
    }

    public void Update(T entity)
    {
        this._context.Set<T>().Update(entity);
        this._context.Entry(entity).State = EntityState.Modified;
        this._context.SaveChanges();

    }

    public void Delete(T entity)
    {
        this._context.Set<T>().Remove(entity);
        this._context.SaveChanges();
    }
}