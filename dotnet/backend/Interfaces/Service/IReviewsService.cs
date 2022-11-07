using modkaz.DBs.Entities;

namespace modkaz.Backend.Interfaces.Service;

public interface IReviewsService : IMyService<ReviewsEntity>
{
    Task<List<ReviewsEntity>> GetReviewsByUserIdAsync(int userId);
}