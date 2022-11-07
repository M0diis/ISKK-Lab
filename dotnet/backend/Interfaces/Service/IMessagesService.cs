using db.Entities;

namespace modkaz.Backend.Interfaces;

public interface IMessagesService : IMyService<MessagesEntity>
{
    Task<List<MessagesEntity>> GetMessagesByUserIdAsync(int userId);
    
    Task<List<MessagesEntity>> GetMessagesByIds(List<int> ids);
}