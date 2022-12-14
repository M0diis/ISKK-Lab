using db;
using db.Entities;
using modkaz.Backend.Interfaces.Repository;

namespace modkaz.Backend.Repositories;

public class PostsRepository : BaseRepository<PostsEntity>, IPostsRepository
{
    private readonly MyDatabase _context;

    public PostsRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}