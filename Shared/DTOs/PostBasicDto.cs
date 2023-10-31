namespace Shared.DTOs;

public class PostBasicDto
{
    public int Id { get; set; }
    public string OwnerUsername { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public PostBasicDto(int id, string ownerUsername, string title, string body)
    {
        Id = id;
        OwnerUsername = ownerUsername;
        Title = title;
        Body = body;
    }
}