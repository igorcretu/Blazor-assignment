using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface ICommentDao
{
    Task<Comment> CreateCommentAsync(Comment comment);
    Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto searchParameters);
}