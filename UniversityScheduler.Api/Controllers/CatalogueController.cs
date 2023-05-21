using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Core.Services.ServiceInterfaces;

namespace UniversityScheduler.Api.Controllers;

[ApiController]
[Route("catalogues")]
public class CatalogueController : ControllerBase
{
    private readonly ICatalogueService _catalogueService;

    public CatalogueController(ICatalogueService catalogueService)
    {
        _catalogueService = catalogueService;
    }

    [HttpPost("add-catalogues")]
    public async Task<ActionResult> AddCataloguesAsync([FromBody] List<Catalogue> catalogues)
    {
        try
        {
            await _catalogueService.AddCataloguesAsync(catalogues);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a list of catalogues!");
    }

    [HttpPost("add-catalogue")]
    public async Task<ActionResult> AddCatalogueAsync([FromBody] Catalogue catalogue)
    {
        try
        {
            await _catalogueService.AddCatalogueAsync(catalogue);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }

        return Ok("Successfully added a catalogue!");
    }

    [HttpGet("get-catalogue-by-id")]
    public async Task<ActionResult<Catalogue>> GetCatalogueByIdAsync([FromQuery] int id)
    {
        try
        {
            return await _catalogueService.GetCatalogueByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return NotFound();
        }
    }

    [HttpGet("get-all-catalogues")]
    public async Task<List<Catalogue>> GetAllCataloguesAsync()
    {
        return await _catalogueService.GetAllCataloguesAsync();
    }

    [HttpDelete("delete-catalogue-by-id")]
    public async Task<ActionResult> DeleteCatalogueByIdAsync([FromQuery] int id)
    {
        try
        {
            await _catalogueService.DeleteCatalogueByIdAsync(id);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine($"Successfully deleted a catalogue having the id {id}");
        return NoContent();
    }

    [HttpDelete("delete-all-catalogues")]
    public async Task<ActionResult> DeleteAllCataloguesAsync()
    {
        try
        {
            await _catalogueService.DeleteAllCataloguesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        Console.WriteLine("Successfully deleted all catalogues");
        return NoContent();
    }

    [HttpPut("update-catalogue-by-id")]
    public async Task<ActionResult> UpdateCatalogueByIdAsync([FromQuery] int id, [FromBody] Catalogue catalogue)
    {
        try
        {
            await _catalogueService.UpdateCatalogueByIdAsync(id, catalogue);
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

        return Ok($"Successfully updated catalogue with id {id}");
    }
}