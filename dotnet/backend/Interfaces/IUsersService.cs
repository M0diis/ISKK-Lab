using modkaz.DBs.Entities;

namespace modkaz.Backend.Interfaces;

public interface IUsersService
{
    Task<List<UsersEntity>> GetUsersAsync();
}