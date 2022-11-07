using modkaz.Backend.Interfaces.Repository;
using modkaz.DBs;
using modkaz.DBs.Entities;

namespace modkaz.Backend.Repositories;

public class PostsRepository : BaseRepository<PostsEntity>, IPostsRepository
{
    private readonly MyDatabase _context;

    public PostsRepository(MyDatabase context) : base(context)
    {
        this._context = context;
    }
}