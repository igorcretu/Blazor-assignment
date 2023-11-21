namespace Shared.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public User Owner { get; set; }
    public int OwnerId { get; set; }
    public Post Post { get; set; }
    public int PostId { get; set; }
    public DateTime Date { get; set; }
    
    public Comment(string content, int ownerId, int postId, DateTime date)
    {
        Content = content;
        OwnerId = ownerId;
        PostId = postId;
        Date = date;
    }
    private Comment() {}
}