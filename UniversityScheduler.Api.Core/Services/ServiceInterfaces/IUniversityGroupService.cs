using UniversityScheduler.Api.Core.Models.University_Entities;

namespace UniversityScheduler.Api.Core.Services.ServiceInterfaces;

public interface IUniversityGroupService
{
    public Task<UniversityGroup> GetUniversityGroupByIdAsync(int id);
    public Task<List<UniversityGroup>> GetAllUniversityGroupsAsync();
    public Task<List<Student>> GetAllStudentsFromGroupAsync(int id);


    public Task QueueAddUniversityGroupAsync(UniversityGroup universityGroup);
    public Task QueueAddUniversityGroupsAsync(List<UniversityGroup> universityGroups);
    public Task QueueUpdateUniversityGroupByIdAsync(int id, UniversityGroup universityGroup);
    public Task QueueDeleteUniversityGroupByIdAsync(int id);
    public void QueueDeleteAllUniversityGroups();
    public Task QueueAddStudentInGroupAsync(int studentId, int groupId);

    public Task AddUniversityGroupAsync(UniversityGroup universityGroup);
    public Task AddUniversityGroupsAsync(List<UniversityGroup> universityGroups);
    public Task UpdateUniversityGroupByIdAsync(int id, UniversityGroup universityGroup);
    public Task DeleteUniversityGroupByIdAsync(int id);
    public Task DeleteAllUniversityGroupsAsync();
    public Task AddStudentInGroupAsync(int studentId, int groupId);
}