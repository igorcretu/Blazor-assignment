using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface ICommentLogic
{
    Task<Comment> CreateCommentAsync(CommentCreationDto dto);
    Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto dto);
}