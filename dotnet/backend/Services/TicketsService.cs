using db.Entities;
using Microsoft.EntityFrameworkCore;
using modkaz.Backend.Interfaces;
using modkaz.Backend.Interfaces.Repository;

namespace modkaz.Backend.Services;

public class TicketsService : ITicketsService
{
    private readonly ITicketsRepository _ticketsRepository;
    
    public TicketsService(ITicketsRepository ticketsRepository)
    {
        _ticketsRepository = ticketsRepository;
    }
    
    public Task<List<TicketsEntity>> GetAllAsync()
    {
        return Task.FromResult(_ticketsRepository.FindAll().ToList());
    }

    public Task<TicketsEntity> GetOneByIdAsync(int id)
    {
        return _ticketsRepository.FindByCondition(x => x.id == id)
            .FirstOrDefaultAsync();
    }

    public void Create(TicketsEntity entity)
    {
        _ticketsRepository.Create(entity);
    }

    public void Delete(TicketsEntity entity)
    {
        _ticketsRepository.Delete(entity);
    }

    public Task<List<TicketsEntity>> GetTicketsByUserIdAsync(int userId)
    {
        return Task.FromResult(_ticketsRepository.FindAll()
            .Where(x => x.fk_userId == userId)
            .ToList());
    }
}