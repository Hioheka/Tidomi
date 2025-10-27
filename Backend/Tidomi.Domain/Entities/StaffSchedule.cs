namespace Tidomi.Domain.Entities;

public class StaffSchedule
{
    public Guid Id { get; set; }
    public Guid StaffId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; } = true;

    // Navigation properties
    public Staff Staff { get; set; } = null!;
}
