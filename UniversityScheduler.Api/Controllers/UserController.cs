using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniversityScheduler.Api.Core.Models;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;
using UniversityScheduler.Api.InputData;

namespace UniversityScheduler.Api.Controllers;

[ApiController] //used to serve http api responses
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("add-user")]
    public async Task<ActionResult> AddUserAsync([FromBody] User user)
    {
        try
        {
            await _userService.AddUserAsync(user);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a user!");
    }

    [HttpPost("add-users")]
    public async Task<ActionResult> AddUsersAsync([FromBody] List<User> users)
    {
        try
        {
            await _userService.AddUsersAsync(users);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a list of users!");
    }

    [HttpGet("get-all-users")]
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userService.GetAllUsersAsync();
    }

    [HttpGet("get-user-by-id")]
    public async Task<ActionResult<User>> GetUserByIdAsync([FromQuery] int id)
    {
        try
        {
            return await _userService.GetUserByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }
    }

    [HttpPut("update-user-by-id")]
    public async Task<ActionResult> UpdateUserByIdAsync([FromQuery] int id, [FromBody] User user)
    {
        try
        {
            await _userService.UpdateUserByIdAsync(id, user);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok($"Successfully updated user with id {id}!");
    }

    [HttpDelete("delete-user-by-id")]
    public async Task<ActionResult> DeleteUserById([FromQuery] int id)
    {
        try
        {
            await _userService.DeleteUserByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine($"Successfully deleted a user having the id {id}!");
        return NoContent();
    }

    [HttpDelete("delete-all-users")]
    public async Task<ActionResult> DeleteAllUsers()
    {
        try
        {
            await _userService.DeleteAllUsersAsync();
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine("Successfully deleted all users!");
        return NoContent();
    }

    [HttpPost("user-login")]
    public async Task<ActionResult<User>> UserLoginAsync([FromBody] LoginData data)
    {
        try
        {
            return await _userService.UserLoginAsync(data.Email, data.Password);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }
    }

    [HttpPost("confirm-email")]
    public async Task<ActionResult> ConfirmUserEmail(int userId)
    {
        try
        {
            await _userService.VerifyUserEmailAsync(userId);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok();
    }
    
    [HttpPut("update-user-by-email")]
    public async Task<ActionResult> UpdateUserByEmailAsync([FromQuery] string email, [FromBody] User user)
    {
        try
        {
            await _userService.UpdateUserByEmailAsync(email, user);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok($"Successfully updated user with email {email}!");
    }

}