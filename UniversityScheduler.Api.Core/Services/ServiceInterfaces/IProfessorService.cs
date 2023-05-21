using UniversityScheduler.Api.Core.Models.University_Entities;

namespace UniversityScheduler.Api.Core.Services.ServiceInterfaces;

public interface IProfessorService
{
    public Task<Professor> GetProfessorByIdAsync(int id);
    public Task<List<Professor>> GetAllProfessorsAsync();

    public Task QueueAddProfessorAsync(Professor professor);
    public Task QueueAddProfessorsAsync(List<Professor> professors);
    public Task QueueUpdateProfessorByIdAsync(int id, Professor professor);
    public Task QueueDeleteProfessorByIdAsync(int id);
    public void QueueDeleteAllProfessors();

    public Task AddProfessorAsync(Professor professor);
    public Task AddProfessorsAsync(List<Professor> professors);
    public Task UpdateProfessorByIdAsync(int id, Professor professor);
    public Task DeleteProfessorByIdAsync(int id);
    public Task DeleteAllProfessorsAsync();
}