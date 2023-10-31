using Shared.Models;

namespace Forum.Services;

public interface IAuthService
{
    Task<User> ValidateUser(string username, string password);
    Task RegisterUser(User user);
}