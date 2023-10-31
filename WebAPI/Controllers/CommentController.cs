using Application.LogicInterfaces;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;
using Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentLogic commentLogic;

    public CommentController(ICommentLogic commentLogic)
    {
        this.commentLogic = commentLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> CreateCommentAsync(CommentCreationDto dto)
    {
        try
        {
            Comment comment = await commentLogic.CreateCommentAsync(dto);
            return Created($"/comments/{comment.Id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAsync([FromQuery]int? userId, [FromQuery]string? username, [FromQuery]int? postId,
        [FromQuery]DateTime? date)
    {
        try
        {
            SearchCommentParametersDto parameters = new(userId, username, postId, date);
            var comments = await commentLogic.GetAsync(parameters);
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}