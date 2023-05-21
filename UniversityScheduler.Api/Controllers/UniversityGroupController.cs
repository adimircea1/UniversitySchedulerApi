using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Controllers;

[ApiController]
[Route("groups")]
public class UniversityGroupController : ControllerBase
{
    private readonly IUniversityGroupService _universityGroupService;

    public UniversityGroupController(IUniversityGroupService universityGroupService)
    {
        _universityGroupService = universityGroupService;
    }

    [HttpPost("add-university-groups")]
    public async Task<ActionResult> AddUniversityGroupsAsync([FromBody] List<UniversityGroup> groups)
    {
        try
        {
            await _universityGroupService.AddUniversityGroupsAsync(groups);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a list of university groups!");
    }

    [HttpPost("add-university-group")]
    public async Task<ActionResult> AddUniversityGroupAsync([FromBody] UniversityGroup group)
    {
        try
        {
            await _universityGroupService.AddUniversityGroupAsync(group);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added an university group!");
    }

    [HttpGet("get-university-group-by-id")]
    public async Task<ActionResult<UniversityGroup>> GetUniversityGroupByIdAsync([FromQuery] int id)
    {
        try
        {
            return await _universityGroupService.GetUniversityGroupByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }
    }

    [HttpGet("get-all-university-groups")]
    public async Task<List<UniversityGroup>> GetAllUniversityGroupsAsync()
    {
        return await _universityGroupService.GetAllUniversityGroupsAsync();
    }

    [HttpDelete("delete-university-group-by-id")]
    public async Task<ActionResult> DeleteUniversityGroupByIdAsync([FromQuery] int id)
    {
        try
        {
            await _universityGroupService.DeleteUniversityGroupByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine($"Successfully deleted an university group having the id {id}");
        return NoContent();
    }

    [HttpDelete("delete-all-university-groups")]
    public async Task<ActionResult> DeleteAllUniversityGroupsAsync()
    {
        try
        {
            await _universityGroupService.DeleteAllUniversityGroupsAsync();
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine("Successfully deleted all university groups");
        return NoContent();
    }

    [HttpPut("update-university-group-by-id")]
    public async Task<ActionResult> UpdateUniversityGroupByIdAsync([FromQuery] int id, [FromBody] UniversityGroup group)
    {
        try
        {
            await _universityGroupService.UpdateUniversityGroupByIdAsync(id, group);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok($"Successfully updated a group having the id {id}");
    }

    [HttpPut("add-student-in-group")]
    public async Task<ActionResult> AddStudentInGroup([FromQuery] int studentId, [FromQuery] int groupId)
    {
        try
        {
            await _universityGroupService.AddStudentInGroupAsync(studentId, groupId);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok();
    }
}