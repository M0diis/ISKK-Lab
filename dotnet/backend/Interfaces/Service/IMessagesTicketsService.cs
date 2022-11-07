using db.Entities;

namespace modkaz.Backend.Interfaces;

public interface IMessagesTicketsService : IMyService<MessagesTicketsEntity>
{
    Task<List<MessagesTicketsEntity>> GetByTicketIdAsync(int ticketId);
    
    Task<List<MessagesTicketsEntity>> GetByMessageIdAsync(int messageId);
}