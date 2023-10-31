using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreatePostAsync(Post post);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task UpdateAsync(Post post);
    Task<Post?> GetByIdAsync(int postId);
    Task DeleteAsync(int id);
}