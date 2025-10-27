using Tidomi.Domain.Entities;

namespace Tidomi.Application.DTOs.Bookings;

public class BookingResponseDto
{
    public Guid Id { get; set; }
    public string BookingNumber { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public TimeSpan ScheduledTime { get; set; }
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public BookingStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public List<BookingItemResponseDto> Items { get; set; } = new();
    public StaffInfoDto? AssignedStaff { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class BookingItemResponseDto
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}

public class StaffInfoDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public int CompletedJobs { get; set; }
}
