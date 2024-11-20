using Microsoft.AspNetCore.Mvc;
using Server.DataAccess.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class BuildingController : ControllerBase
{
    private readonly IBuildingRepository _buildingRepository;
    private readonly IApartmentRepository _apartmentRepository;

    public BuildingController(IBuildingRepository buildingRepository, IApartmentRepository apartmentRepository)
    {
        _buildingRepository = buildingRepository;
        _apartmentRepository = apartmentRepository;
    }

    [HttpGet("search-buildings")]
    public async Task<IActionResult> SearchBuildings([FromQuery] string? query = null)
    {
        var buildings = await _buildingRepository.SearchBuildingsAsync(query ?? string.Empty);
        return Ok(buildings);
    }

    // Search endpoint for apartments within a building
    [HttpGet("search-apartments")]
    public async Task<IActionResult> SearchApartments([FromQuery] long buildingId, [FromQuery] string? query = null)
    {
        if (buildingId <= 0)
            return BadRequest("Invalid building ID.");

        var apartments = await _apartmentRepository.SearchApartmentsAsync(buildingId, query ?? string.Empty);
        return Ok(apartments);
    }
}