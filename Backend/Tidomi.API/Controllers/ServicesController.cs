using Microsoft.AspNetCore.Mvc;
using Tidomi.Application.DTOs.Services;
using Tidomi.Application.Interfaces;

namespace Tidomi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet("categories")]
    public async Task<ActionResult<List<ServiceCategoryDto>>> GetAllCategories([FromQuery] string language = "en")
    {
        var result = await _serviceService.GetAllCategoriesWithServicesAsync(language);
        return Ok(result);
    }

    [HttpGet("category/{categoryId}")]
    public async Task<ActionResult<List<ServiceDto>>> GetServicesByCategory(Guid categoryId, [FromQuery] string language = "en")
    {
        var result = await _serviceService.GetServicesByCategoryAsync(categoryId, language);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> GetServiceById(Guid id, [FromQuery] string language = "en")
    {
        var result = await _serviceService.GetServiceByIdAsync(id, language);
        if (result == null)
            return NotFound(new { message = "Service not found" });

        return Ok(result);
    }
}
