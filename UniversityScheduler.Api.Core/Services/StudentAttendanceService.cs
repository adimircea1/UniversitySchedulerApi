using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Extensions;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Core.Services;

[Registration(Type = RegistrationKind.Scoped)]
public class StudentAttendanceService : IStudentAttendanceService
{
    private readonly ICourseService _courseService;
    private readonly IDatabaseGenericRepository<StudentAttendance> _studentAttendanceRepository;
    private readonly IStudentService _studentService;

    public StudentAttendanceService(IDatabaseGenericRepository<StudentAttendance> studentAttendanceRepository,
        IStudentService studentService, ICourseService courseService)
    {
        _studentAttendanceRepository = studentAttendanceRepository;
        _studentService = studentService;
        _courseService = courseService;
    }

    public async Task<StudentAttendance> GetStudentAttendanceByIdAsync(int id)
    {
        return await _studentAttendanceRepository.GetEntityByIdAsync(id) ??
               throw new Exception($"Couldn't find student attendance with id {id}");
    }

    public async Task<List<StudentAttendance>> GetAllStudentAttendancesAsync()
    {
        return await _studentAttendanceRepository.GetAllEntitiesAsync();
    }

    public async Task<List<StudentAttendance>> GetAttendancesOfStudentAsync(int studentId)
    {
        var student = await _studentService.GetStudentByIdAsync(studentId);

        var attendances = await GetAllStudentAttendancesAsync();

        var filteredAttendances = attendances.Where(attendance => attendance.StudentId == studentId)
            .ToList();

        return filteredAttendances;
    }

    public async Task<List<StudentAttendance>> GetCourseAttendancesAsync(int courseId)
    {
        var course = await _courseService.GetCourseByIdAsync(courseId);

        var attendances = await GetAllStudentAttendancesAsync();

        var filteredAttendances = attendances.Where(attendance => attendance.CourseId == courseId)
            .ToList();

        return filteredAttendances;
    }


    public async Task AddStudentAttendanceAsync(StudentAttendance studentAttendance)
    {
        await QueueAddStudentAttendanceAsync(studentAttendance);
        await _studentAttendanceRepository.SaveChangesAsync();
    }

    public async Task AddStudentAttendancesAsync(List<StudentAttendance> studentAttendances)
    {
        await QueueAddStudentAttendancesAsync(studentAttendances);
        await _studentAttendanceRepository.SaveChangesAsync();
    }

    public async Task UpdateStudentAttendanceByIdAsync(int id, StudentAttendance studentAttendance)
    {
        await QueueUpdateStudentAttendanceByIdAsync(id, studentAttendance);
        await _studentAttendanceRepository.SaveChangesAsync();
    }

    public async Task DeleteStudentAttendanceByIdAsync(int id)
    {
        await QueueDeleteStudentAttendanceByIdAsync(id);
        await _studentAttendanceRepository.SaveChangesAsync();
    }

    public async Task DeleteAllStudentAttendancesAsync()
    {
        QueueDeleteAllStudentAttendances();
        await _studentAttendanceRepository.SaveChangesAsync();
    }

    public async Task QueueAddStudentAttendanceAsync(StudentAttendance studentAttendance)
    {
        studentAttendance.Validate();
        await _studentAttendanceRepository.AddEntityAsync(studentAttendance);
    }

    public async Task QueueAddStudentAttendancesAsync(List<StudentAttendance> studentAttendances)
    {
        foreach (var studentAttendance in studentAttendances)
        {
            studentAttendance.Validate();
            await _studentAttendanceRepository.AddEntityAsync(studentAttendance);
        }
    }

    public async Task QueueUpdateStudentAttendanceByIdAsync(int id, StudentAttendance studentAttendance)
    {
        await _studentAttendanceRepository.UpdateEntityByIdAsync(id, studentAttendance);
    }

    public async Task QueueDeleteStudentAttendanceByIdAsync(int id)
    {
        await _studentAttendanceRepository.DeleteEntityByIdAsync(id);
    }

    public void QueueDeleteAllStudentAttendances()
    {
        _studentAttendanceRepository.DeleteAllEntities();
    }
}