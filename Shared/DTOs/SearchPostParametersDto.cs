namespace Shared.DTOs;

public class SearchPostParametersDto
{
    public int? UserId { get; }
    public string? Username { get; }
    public string? Email { get; }
    public string? TitleContains { get; }

    public SearchPostParametersDto(int? userId, string? username, string? email, string? titleContains)
    {
        UserId = userId;
        Username = username;
        Email = email;
        TitleContains = titleContains;
    }
}