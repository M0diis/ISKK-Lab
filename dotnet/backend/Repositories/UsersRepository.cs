using modkaz.Backend.Interfaces.Repository;
using modkaz.DBs;
using modkaz.DBs.Entities;

namespace modkaz.Backend.Repositories;

public class UsersRepository : BaseRepository<UsersEntity>, IUsersRepository
{
    private readonly MyDatabase _context;

    public UsersRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}