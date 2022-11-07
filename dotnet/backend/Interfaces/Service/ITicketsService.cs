using modkaz.DBs.Entities;

namespace modkaz.Backend.Interfaces;

public interface ITicketsService : IMyService<TicketsEntity>
{
    Task<List<TicketsEntity>> GetTicketsByUserIdAsync(int userId);
}