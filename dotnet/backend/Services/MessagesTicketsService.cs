using db.Entities;
using Microsoft.EntityFrameworkCore;
using modkaz.Backend.Interfaces;
using modkaz.Backend.Interfaces.Repository;

namespace modkaz.Backend.Services;

public class MessagesTicketsService : IMessagesTicketsService
{
    private readonly IMessagesTicketsRepository _messagesTicketsRepository;
    
    public MessagesTicketsService(IMessagesTicketsRepository messagesTicketsRepository)
    {
        _messagesTicketsRepository = messagesTicketsRepository;
    }
    
    public Task<List<MessagesTicketsEntity>> GetAllAsync()
    {
        return Task.FromResult(_messagesTicketsRepository.FindAll().ToList());
    }

    public void Create(MessagesTicketsEntity entity)
    {
        _messagesTicketsRepository.Create(entity);
    }

    public void Delete(MessagesTicketsEntity entity)
    {
        _messagesTicketsRepository.Delete(entity);
    }


    public Task<List<MessagesTicketsEntity>> GetByTicketIdAsync(int ticketId)
    {
        return Task.FromResult(_messagesTicketsRepository
            .FindByCondition(x => x.fk_ticketId == ticketId)
            .ToList());
    }

    public Task<List<MessagesTicketsEntity>> GetByMessageIdAsync(int messageId)
    {
        return Task.FromResult(_messagesTicketsRepository
            .FindByCondition(x => x.fk_messageId == messageId)
            .ToList());
    }
    
    public Task<MessagesTicketsEntity> GetOneByIdAsync(int id)
    {
        throw new NotImplementedException("Not implementable for ticket-messages.");
    }
    
    public Task<List<MessagesTicketsEntity>> GetMessagesByUserIdAsync(int userId)
    {
        throw new NotImplementedException("Not implementable for ticket-messages.");
    }
}