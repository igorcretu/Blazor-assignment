using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic postLogic;

    public PostController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Post>> CreatePostAsync(PostCreationDto dto)
    {
        try
        {
            Post post = await postLogic.CreatePostAsync(dto);
            return Created($"/posts/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] int? userId, string? username,
        string? email, string? titleContains)
    {
        try
        {
            SearchPostParametersDto parameters = new(userId, username, email, titleContains);
            var posts = await postLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostBasicDto>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            PostBasicDto post = await postLogic.GetByIdAsync(id);
            return Ok(post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await postLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDto dto)
    {
        try
        {
            await postLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}