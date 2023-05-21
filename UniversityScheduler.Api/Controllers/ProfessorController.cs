using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Controllers;

[ApiController]
[Route("professors")]
public class ProfessorController : ControllerBase
{
    private readonly IProfessorService _professorService;

    public ProfessorController(IProfessorService professorService)
    {
        _professorService = professorService;
    }

    [HttpPost("add-professor")]
    public async Task<ActionResult> AddProfessorAsync([FromBody] Professor professor)
    {
        try
        {
            await _professorService.AddProfessorAsync(professor);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a professor!");
    }

    [HttpPost("add-professors")]
    public async Task<ActionResult> AddProfessorsAsync([FromBody] List<Professor> professors)
    {
        try
        {
            await _professorService.AddProfessorsAsync(professors);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a list of professors!");
    }

    [HttpGet("get-all-professors")]
    public async Task<List<Professor>> GetAllProfessorsAsync()
    {
        return await _professorService.GetAllProfessorsAsync();
    }

    [HttpGet("get-professor-by-id")]
    public async Task<ActionResult<Professor>> GetProfessorByIdAsync([FromQuery] int id)
    {
        try
        {
            return await _professorService.GetProfessorByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }
    }

    [HttpPut("update-professor-by-id")]
    public async Task<ActionResult> UpdateProfessorByIdAsync([FromQuery] int id, [FromBody] Professor professor)
    {
        try
        {
            await _professorService.UpdateProfessorByIdAsync(id, professor);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok($"Successfully updated professor with id {id}!");
    }

    [HttpDelete("delete-professor-by-id")]
    public async Task<ActionResult> DeleteProfessorById([FromQuery] int id)
    {
        try
        {
            await _professorService.DeleteProfessorByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine($"Successfully deleted a professor having the id {id}!");
        return NoContent();
    }

    [HttpDelete("delete-all-professors")]
    public async Task<ActionResult> DeleteAllProfessorsAsync()
    {
        try
        {
            await _professorService.DeleteAllProfessorsAsync();
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine("Successfully deleted all professors!");
        return NoContent();
    }
}