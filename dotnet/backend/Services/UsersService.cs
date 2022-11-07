using Microsoft.EntityFrameworkCore;
using modkaz.Backend.Interfaces.Repository;
using modkaz.Backend.Interfaces.Service;
using modkaz.DBs.Entities;

namespace modkaz.Backend.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    
    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public Task<List<UsersEntity>> GetAllAsync()
    {
        return Task.FromResult(_usersRepository.FindAll().ToList());
    }

    public Task<UsersEntity> GetOneByIdAsync(int id)
    {
        return _usersRepository.FindByCondition(x => x.id == id)
            .FirstOrDefaultAsync();
    }

    public void Create(UsersEntity entity)
    {
        _usersRepository.Create(entity);
    }

    public void Delete(UsersEntity entity)
    {
        _usersRepository.Delete(entity);
    }
}