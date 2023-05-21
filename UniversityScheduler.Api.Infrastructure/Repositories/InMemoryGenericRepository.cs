using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.RepositoryInterfaces;

namespace UniversityScheduler.Api.Infrastructure.Repositories;

[Registration(Type = RegistrationKind.Scoped)]
public class InMemoryGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : IEntity
{
    private static readonly List<TEntity> Entities = new();

    public void AddEntity(TEntity entity)
    {
        Entities.Add(entity);
    }

    public void AddListOfEntities(List<TEntity> entities)
    {
        Entities.AddRange(entities);
    }

    public void UpdateEntityById(int id, TEntity newEntity)
    {
        var oldEntity = Entities.First(entity => entity.Id == id);
        var index = Entities.IndexOf(oldEntity);
        Entities[index] = newEntity;
    }

    public void DeleteEntityById(int id)
    {
        Entities.Remove(Entities.First(entity => entity.Id == id));
    }

    public void DeleteAllEntities()
    {
        Entities.Clear();
    }

    public TEntity? GetEntityById(int id)
    {
        return Entities.FirstOrDefault(entity => entity.Id == id);
    }

    public List<TEntity> GetAllEntities()
    {
        return Entities;
    }
}