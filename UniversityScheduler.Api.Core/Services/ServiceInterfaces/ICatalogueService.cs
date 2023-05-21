using UniversityScheduler.Api.Core.Models.University_Entities;

namespace UniversityScheduler.Api.Core.Services.ServiceInterfaces;

public interface ICatalogueService
{
    public Task<Catalogue> GetCatalogueByIdAsync(int id);
    public Task<List<Catalogue>> GetAllCataloguesAsync();


    public Task QueueAddCatalogueAsync(Catalogue catalogue);
    public Task QueueAddCataloguesAsync(List<Catalogue> catalogues);
    public Task QueueUpdateCatalogueByIdAsync(int id, Catalogue catalogue);
    public Task QueueDeleteCatalogueByIdAsync(int id);
    public void QueueDeleteAllCatalogues();

    public Task AddCatalogueAsync(Catalogue catalogue);
    public Task AddCataloguesAsync(List<Catalogue> catalogues);
    public Task UpdateCatalogueByIdAsync(int id, Catalogue catalogue);
    public Task DeleteCatalogueByIdAsync(int id);
    public Task DeleteAllCataloguesAsync();
}