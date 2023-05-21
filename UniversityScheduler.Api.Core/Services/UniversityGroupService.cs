using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Extensions;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Core.Services;

[Registration(Type = RegistrationKind.Scoped)]
public class UniversityGroupService : IUniversityGroupService
{
    private readonly IStudentService _studentService;
    private readonly IDatabaseGenericRepository<UniversityGroup> _universityGroupRepository;

    public UniversityGroupService(IDatabaseGenericRepository<UniversityGroup> universityGroupRepository,
        IStudentService studentService)
    {
        _universityGroupRepository = universityGroupRepository;
        _studentService = studentService;
    }

    public async Task<UniversityGroup> GetUniversityGroupByIdAsync(int id)
    {
        return await _universityGroupRepository.GetEntityByIdAsync(id) ??
               throw new Exception($"Couldn't find university group with id {id}");
    }

    public async Task<List<UniversityGroup>> GetAllUniversityGroupsAsync()
    {
        return await _universityGroupRepository.GetAllEntitiesAsync();
    }

    public async Task<List<Student>> GetAllStudentsFromGroupAsync(int id)
    {
        var catalogue = await GetUniversityGroupByIdAsync(id);
        var students = await _studentService.GetAllStudentsAsync();
        var filteredStudents = students.Where(student => student.UniversityGroupId == id).ToList();
        return filteredStudents;
    }

    public async Task AddUniversityGroupAsync(UniversityGroup universityGroup)
    {
        await QueueAddUniversityGroupAsync(universityGroup);
        await _universityGroupRepository.SaveChangesAsync();
    }

    public async Task AddUniversityGroupsAsync(List<UniversityGroup> universityGroups)
    {
        await QueueAddUniversityGroupsAsync(universityGroups);
        await _universityGroupRepository.SaveChangesAsync();
    }

    public async Task UpdateUniversityGroupByIdAsync(int id, UniversityGroup universityGroup)
    {
        await QueueUpdateUniversityGroupByIdAsync(id, universityGroup);
        await _universityGroupRepository.SaveChangesAsync();
    }

    public async Task DeleteUniversityGroupByIdAsync(int id)
    {
        await QueueDeleteUniversityGroupByIdAsync(id);
        await _universityGroupRepository.SaveChangesAsync();
    }

    public async Task DeleteAllUniversityGroupsAsync()
    {
        QueueDeleteAllUniversityGroups();
        await _universityGroupRepository.SaveChangesAsync();
    }

    public async Task AddStudentInGroupAsync(int studentId, int groupId)
    {
        await QueueAddStudentInGroupAsync(studentId, groupId);
        await _universityGroupRepository.SaveChangesAsync();
    }

    public async Task QueueAddUniversityGroupAsync(UniversityGroup universityGroup)
    {
        universityGroup.Validate();
        await _universityGroupRepository.AddEntityAsync(universityGroup);
    }

    public async Task QueueAddUniversityGroupsAsync(List<UniversityGroup> universityGroups)
    {
        foreach (var universityGroup in universityGroups)
        {
            universityGroup.Validate();
            await _universityGroupRepository.AddEntityAsync(universityGroup);
        }
    }

    public async Task QueueUpdateUniversityGroupByIdAsync(int id, UniversityGroup universityGroup)
    {
        await _universityGroupRepository.UpdateEntityByIdAsync(id, universityGroup);
    }

    public async Task QueueDeleteUniversityGroupByIdAsync(int id)
    {
        await _universityGroupRepository.DeleteEntityByIdAsync(id);
    }

    public void QueueDeleteAllUniversityGroups()
    {
        _universityGroupRepository.DeleteAllEntities();
    }

    public async Task QueueAddStudentInGroupAsync(int studentId, int groupId)
    {
        var student = await _studentService.GetStudentByIdAsync(studentId);
        var group = await GetUniversityGroupByIdAsync(groupId);

        student.UniversityGroupId = groupId;
        group.NumberOfMembers += 1;

        await _studentService.QueueUpdateStudentByIdAsync(studentId, student);
    }
}