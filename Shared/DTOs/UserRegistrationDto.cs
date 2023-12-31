﻿namespace Shared.DTOs;

public class UserRegistrationDto
{
    public string Username { get; }
    public string Password { get; }
    public string Email { get; }
    public string Role { get; }

    public UserRegistrationDto(string username, string password, string email, string role)
    {
        Username = username;
        Password = password;
        Email = email;
        Role = role;
    }
}