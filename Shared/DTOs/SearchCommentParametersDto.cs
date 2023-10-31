namespace Shared.DTOs;

public class SearchCommentParametersDto
{
    public int? UserId { get; }
    public string? Username { get; }
    public int? PostId { get; }
    public DateTime? Date { get; }
    
    public SearchCommentParametersDto(int? userId, string? username, int? postId, DateTime? date)
    {
        UserId = userId;
        Username = username;
        PostId = postId;
        Date = date;
    }
    
    public SearchCommentParametersDto(int? postId)
    {
        PostId = postId;
    }
}