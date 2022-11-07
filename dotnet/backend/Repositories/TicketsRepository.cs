using db;
using db.Entities;
using modkaz.Backend.Interfaces.Repository;

namespace modkaz.Backend.Repositories;

public class TicketsRepository : BaseRepository<TicketsEntity>, ITicketsRepository
{
    private readonly MyDatabase _context;

    public TicketsRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}