using UniversityScheduler.Api.Core.Models.University_Entities;

namespace UniversityScheduler.Api.Core.Services.ServiceInterfaces;

public interface IStudentAttendanceService
{
    public Task<StudentAttendance> GetStudentAttendanceByIdAsync(int id);
    public Task<List<StudentAttendance>> GetAllStudentAttendancesAsync();
    public Task<List<StudentAttendance>> GetAttendancesOfStudentAsync(int studentId);
    public Task<List<StudentAttendance>> GetCourseAttendancesAsync(int courseId);


    public Task QueueAddStudentAttendanceAsync(StudentAttendance studentAttendance);
    public Task QueueAddStudentAttendancesAsync(List<StudentAttendance> studentAttendances);
    public Task QueueUpdateStudentAttendanceByIdAsync(int id, StudentAttendance studentAttendance);
    public Task QueueDeleteStudentAttendanceByIdAsync(int id);
    public void QueueDeleteAllStudentAttendances();

    public Task AddStudentAttendanceAsync(StudentAttendance studentAttendance);
    public Task AddStudentAttendancesAsync(List<StudentAttendance> studentAttendances);
    public Task UpdateStudentAttendanceByIdAsync(int id, StudentAttendance studentAttendance);
    public Task DeleteStudentAttendanceByIdAsync(int id);
    public Task DeleteAllStudentAttendancesAsync();
}