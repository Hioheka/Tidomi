using Tidomi.Application.DTOs.Services;

namespace Tidomi.Application.Interfaces;

public interface IServiceService
{
    Task<List<ServiceCategoryDto>> GetAllCategoriesWithServicesAsync(string language);
    Task<List<ServiceDto>> GetServicesByCategoryAsync(Guid categoryId, string language);
    Task<ServiceDto?> GetServiceByIdAsync(Guid id, string language);
}
