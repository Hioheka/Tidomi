namespace Tidomi.Application.DTOs.Bookings;

public class CreateBookingDto
{
    public List<BookingItemDto> Items { get; set; } = new();
    public DateTime ScheduledDate { get; set; }
    public TimeSpan ScheduledTime { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public Guid? PreferredStaffId { get; set; }
}

public class BookingItemDto
{
    public Guid ServiceId { get; set; }
    public int Quantity { get; set; }
    public string Notes { get; set; } = string.Empty;
}
