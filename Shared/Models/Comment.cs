namespace Shared.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public User Owner { get; set; }
    public Post Post { get; set; }
    public DateTime Date { get; set; }
    
    public Comment(string content, User owner, Post post, DateTime date)
    {
        Content = content;
        Owner = owner;
        Post = post;
        Date = date;
    }
}