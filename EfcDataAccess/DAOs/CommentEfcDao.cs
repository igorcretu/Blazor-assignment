using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTOs;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    private readonly ForumContext context;
    
    public CommentEfcDao(ForumContext context)
    {
        this.context = context;
    }
    
    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        EntityEntry<Comment> newComment = await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return newComment.Entity;
    }

    public async Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters)
    {
        IQueryable<Comment> query = context.Comments
            .Include(comment => comment.Owner)
            .Include(comment => comment.Post)
            .ThenInclude(post => post.Owner)
            .AsQueryable();

        if (searchParameters.UserId != null)
        {
            query = query.Where(comment => comment.Owner.Id == searchParameters.UserId);
        }

        if (searchParameters.PostId != null)
        {
            query = query.Where(comment => comment.Post.Id == searchParameters.PostId);
        }
        
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            query = query.Where(comment => comment.Owner.Username.Equals(searchParameters.Username));
        }

        if (searchParameters.Date != null)
        {
            query = query.Where(comment => comment.Date.Equals(searchParameters.Date));
        }
        
        List<Comment> comments = await query.ToListAsync();
        return comments;
    }
}