using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Controllers;

[ApiController] //used to serve http api responses
[Route("students")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost("add-student")]
    public async Task<ActionResult> AddStudentAsync([FromBody] Student student)
    {
        try
        {
            await _studentService.AddStudentAsync(student);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a student!");
    }

    [HttpPost("add-students")]
    public async Task<ActionResult> AddStudentsAsync([FromBody] List<Student> students)
    {
        try
        {
            await _studentService.AddStudentsAsync(students);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a list of students!");
    }

    [HttpGet("get-all-students")]
    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _studentService.GetAllStudentsAsync();
    }

    [HttpGet("get-student-by-id")]
    public async Task<ActionResult<Student>> GetStudentByIdAsync([FromQuery] int id)
    {
        try
        {
            return await _studentService.GetStudentByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }
    }

    [HttpPut("update-student-by-id")]
    public async Task<ActionResult> UpdateStudentByIdAsync([FromQuery] int id, [FromBody] Student student)
    {
        try
        {
            await _studentService.UpdateStudentByIdAsync(id, student);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok($"Successfully updated student with id {id}!");
    }

    [HttpDelete("delete-student-by-id")]
    public async Task<ActionResult> DeleteStudentById([FromQuery] int id)
    {
        try
        {
            await _studentService.DeleteStudentByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine($"Successfully deleted a student having the id {id}!");
        return NoContent();
    }

    [HttpDelete("delete-all-students")]
    public async Task<ActionResult> DeleteAllStudents()
    {
        try
        {
            await _studentService.DeleteAllStudentsAsync();
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine("Successfully deleted all students!");
        return NoContent();
    }
}