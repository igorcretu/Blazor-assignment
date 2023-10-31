using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> RegisterAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}