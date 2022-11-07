using modkaz.Backend.Interfaces;
using modkaz.DBs;
using modkaz.DBs.Entities;

namespace modkaz.Backend.Repositories;

public class ReviewsRepository : BaseRepository<ReviewsEntity>, IReviewsRepository
{
    private readonly MyDatabase _context;

    public ReviewsRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}