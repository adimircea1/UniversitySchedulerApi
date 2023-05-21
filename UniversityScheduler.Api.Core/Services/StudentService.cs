using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Extensions;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Core.Services;

[Registration(Type = RegistrationKind.Scoped)]
public class StudentService : IStudentService
{
    private readonly IDatabaseGenericRepository<Student> _studentRepository;


    public StudentService(IDatabaseGenericRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _studentRepository.GetEntityByIdAsync(id) ??
               throw new Exception($"Couldn't find student with id {id}");
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllEntitiesAsync();
    }

    public async Task AddStudentAsync(Student student)
    {
        await QueueAddStudentAsync(student);
        await _studentRepository.SaveChangesAsync();
    }

    public async Task AddStudentsAsync(List<Student> students)
    {
        await QueueAddStudentsAsync(students);
        await _studentRepository.SaveChangesAsync();
    }

    public async Task UpdateStudentByIdAsync(int id, Student student)
    {
        await QueueUpdateStudentByIdAsync(id, student);
        await _studentRepository.SaveChangesAsync();
    }

    public async Task DeleteStudentByIdAsync(int id)
    {
        await QueueDeleteStudentByIdAsync(id);
        await _studentRepository.SaveChangesAsync();
    }

    public async Task DeleteAllStudentsAsync()
    {
        QueueDeleteAllStudents();
        await _studentRepository.SaveChangesAsync();
    }

    public async Task QueueAddStudentAsync(Student student)
    {
        student.Validate();
        await _studentRepository.AddEntityAsync(student);
    }

    public async Task QueueAddStudentsAsync(List<Student> students)
    {
        foreach (var student in students)
        {
            student.Validate();
            await _studentRepository.AddEntityAsync(student);
        }
    }

    public async Task QueueUpdateStudentByIdAsync(int id, Student student)
    {
        await _studentRepository.UpdateEntityByIdAsync(id, student);
    }

    public async Task QueueDeleteStudentByIdAsync(int id)
    {
        await _studentRepository.DeleteEntityByIdAsync(id);
    }

    public void QueueDeleteAllStudents()
    {
        _studentRepository.DeleteAllEntities();
    }
}