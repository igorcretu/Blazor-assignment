using System.ComponentModel.DataAnnotations;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Forum.Services;

public class AuthService : IAuthService
{
    private readonly IUserLogic userLogic;

    public AuthService(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    
    public async Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = await userLogic.GetByUsernameAsync(username);

        if (existingUser == null)
        {
            throw new Exception("User not found!");
        }
        
        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Incorrect password!");
        }

        return await Task.FromResult(existingUser);
    }

    public async Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be empty!");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be empty!");
        }

        if (string.IsNullOrEmpty(user.Email))
        {
            throw new ValidationException("Email cannot be empty!");
        }

        if (string.IsNullOrEmpty(user.Role))
        {
            throw new ValidationException("Role cannot be empty!");
        }

        await userLogic.RegisterAsync(new UserRegistrationDto(user.Username, user.Password, user.Email, user.Role));
    }
}