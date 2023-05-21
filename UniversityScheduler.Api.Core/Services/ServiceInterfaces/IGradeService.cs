using UniversityScheduler.Api.Core.Models.University_Entities;

namespace UniversityScheduler.Api.Core.Services.ServiceInterfaces;

public interface IGradeService
{
    public Task<Grade> GetGradeByIdAsync(int id);
    public Task<List<Grade>> GetAllGradesAsync();
    public Task<List<Grade>> GetAllGradesFromCatalogueAsync(int id);
    public Task<List<Grade>> GetAllGradesOfAStudentAsync(int id);


    public Task QueueAddGradeAsync(Grade grade);
    public Task QueueAddGradesAsync(List<Grade> grades);
    public Task QueueUpdateGradeByIdAsync(int id, Grade grade);
    public Task QueueDeleteGradeByIdAsync(int id);
    public void QueueDeleteAllGrades();

    public Task AddGradeAsync(Grade grade);
    public Task AddGradesAsync(List<Grade> grades);
    public Task UpdateGradeByIdAsync(int id, Grade grade);
    public Task DeleteGradeByIdAsync(int id);
    public Task DeleteAllGradesAsync();
}