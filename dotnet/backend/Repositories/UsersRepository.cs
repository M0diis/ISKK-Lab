using db;
using db.Entities;
using modkaz.Backend.Interfaces.Repository;

namespace modkaz.Backend.Repositories;

public class UsersRepository : BaseRepository<UsersEntity>, IUsersRepository
{
    private readonly MyDatabase _context;

    public UsersRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}