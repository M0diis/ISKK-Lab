using db.Entities;
using Microsoft.EntityFrameworkCore;
using modkaz.Backend.Interfaces;
using modkaz.Backend.Interfaces.Repository;

namespace modkaz.Backend.Services;

public class MessagesService : IMessagesService
{
    private readonly IMessagesRepository _messagesRepository;
    
    public MessagesService(IMessagesRepository messagesRepository)
    {
        _messagesRepository = messagesRepository;
    }
    
    public Task<List<MessagesEntity>> GetAllAsync()
    {
        return Task.FromResult(_messagesRepository.FindAll().ToList());
    }

    public Task<MessagesEntity> GetOneByIdAsync(int id)
    {
        return _messagesRepository.FindByCondition(x => x.id == id)
            .FirstOrDefaultAsync();
    }

    public void Create(MessagesEntity entity)
    {
        _messagesRepository.Create(entity);
    }

    public void Delete(MessagesEntity entity)
    {
        _messagesRepository.Delete(entity);
    }

    public Task<List<MessagesEntity>> GetMessagesByUserIdAsync(int userId)
    {
        return Task.FromResult(_messagesRepository.FindAll()
            .Where(x => x.fk_userId == userId)
            .ToList());
    }

    public Task<List<MessagesEntity>> GetMessagesByIds(List<int> ids)
    {
        return Task.FromResult(_messagesRepository.FindAll()
            .Where(x => ids.Contains(x.id))
            .ToList());
    }
}