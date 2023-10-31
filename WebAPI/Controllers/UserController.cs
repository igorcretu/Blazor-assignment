using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;
    
    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }

    [HttpPost]
    public async Task<ActionResult<User>> RegisterAsync(UserRegistrationDto dto)
    {
        try
        {
            User user = await userLogic.RegisterAsync(dto);
            return Created($"/users/{user.Username}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
    {
        try
        {
            SearchUserParametersDto parameters = new(username);
            IEnumerable<User> users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}