using db.Entities;
using Microsoft.EntityFrameworkCore;
using modkaz.Backend.Interfaces.Repository;
using modkaz.Backend.Interfaces.Service;

namespace modkaz.Backend.Services;

public class ReviewsService : IReviewsService
{
    private readonly IReviewsRepository _reviewsRepository;
    
    public ReviewsService(IReviewsRepository reviewsRepository)
    {
        _reviewsRepository = reviewsRepository;
    }
    
    public Task<List<ReviewsEntity>> GetAllAsync()
    {
        return Task.FromResult(_reviewsRepository.FindAll().ToList());
    }

    public Task<ReviewsEntity> GetOneByIdAsync(int id)
    {
        return _reviewsRepository.FindByCondition(x => x.id == id).FirstOrDefaultAsync();
    }

    public void Create(ReviewsEntity entity)
    {
        _reviewsRepository.Create(entity);
    }

    public void Delete(ReviewsEntity entity)
    {
        _reviewsRepository.Delete(entity);
    }

    public Task<List<ReviewsEntity>> GetReviewsByUserIdAsync(int userId)
    {
        return Task.FromResult(_reviewsRepository.FindAll()
            .Where(x => x.fk_userId == userId)
            .ToList());
    }
}