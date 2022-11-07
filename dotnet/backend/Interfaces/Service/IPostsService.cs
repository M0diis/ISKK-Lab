using db.Entities;

namespace modkaz.Backend.Interfaces.Service;

public interface IPostsService : IMyService<PostsEntity>
{
    Task<List<PostsEntity>> GetPostsByUserIdAsync(int userId);
}