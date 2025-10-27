namespace Tidomi.Domain.Entities;

public class Staff
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Bio { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public int CompletedJobs { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<StaffSchedule> Schedules { get; set; } = new List<StaffSchedule>();
}
