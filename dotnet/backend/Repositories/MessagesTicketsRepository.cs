using db;
using db.Entities;
using modkaz.Backend.Interfaces.Repository;

namespace modkaz.Backend.Repositories;

public class MessagesTicketsRepository : BaseRepository<MessagesTicketsEntity>, IMessagesTicketsRepository
{
    private readonly MyDatabase _context;

    public MessagesTicketsRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}