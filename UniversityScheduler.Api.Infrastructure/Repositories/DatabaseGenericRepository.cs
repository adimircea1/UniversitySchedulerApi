using Microsoft.EntityFrameworkCore;
using UniversityScheduler.Api.Core.Extensions;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.RepositoryInterfaces;

namespace UniversityScheduler.Api.Infrastructure.Repositories;

public class DatabaseGenericRepository<TEntity> : IDatabaseGenericRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DataContext _context;
    private readonly DbSet<TEntity> _entitySet;

    public DatabaseGenericRepository(DataContext context)
    {
        _context = context;
        _entitySet = _context.Set<TEntity>();
    }

    public async Task AddEntityAsync(TEntity entity)
    {
        await _entitySet.AddAsync(entity);
    }

    public async Task AddEntitiesAsync(List<TEntity> entities)
    {
        await _entitySet.AddRangeAsync(entities);
    }

    public async Task<TEntity?> GetEntityByIdAsync(int? id)
    {
        return await _entitySet.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<List<TEntity>> GetAllEntitiesAsync()
    {
        return await _entitySet.ToListAsync();
    }

    public async Task DeleteEntityByIdAsync(int id)
    {
        var entity = await _entitySet.FindAsync(id);

        if (entity == null) throw new Exception("Cannot delete a non-existent entity!");

        _entitySet.Remove(entity);
    }

    public void DeleteAllEntities()
    {
        _entitySet.RemoveRange(_entitySet);
    }

    public async Task UpdateEntityByIdAsync(int id, TEntity updatedEntity)
    {
        var outdatedEntity = await _entitySet.FindAsync(id);

        if (outdatedEntity == null) throw new Exception("Cannot update a non-existent entity!");

        outdatedEntity.UpdateByReflection(updatedEntity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}