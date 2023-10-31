using Application.DaoInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace FileData.DAOs;

public class CommentFileDao : ICommentDao
{
    private readonly FileContext context;
    
    public CommentFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<Comment> CreateCommentAsync(Comment comment)
    {
        int commentId = 1;
        if (context.Comments.Any())
        {
            commentId = context.Comments.Max(u => u.Id);
            commentId++;
        }
        
        comment.Id = commentId;
        context.Comments.Add(comment);
        context.SaveChanges();
        
        return Task.FromResult(comment);
    }

    public Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters)
    {
        IEnumerable<Comment> comments = context.Comments.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            comments = context.Comments.Where(comment =>
                comment.Owner.Username.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParameters.UserId != null)
        {
            comments = context.Comments.Where(comment => comment.Owner.Id == searchParameters.UserId);
        }
        
        if (searchParameters.PostId != null)
        {
            comments = context.Comments.Where(comment => comment.Post.Id == searchParameters.PostId);
        }

        if (searchParameters.Date != null)
        {
            comments = context.Comments.Where(comment => comment.Date == searchParameters.Date);
        }

        return Task.FromResult(comments);
    }
}