using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Controllers;

[ApiController]
[Route("courses")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost("add-course")]
    public async Task<ActionResult> AddCourseAsync([FromBody] Course course)
    {
        try
        {
            await _courseService.AddCourseAsync(course);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a course!");
    }

    [HttpPost("add-courses")]
    public async Task<ActionResult> AddCoursesAsync([FromBody] List<Course> courses)
    {
        try
        {
            await _courseService.AddCoursesAsync(courses);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a list of courses!");
    }

    [HttpGet("get-all-courses")]
    public async Task<List<Course>> GetAllCoursesAsync()
    {
        return await _courseService.GetAllCoursesAsync();
    }

    [HttpGet("get-course-by-id")]
    public async Task<ActionResult<Course>> GetCourseByIdAsync([FromQuery] int id)
    {
        try
        {
            return await _courseService.GetCourseByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }
    }

    [HttpPut("update-course-by-id")]
    public async Task<ActionResult> UpdateCourseByIdAsync([FromQuery] int id, [FromBody] Course course)
    {
        try
        {
            await _courseService.UpdateCourseByIdAsync(id, course);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok($"Successfully updated course with id {id}!");
    }

    [HttpDelete("delete-course-by-id")]
    public async Task<ActionResult> DeleteCourseById([FromQuery] int id)
    {
        try
        {
            await _courseService.DeleteCourseByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine($"Successfully deleted a course having the id {id}!");
        return NoContent();
    }

    [HttpDelete("delete-all-courses")]
    public async Task<ActionResult> DeleteAllCoursesAsync()
    {
        try
        {
            await _courseService.DeleteAllCoursesAsync();
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine("Successfully deleted all courses!");
        return NoContent();
    }
}