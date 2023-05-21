using UniversityScheduler.Api.Core.Models.University_Entities;

namespace UniversityScheduler.Api.Core.Services.ServiceInterfaces;

public interface IStudentService
{
    public Task<Student> GetStudentByIdAsync(int id);
    public Task<List<Student>> GetAllStudentsAsync();

    public Task QueueAddStudentAsync(Student student);
    public Task QueueAddStudentsAsync(List<Student> students);
    public Task QueueUpdateStudentByIdAsync(int id, Student student);
    public Task QueueDeleteStudentByIdAsync(int id);
    public void QueueDeleteAllStudents();

    public Task AddStudentAsync(Student student);
    public Task AddStudentsAsync(List<Student> students);
    public Task UpdateStudentByIdAsync(int id, Student student);
    public Task DeleteStudentByIdAsync(int id);
    public Task DeleteAllStudentsAsync();
}