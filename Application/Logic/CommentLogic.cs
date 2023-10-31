using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class CommentLogic : ICommentLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;
    private readonly ICommentDao commentDao;

    public CommentLogic(IPostDao postDao, IUserDao userDao, ICommentDao commentDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
        this.commentDao = commentDao;
    }
    
    public async Task<Comment> CreateCommentAsync(CommentCreationDto dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.OwnerUsername);
        if (user == null)
            throw new Exception($"User with username {dto.OwnerUsername} was not found.");
        
        Post? post = await postDao.GetByIdAsync(dto.PostId);
        if (post == null)
            throw new Exception($"Post with id {dto.PostId} was not found.");

        DateTime currentDate = DateTime.Now;
        
        Comment toCreate = new Comment(dto.Content, user, post, currentDate);
        Comment created = await commentDao.CreateCommentAsync(toCreate);
        return created;
    }

    public Task<IEnumerable<Comment>> GetAsync(SearchCommentParametersDto dto)
    {
        return commentDao.GetAsync(dto);
    }
}