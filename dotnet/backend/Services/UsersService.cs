using modkaz.Backend.Interfaces;
using modkaz.DBs.Entities;

namespace modkaz.Backend.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _reviewsRepository;
    
    public UsersService(IUsersRepository reviewsRepository)
    {
        _reviewsRepository = reviewsRepository;
    }
    
    public async Task<List<UsersEntity>> GetUsersAsync()
    {
        return _reviewsRepository.FindAllList();
    }
}