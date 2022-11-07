using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using modkaz.Backend.Interfaces;
using modkaz.DBs;

namespace modkaz.Backend.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    private readonly MyDatabase _context;

    public BaseRepository(MyDatabase context)
    {
        _context = context;
    }

    public IQueryable<T> FindAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression).AsNoTracking();
    }

    public void Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }
        
}