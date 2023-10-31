using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> RegisterUser(UserRegistrationDto dto);
    Task<User> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto dto);
}