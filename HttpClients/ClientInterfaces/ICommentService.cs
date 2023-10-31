using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface ICommentService
{
    Task<Comment> CreateCommentAsync(CommentCreationDto dto);
    Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto dto);
}