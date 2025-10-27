using Microsoft.EntityFrameworkCore;
using Tidomi.Application.DTOs.Services;
using Tidomi.Application.Interfaces;
using Tidomi.Infrastructure.Data;

namespace Tidomi.Infrastructure.Services;

public class ServiceService : IServiceService
{
    private readonly TidomiDbContext _context;

    public ServiceService(TidomiDbContext context)
    {
        _context = context;
    }

    public async Task<List<ServiceCategoryDto>> GetAllCategoriesWithServicesAsync(string language)
    {
        var categories = await _context.ServiceCategories
            .Include(c => c.Services.Where(s => s.IsActive))
            .Where(c => c.IsActive)
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync();

        return categories.Select(c => new ServiceCategoryDto
        {
            Id = c.Id,
            Name = language.ToLower() == "tr" ? c.NameTr : c.NameEn,
            Description = language.ToLower() == "tr" ? c.DescriptionTr : c.DescriptionEn,
            IconUrl = c.IconUrl,
            Services = c.Services.OrderBy(s => s.DisplayOrder).Select(s => new ServiceDto
            {
                Id = s.Id,
                CategoryId = s.CategoryId,
                CategoryName = language.ToLower() == "tr" ? c.NameTr : c.NameEn,
                Name = language.ToLower() == "tr" ? s.NameTr : s.NameEn,
                Description = language.ToLower() == "tr" ? s.DescriptionTr : s.DescriptionEn,
                Type = s.Type,
                Price = s.Price,
                Unit = s.Unit,
                DurationMinutes = s.DurationMinutes,
                ImageUrl = s.ImageUrl
            }).ToList()
        }).ToList();
    }

    public async Task<List<ServiceDto>> GetServicesByCategoryAsync(Guid categoryId, string language)
    {
        var services = await _context.Services
            .Include(s => s.Category)
            .Where(s => s.CategoryId == categoryId && s.IsActive)
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();

        return services.Select(s => new ServiceDto
        {
            Id = s.Id,
            CategoryId = s.CategoryId,
            CategoryName = language.ToLower() == "tr" ? s.Category.NameTr : s.Category.NameEn,
            Name = language.ToLower() == "tr" ? s.NameTr : s.NameEn,
            Description = language.ToLower() == "tr" ? s.DescriptionTr : s.DescriptionEn,
            Type = s.Type,
            Price = s.Price,
            Unit = s.Unit,
            DurationMinutes = s.DurationMinutes,
            ImageUrl = s.ImageUrl
        }).ToList();
    }

    public async Task<ServiceDto?> GetServiceByIdAsync(Guid id, string language)
    {
        var service = await _context.Services
            .Include(s => s.Category)
            .FirstOrDefaultAsync(s => s.Id == id && s.IsActive);

        if (service == null) return null;

        return new ServiceDto
        {
            Id = service.Id,
            CategoryId = service.CategoryId,
            CategoryName = language.ToLower() == "tr" ? service.Category.NameTr : service.Category.NameEn,
            Name = language.ToLower() == "tr" ? service.NameTr : service.NameEn,
            Description = language.ToLower() == "tr" ? service.DescriptionTr : service.DescriptionEn,
            Type = service.Type,
            Price = service.Price,
            Unit = service.Unit,
            DurationMinutes = service.DurationMinutes,
            ImageUrl = service.ImageUrl
        };
    }
}
