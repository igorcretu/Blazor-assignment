using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<PostBasicDto> GetByIdAsync(string id);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto dto);
    Task<Post> CreateAsync(PostCreationDto dto);
}