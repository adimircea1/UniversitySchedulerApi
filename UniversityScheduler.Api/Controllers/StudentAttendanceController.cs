using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Controllers;

[ApiController]
[Route("student-attendance")]
public class StudentAttendanceController : ControllerBase
{
    private readonly IStudentAttendanceService _studentAttendanceService;

    public StudentAttendanceController(IStudentAttendanceService studentAttendanceService)
    {
        _studentAttendanceService = studentAttendanceService;
    }

    [HttpPost("add-attendance")]
    public async Task<ActionResult> AddAttendanceAsync([FromBody] StudentAttendance attendance)
    {
        try
        {
            await _studentAttendanceService.AddStudentAttendanceAsync(attendance);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added student attendance!");
    }

    [HttpPost("add-multiple-attendance")]
    public async Task<ActionResult> AddMultipleAttendanceAsync([FromBody] List<StudentAttendance> attendanceList)
    {
        try
        {
            await _studentAttendanceService.AddStudentAttendancesAsync(attendanceList);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added multiple student attendance records!");
    }

    [HttpGet("get-attendance-by-id")]
    public async Task<ActionResult<StudentAttendance>> GetAttendanceByIdAsync([FromQuery] int id)
    {
        try
        {
            return await _studentAttendanceService.GetStudentAttendanceByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }
    }

    [HttpGet("get-all-attendance")]
    public async Task<List<StudentAttendance>> GetAllAttendanceAsync()
    {
        return await _studentAttendanceService.GetAllStudentAttendancesAsync();
    }

    [HttpPut("update-attendance-by-id")]
    public async Task<ActionResult> UpdateAttendanceByIdAsync([FromQuery] int id,
        [FromBody] StudentAttendance attendance)
    {
        try
        {
            await _studentAttendanceService.UpdateStudentAttendanceByIdAsync(id, attendance);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok($"Successfully updated attendance record with id {id}!");
    }

    [HttpDelete("delete-attendance-by-id")]
    public async Task<ActionResult> DeleteAttendanceById([FromQuery] int id)
    {
        try
        {
            await _studentAttendanceService.DeleteStudentAttendanceByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine($"Successfully deleted attendance record with id {id}!");
        return NoContent();
    }

    [HttpDelete("delete-all-attendance")]
    public async Task<ActionResult> DeleteAllAttendanceAsync()
    {
        try
        {
            await _studentAttendanceService.DeleteAllStudentAttendancesAsync();
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine("Successfully deleted all attendance records!");
        return NoContent();
    }

    [HttpGet("get-course-attendances")]
    public async Task<List<StudentAttendance>> GetCourseAttendancesAsync([FromQuery] int id)
    {
        return await _studentAttendanceService.GetCourseAttendancesAsync(id);
    }

    [HttpGet("get-attendances-of-student")]
    public async Task<List<StudentAttendance>> GetAttendancesOfStudentAsync([FromQuery] int id)
    {
        return await _studentAttendanceService.GetAttendancesOfStudentAsync(id);
    }
}