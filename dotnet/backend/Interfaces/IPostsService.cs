using modkaz.DBs.Entities;

namespace modkaz.Backend.Interfaces;

public interface IPostsService
{
    Task<PostsEntity> GetPostByIdAsync(int id);
    
    Task<List<PostsEntity>> GetPostsAsync();
    
    Task<List<PostsEntity>> GetPostsByUserAsync(int userId);
    
    void CreatePost(PostsEntity postsEntity);
    void DeletePost(PostsEntity postsEntity);
}