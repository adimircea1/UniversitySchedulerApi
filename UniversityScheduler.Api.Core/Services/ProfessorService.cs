using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Extensions;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Core.Services;

[Registration(Type = RegistrationKind.Scoped)]
public class ProfessorService : IProfessorService
{
    private readonly IDatabaseGenericRepository<Professor> _professorRepository;

    public ProfessorService(IDatabaseGenericRepository<Professor> professorRepository)
    {
        _professorRepository = professorRepository;
    }

    public async Task<Professor> GetProfessorByIdAsync(int id)
    {
        return await _professorRepository.GetEntityByIdAsync(id) ??
               throw new Exception($"Couldn't find professor with id {id}");
    }

    public async Task<List<Professor>> GetAllProfessorsAsync()
    {
        return await _professorRepository.GetAllEntitiesAsync();
    }

    public async Task AddProfessorAsync(Professor professor)
    {
        await QueueAddProfessorAsync(professor);
        await _professorRepository.SaveChangesAsync();
    }

    public async Task AddProfessorsAsync(List<Professor> professors)
    {
        await QueueAddProfessorsAsync(professors);
        await _professorRepository.SaveChangesAsync();
    }

    public async Task UpdateProfessorByIdAsync(int id, Professor professor)
    {
        await QueueUpdateProfessorByIdAsync(id, professor);
        await _professorRepository.SaveChangesAsync();
    }

    public async Task DeleteProfessorByIdAsync(int id)
    {
        await QueueDeleteProfessorByIdAsync(id);
        await _professorRepository.SaveChangesAsync();
    }

    public async Task DeleteAllProfessorsAsync()
    {
        QueueDeleteAllProfessors();
        await _professorRepository.SaveChangesAsync();
    }

    public async Task QueueAddProfessorAsync(Professor professor)
    {
        professor.Validate();
        await _professorRepository.AddEntityAsync(professor);
    }

    public async Task QueueAddProfessorsAsync(List<Professor> professors)
    {
        foreach (var professor in professors)
        {
            professor.Validate();
            await _professorRepository.AddEntityAsync(professor);
        }
    }

    public async Task QueueUpdateProfessorByIdAsync(int id, Professor professor)
    {
        await _professorRepository.UpdateEntityByIdAsync(id, professor);
    }

    public async Task QueueDeleteProfessorByIdAsync(int id)
    {
        await _professorRepository.DeleteEntityByIdAsync(id);
    }

    public void QueueDeleteAllProfessors()
    {
        _professorRepository.DeleteAllEntities();
    }
}