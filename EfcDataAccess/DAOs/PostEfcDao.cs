using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTOs;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class PostEfcDao : IPostDao
{
    private readonly ForumContext context;

    public PostEfcDao(ForumContext context)
    {
        this.context = context;
    }
    
    public async Task<Post> CreatePostAsync(Post post)
    {
        EntityEntry<Post> newPost = await context.Posts.AddAsync(post);
        await context.SaveChangesAsync();
        return newPost.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> query = context.Posts.Include(post => post.Owner).AsQueryable();
        
        if (searchParameters.UserId != null)
        {
            query = query.Where(post => post.Owner.Id == searchParameters.UserId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(post => post.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            query = query.Where(post => post.Owner.Username.Equals(searchParameters.Username));
        }

        if (!string.IsNullOrEmpty(searchParameters.Email))
        {
            query = query.Where(post => post.Owner.Email.ToLower().Equals(searchParameters.Email.ToLower()));
        }
        
        List<Post> posts = await query.ToListAsync();
        return posts;
    }

    public async Task UpdateAsync(Post post)
    {
        context.Posts.Update(post);
        await context.SaveChangesAsync();
    }

    public async Task<Post?> GetByIdAsync(int postId)
    {
        Post? post = await context.Posts
            .Include(post => post.Owner)
            .FirstOrDefaultAsync(post => post.Id == postId);
        return post;
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        context.Posts.Remove(post);
        await context.SaveChangesAsync();
    }
}