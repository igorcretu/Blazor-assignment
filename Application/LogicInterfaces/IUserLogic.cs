using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> RegisterAsync(UserRegistrationDto dto);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
    Task<User?> GetByUsernameAsync(string username);
}