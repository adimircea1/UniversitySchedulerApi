using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Extensions;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Core.Services;

[Registration(Type = RegistrationKind.Scoped)]
public class CourseService : ICourseService
{
    private readonly IDatabaseGenericRepository<Course> _courseRepository;

    public CourseService(IDatabaseGenericRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        return await _courseRepository.GetEntityByIdAsync(id) ??
               throw new Exception($"Couldn't find course with id {id}");
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        return await _courseRepository.GetAllEntitiesAsync();
    }

    public async Task AddCourseAsync(Course course)
    {
        await QueueAddCourseAsync(course);
        await _courseRepository.SaveChangesAsync();
    }

    public async Task AddCoursesAsync(List<Course> courses)
    {
        await QueueAddCoursesAsync(courses);
        await _courseRepository.SaveChangesAsync();
    }

    public async Task UpdateCourseByIdAsync(int id, Course course)
    {
        await QueueUpdateCourseByIdAsync(id, course);
        await _courseRepository.SaveChangesAsync();
    }

    public async Task DeleteCourseByIdAsync(int id)
    {
        await QueueDeleteCourseByIdAsync(id);
        await _courseRepository.SaveChangesAsync();
    }

    public async Task DeleteAllCoursesAsync()
    {
        QueueDeleteAllCourses();
        await _courseRepository.SaveChangesAsync();
    }

    public async Task QueueAddCourseAsync(Course course)
    {
        course.Validate();
        await _courseRepository.AddEntityAsync(course);
    }

    public async Task QueueAddCoursesAsync(List<Course> courses)
    {
        foreach (var course in courses)
        {
            course.Validate();
            await _courseRepository.AddEntityAsync(course);
        }
    }

    public async Task QueueUpdateCourseByIdAsync(int id, Course course)
    {
        await _courseRepository.UpdateEntityByIdAsync(id, course);
    }

    public async Task QueueDeleteCourseByIdAsync(int id)
    {
        await _courseRepository.DeleteEntityByIdAsync(id);
    }

    public void QueueDeleteAllCourses()
    {
        _courseRepository.DeleteAllEntities();
    }
}