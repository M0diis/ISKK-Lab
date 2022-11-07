using db.Entities;
using Microsoft.EntityFrameworkCore;
using modkaz.Backend.Interfaces.Repository;
using modkaz.Backend.Interfaces.Service;

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

    public Task<List<PostsEntity>> GetAllAsync()
    {
        return Task.FromResult(_postsRepository.FindAll().ToList());
    }

    public Task<PostsEntity> GetOneByIdAsync(int id)
    {
        return _postsRepository.FindByCondition(x => x.id == id).FirstOrDefaultAsync();
    }

    public Task<List<PostsEntity>> GetPostsByUserIdAsync(int userId)
    {
        return Task.FromResult(_postsRepository.FindAll()
            .Where(x => x.fk_userId == userId)
            .ToList());
    }

    public void Create(PostsEntity postsEntity)
    {
        _postsRepository.Create(postsEntity);
    }

    public void Delete(PostsEntity postsEntity)
    {
        _postsRepository.Delete(postsEntity);
    }
}