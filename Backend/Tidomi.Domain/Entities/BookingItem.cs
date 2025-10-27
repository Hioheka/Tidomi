namespace Tidomi.Domain.Entities;

public class BookingItem
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Guid ServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string Notes { get; set; } = string.Empty;

    // Navigation properties
    public Booking Booking { get; set; } = null!;
    public Service Service { get; set; } = null!;
}
