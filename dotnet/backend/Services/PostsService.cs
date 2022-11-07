using Microsoft.EntityFrameworkCore;
using modkaz.Backend.Interfaces;
using modkaz.DBs.Entities;

namespace modkaz.Backend.Services;

public class PostsService : IPostsService
{
    private readonly IPostsRepository _postsRepository;
    
    public PostsService(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }

    public Task<PostsEntity> GetPostByIdAsync(int id)
    {
        return _postsRepository.FindByCondition(x => x.id == id).FirstOrDefaultAsync();
    }

    public async Task<List<PostsEntity>> GetPostsAsync()
    {
        return _postsRepository.FindAllList();
    }
    
    public async Task<List<PostsEntity>> GetPostsByUserAsync(int userId)
    {
        return _postsRepository.FindAll()
            .Where(x => x.fk_userId == userId).ToList();
    }

    public void CreatePost(PostsEntity postsEntity)
    {
        _postsRepository.Create(postsEntity);
    }

    public void DeletePost(PostsEntity postsEntity)
    {
        _postsRepository.Delete(postsEntity);
    }
}