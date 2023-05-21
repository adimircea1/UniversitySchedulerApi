using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Extensions;
using UniversityScheduler.Api.Core.Models.Attributes;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.RepositoryInterfaces;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Core.Services;

[Registration(Type = RegistrationKind.Scoped)]
public class CatalogueService : ICatalogueService
{
    private readonly IDatabaseGenericRepository<Catalogue> _catalogueRepository;

    public CatalogueService(IDatabaseGenericRepository<Catalogue> catalogueRepository)
    {
        _catalogueRepository = catalogueRepository;
    }

    public async Task<Catalogue> GetCatalogueByIdAsync(int id)
    {
        return await _catalogueRepository.GetEntityByIdAsync(id) ??
               throw new Exception($"Couldn't find catalogue with id {id}");
    }

    public async Task<List<Catalogue>> GetAllCataloguesAsync()
    {
        return await _catalogueRepository.GetAllEntitiesAsync();
    }

    public async Task AddCatalogueAsync(Catalogue catalogue)
    {
        await QueueAddCatalogueAsync(catalogue);
        await _catalogueRepository.SaveChangesAsync();
    }

    public async Task AddCataloguesAsync(List<Catalogue> catalogues)
    {
        await QueueAddCataloguesAsync(catalogues);
        await _catalogueRepository.SaveChangesAsync();
    }

    public async Task UpdateCatalogueByIdAsync(int id, Catalogue catalogue)
    {
        await QueueUpdateCatalogueByIdAsync(id, catalogue);
        await _catalogueRepository.SaveChangesAsync();
    }

    public async Task DeleteCatalogueByIdAsync(int id)
    {
        await QueueDeleteCatalogueByIdAsync(id);
        await _catalogueRepository.SaveChangesAsync();
    }

    public async Task DeleteAllCataloguesAsync()
    {
        QueueDeleteAllCatalogues();
        await _catalogueRepository.SaveChangesAsync();
    }

    public async Task QueueAddCatalogueAsync(Catalogue catalogue)
    {
        catalogue.Validate();
        await _catalogueRepository.AddEntityAsync(catalogue);
    }

    public async Task QueueAddCataloguesAsync(List<Catalogue> catalogues)
    {
        foreach (var catalogue in catalogues)
        {
            catalogue.Validate();
            await _catalogueRepository.AddEntityAsync(catalogue);
        }
    }

    public async Task QueueUpdateCatalogueByIdAsync(int id, Catalogue catalogue)
    {
        await _catalogueRepository.UpdateEntityByIdAsync(id, catalogue);
    }

    public async Task QueueDeleteCatalogueByIdAsync(int id)
    {
        await _catalogueRepository.DeleteEntityByIdAsync(id);
    }

    public void QueueDeleteAllCatalogues()
    {
        _catalogueRepository.DeleteAllEntities();
    }
}