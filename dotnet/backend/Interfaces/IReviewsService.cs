using modkaz.DBs.Entities;

namespace modkaz.Backend.Interfaces;

public interface IReviewsService
{
    Task<List<ReviewsEntity>> GetReviewsAsync();
    
    Task<List<ReviewsEntity>> GetReviewsByUserAsync(int userId);
}