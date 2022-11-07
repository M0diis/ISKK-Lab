using db;
using db.Entities;
using modkaz.Backend.Interfaces.Repository;

namespace modkaz.Backend.Repositories;

public class ReviewsRepository : BaseRepository<ReviewsEntity>, IReviewsRepository
{
    private readonly MyDatabase _context;

    public ReviewsRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}