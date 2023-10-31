using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;
    
    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }
    
    public async Task<Post> CreatePostAsync(PostCreationDto dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.OwnerUsername);
        if (user == null)
            throw new Exception($"User with username {dto.OwnerUsername} was not found.");
        
        ValidatePostData(dto);
        Post toCreate = new Post(user, dto.Title, dto.Body);
        Post created = await postDao.CreatePostAsync(toCreate);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto dto)
    {
        return postDao.GetAsync(dto);
    }

    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await postDao.GetByIdAsync(dto.Id);
        
        if (existing == null)
            throw new Exception($"Post with id {dto.Id} was not found.");
        
        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
                throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        string bodyToUse = dto.Body ?? existing.Body;
        
        Post updated = new(userToUse, titleToUse, bodyToUse)
        {
            Id = existing.Id,
            Owner = userToUse,
            Title = titleToUse,
            Body = bodyToUse
        };
        
        ValidatePostData(updated);
        
        await postDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with {id} not found.");
        }
        
        await postDao.DeleteAsync(id);
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found.");
        }

        return new PostBasicDto(post.Id, post.Owner.Username, post.Title, post.Body);
    }

    private static void ValidatePostData(PostCreationDto dto)
    {
        if (dto.Title.Length < 5 || string.IsNullOrEmpty(dto.Title))
            throw new Exception("Title must be at least 5 characters or cannot be empty!");
    }
    
    private static void ValidatePostData(Post post)
    {
        if (post.Title.Length < 5 || string.IsNullOrEmpty(post.Title))
            throw new Exception("Title must be at least 5 characters or cannot be empty!");
    }
}