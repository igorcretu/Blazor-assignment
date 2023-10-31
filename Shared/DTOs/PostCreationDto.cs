namespace Shared.DTOs;

public class PostCreationDto
{
    public int OwnerId { get; set; }
    public string OwnerUsername{ get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public PostCreationDto(int ownerId, string ownerUsername, string title, string body)
    {
        OwnerId = ownerId;
        OwnerUsername = ownerUsername;
        Title = title;
        Body = body;
    }
    
    public PostCreationDto(string ownerUsername, string title, string body)
    {
        OwnerUsername = ownerUsername;
        Title = title;
        Body = body;
    }
    
    public PostCreationDto() {}
}