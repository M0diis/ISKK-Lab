using modkaz.Backend.Interfaces;
using modkaz.DBs.Entities;

namespace modkaz.Backend.Services;

public class ReviewsService : IReviewsService
{
    private readonly IReviewsRepository _reviewsRepository;
    
    public ReviewsService(IReviewsRepository reviewsRepository)
    {
        _reviewsRepository = reviewsRepository;
    }
    
    public async Task<List<ReviewsEntity>> GetReviewsAsync()
    {
        return _reviewsRepository.FindAllList();
    }
    
    public async Task<List<ReviewsEntity>> GetReviewsByUserAsync(int userId)
    {
        return _reviewsRepository.FindAll()
            .Where(x => x.fk_userId == userId).ToList();
    }
}