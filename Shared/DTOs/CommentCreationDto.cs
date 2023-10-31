namespace Shared.DTOs;

public class CommentCreationDto
{
    public string OwnerUsername { get; set; }
    public string Content { get; set; }
    public int OwnerId { get; set; }
    public int PostId { get; set; }
    
    public CommentCreationDto() {}
    
    public CommentCreationDto(string content, int ownerId, int postId, string ownerUsername)
    {
        Content = content;
        OwnerUsername = ownerUsername;
        OwnerId = ownerId;
        PostId = postId;
    }
    
    public CommentCreationDto(string content, string ownerUsername, int postId)
    {
        Content = content;
        OwnerUsername = ownerUsername;
        PostId = postId;
    }
}