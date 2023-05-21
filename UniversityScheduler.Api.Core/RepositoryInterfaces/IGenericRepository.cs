namespace UniversityScheduler.Api.Core.RepositoryInterfaces;

public interface IGenericRepository<TEntity>
{
    public void AddEntity(TEntity entity);
    public void AddListOfEntities(List<TEntity> entities);
    public void UpdateEntityById(int id, TEntity newEntity);
    public void DeleteEntityById(int id);
    public void DeleteAllEntities();
    public TEntity? GetEntityById(int id);
    public List<TEntity> GetAllEntities();
}