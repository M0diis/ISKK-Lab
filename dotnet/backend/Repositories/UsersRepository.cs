using modkaz.Backend.Entities;
using modkaz.Backend.Interfaces;
using modkaz.DBs;

namespace modkaz.Backend.Repositories;

public class UsersRepository : BaseRepository<UserEntity>, IUsersRepository
{
    private readonly MyDatabase _context;

    public UsersRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}