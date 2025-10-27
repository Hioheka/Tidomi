namespace Tidomi.Domain.Entities;

public class Service
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string NameTr { get; set; } = string.Empty;
    public string DescriptionEn { get; set; } = string.Empty;
    public string DescriptionTr { get; set; } = string.Empty;
    public ServiceType Type { get; set; }
    public decimal Price { get; set; }
    public string Unit { get; set; } = string.Empty; // hour, session, sqm, etc.
    public int DurationMinutes { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public ServiceCategory Category { get; set; } = null!;
    public ICollection<BookingItem> BookingItems { get; set; } = new List<BookingItem>();
}

public enum ServiceType
{
    Hourly = 0,
    Package = 1,
    PerArea = 2
}
