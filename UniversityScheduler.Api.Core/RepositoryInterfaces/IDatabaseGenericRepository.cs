namespace UniversityScheduler.Api.Core.RepositoryInterfaces;

public interface IDatabaseGenericRepository<TEntity>
{
    public Task AddEntityAsync(TEntity entity);
    public Task AddEntitiesAsync(List<TEntity> entities);
    public Task<TEntity?> GetEntityByIdAsync(int? id);
    public Task<List<TEntity>> GetAllEntitiesAsync();
    public Task DeleteEntityByIdAsync(int id);
    public void DeleteAllEntities();
    public Task UpdateEntityByIdAsync(int id, TEntity updatedEntity);
    public Task SaveChangesAsync();
}