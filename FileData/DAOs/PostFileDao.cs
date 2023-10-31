using Application.DaoInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext context;
    
    public PostFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<Post> CreatePostAsync(Post post)
    {
        int postId = 1;
        if (context.Posts.Any())
        {
            postId = context.Posts.Max(u => u.Id);
            postId++;
        }

        post.Id = postId;
        context.Posts.Add(post);
        context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IEnumerable<Post> posts = context.Posts.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            posts = context.Posts.Where(post =>
                post.Owner.Username.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParameters.UserId != null)
        {
            posts = context.Posts.Where(post => post.Owner.Id == searchParameters.UserId);
        }

        if (!string.IsNullOrEmpty(searchParameters.Email))
        {
            posts = context.Posts.Where(post =>
                post.Owner.Email.Equals(searchParameters.Email, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            posts = context.Posts.Where(post =>
                post.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(posts);
    }

    public Task UpdateAsync(Post toUpdate)
    {
        Post? existing = context.Posts.FirstOrDefault(p => p.Id == toUpdate.Id);
        if (existing == null)
        {
            throw new Exception($"Post with id {toUpdate.Id} was not found.");
        }

        context.Posts.Remove(existing);
        context.Posts.Add(toUpdate);
        
        context.SaveChanges();

        return Task.CompletedTask;
    }

    public Task<Post?> GetByIdAsync(int postId)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.Id == postId);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        Post? existing = context.Posts.FirstOrDefault(post => post.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} was not found.");
        }
        
        context.Posts.Remove(existing);
        context.SaveChanges();

        return Task.CompletedTask;
    }
}