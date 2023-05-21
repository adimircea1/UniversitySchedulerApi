using UniversityScheduler.Api.Core.Models.University_Entities;

namespace UniversityScheduler.Api.Core.Services.ServiceInterfaces;

public interface ICourseService
{
    public Task<Course> GetCourseByIdAsync(int id);
    public Task<List<Course>> GetAllCoursesAsync();

    public Task QueueAddCourseAsync(Course course);
    public Task QueueAddCoursesAsync(List<Course> courses);
    public Task QueueUpdateCourseByIdAsync(int id, Course course);
    public Task QueueDeleteCourseByIdAsync(int id);
    public void QueueDeleteAllCourses();

    public Task AddCourseAsync(Course course);
    public Task AddCoursesAsync(List<Course> courses);
    public Task UpdateCourseByIdAsync(int id, Course course);
    public Task DeleteCourseByIdAsync(int id);
    public Task DeleteAllCoursesAsync();
}