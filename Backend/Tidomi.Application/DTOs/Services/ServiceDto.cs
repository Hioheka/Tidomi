using Tidomi.Domain.Entities;

namespace Tidomi.Application.DTOs.Services;

public class ServiceDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ServiceType Type { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}

public class ServiceCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public List<ServiceDto> Services { get; set; } = new();
}
