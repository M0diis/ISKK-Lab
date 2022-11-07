using db;
using db.Entities;
using modkaz.Backend.Interfaces.Repository;

namespace modkaz.Backend.Repositories;

public class MessagesRepository : BaseRepository<MessagesEntity>, IMessagesRepository
{
    private readonly MyDatabase _context;

    public MessagesRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}