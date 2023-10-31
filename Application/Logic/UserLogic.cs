using System.Net.Mail;
using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> RegisterAsync(UserRegistrationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.Username);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateUserData(dto);
        User toCreate = new User(dto.Username, dto.Password, dto.Email, dto.Role);
        User created = await userDao.RegisterAsync(toCreate);
        return created;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await userDao.GetByUsernameAsync(username);
    }

    private static void ValidateUserData(UserRegistrationDto dto)
    {
        string username = dto.Username;
        string password = dto.Password;
        string role = dto.Role;
        string email = dto.Email;
        
        if (username.Length < 3)
            throw new Exception("Username must be at least 3 characters!");
        if (username.Length > 16)
            throw new Exception("Username must be less than 16 characters!");
        if (password.Length < 5 || string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            throw new Exception("Password must be at least 5 characters!");
        if (role.Length < 3)
            throw new Exception("Role must be at least 3 characters!");
        if (string.IsNullOrWhiteSpace(email)) 
            throw new Exception("Email cannot be empty!");
        try
        {
            MailAddress m = new MailAddress(email);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}