using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Extensions;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Core.Services;

[Registration(Type = RegistrationKind.Scoped)]
public class GradeService : IGradeService
{
    private readonly ICatalogueService _catalogueService;
    private readonly IDatabaseGenericRepository<Grade> _gradeRepository;
    private readonly IStudentService _studentService;

    public GradeService(IDatabaseGenericRepository<Grade> gradeRepository, ICatalogueService catalogueService,
        IStudentService studentService)
    {
        _gradeRepository = gradeRepository;
        _catalogueService = catalogueService;
        _studentService = studentService;
    }

    public async Task<Grade> GetGradeByIdAsync(int id)
    {
        return await _gradeRepository.GetEntityByIdAsync(id) ??
               throw new Exception($"Couldn't find grade with id {id}");
    }

    public async Task<List<Grade>> GetAllGradesAsync()
    {
        return await _gradeRepository.GetAllEntitiesAsync();
    }

    public async Task<List<Grade>> GetAllGradesFromCatalogueAsync(int id)
    {
        var catalogue = await _catalogueService.GetCatalogueByIdAsync(id);
        var grades = await GetAllGradesAsync();
        var filteredGrades = grades.Where(grade => grade.CatalogueId == id).ToList();
        return filteredGrades;
    }

    public async Task<List<Grade>> GetAllGradesOfAStudentAsync(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        var grades = await GetAllGradesAsync();
        var filteredGrades = grades.Where(grade => grade.StudentId == id).ToList();
        return filteredGrades;
    }

    public async Task AddGradeAsync(Grade grade)
    {
        await QueueAddGradeAsync(grade);
        await _gradeRepository.SaveChangesAsync();
    }

    public async Task AddGradesAsync(List<Grade> grades)
    {
        await QueueAddGradesAsync(grades);
        await _gradeRepository.SaveChangesAsync();
    }

    public async Task UpdateGradeByIdAsync(int id, Grade grade)
    {
        await QueueUpdateGradeByIdAsync(id, grade);
        await _gradeRepository.SaveChangesAsync();
    }

    public async Task DeleteGradeByIdAsync(int id)
    {
        await QueueDeleteGradeByIdAsync(id);
        await _gradeRepository.SaveChangesAsync();
    }

    public async Task DeleteAllGradesAsync()
    {
        QueueDeleteAllGrades();
        await _gradeRepository.SaveChangesAsync();
    }

    public async Task QueueAddGradeAsync(Grade grade)
    {
        grade.Validate();
        await _gradeRepository.AddEntityAsync(grade);
    }

    public async Task QueueAddGradesAsync(List<Grade> grades)
    {
        foreach (var grade in grades)
        {
            grade.Validate();
            await _gradeRepository.AddEntityAsync(grade);
        }
    }

    public async Task QueueUpdateGradeByIdAsync(int id, Grade grade)
    {
        await _gradeRepository.UpdateEntityByIdAsync(id, grade);
    }

    public async Task QueueDeleteGradeByIdAsync(int id)
    {
        await _gradeRepository.DeleteEntityByIdAsync(id);
    }

    public void QueueDeleteAllGrades()
    {
        _gradeRepository.DeleteAllEntities();
    }
}