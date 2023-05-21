using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Controllers;

[ApiController]
[Route("grades")]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpPost("add-grade")]
    public async Task<ActionResult> AddGradeAsync([FromBody] Grade grade)
    {
        try
        {
            await _gradeService.AddGradeAsync(grade);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a grade!");
    }

    [HttpPost("add-grades")]
    public async Task<ActionResult> AddGradesAsync([FromBody] List<Grade> grades)
    {
        try
        {
            await _gradeService.AddGradesAsync(grades);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a list of grades!");
    }

    [HttpGet("get-all-grades")]
    public async Task<List<Grade>> GetAllGradesAsync()
    {
        return await _gradeService.GetAllGradesAsync();
    }

    [HttpGet("get-grade-by-id")]
    public async Task<ActionResult<Grade>> GetGradeByIdAsync([FromQuery] int id)
    {
        try
        {
            return await _gradeService.GetGradeByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }
    }

    [HttpPut("update-grade-by-id")]
    public async Task<ActionResult> UpdateGradeByIdAsync([FromQuery] int id, [FromBody] Grade grade)
    {
        try
        {
            await _gradeService.UpdateGradeByIdAsync(id, grade);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok($"Successfully updated grade with id {id}!");
    }

    [HttpDelete("delete-grade-by-id")]
    public async Task<ActionResult> DeleteGradeById([FromQuery] int id)
    {
        try
        {
            await _gradeService.DeleteGradeByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine($"Successfully deleted a grade having the id {id}!");
        return NoContent();
    }

    [HttpDelete("delete-all-grades")]
    public async Task<ActionResult> DeleteAllGradesAsync()
    {
        try
        {
            await _gradeService.DeleteAllGradesAsync();
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine("Successfully deleted all grades!");
        return NoContent();
    }

    [HttpGet("get-grades-from-catalogue")]
    public async Task<List<Grade>> GetAllGradesFromCatalogueAsync([FromQuery] int catalogueId)
    {
        return await _gradeService.GetAllGradesFromCatalogueAsync(catalogueId);
    }

    [HttpGet("get-grades-from-student")]
    public async Task<List<Grade>> GetAllGradesOfAStudentAsync([FromQuery] int studentId)
    {
        return await _gradeService.GetAllGradesFromCatalogueAsync(studentId);
    }
}