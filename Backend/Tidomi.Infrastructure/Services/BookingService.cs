using Microsoft.EntityFrameworkCore;
using Tidomi.Application.DTOs.Bookings;
using Tidomi.Application.Interfaces;
using Tidomi.Domain.Entities;
using Tidomi.Infrastructure.Data;

namespace Tidomi.Infrastructure.Services;

public class BookingService : IBookingService
{
    private readonly TidomiDbContext _context;

    public BookingService(TidomiDbContext context)
    {
        _context = context;
    }

    public async Task<BookingResponseDto> CreateBookingAsync(Guid customerId, CreateBookingDto dto)
    {
        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            BookingNumber = GenerateBookingNumber(),
            CustomerId = customerId,
            ScheduledDate = dto.ScheduledDate,
            ScheduledTime = dto.ScheduledTime,
            Address = dto.Address,
            City = dto.City,
            District = dto.District,
            PostalCode = dto.PostalCode,
            Notes = dto.Notes,
            Status = BookingStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        decimal totalAmount = 0;
        foreach (var item in dto.Items)
        {
            var service = await _context.Services.FindAsync(item.ServiceId);
            if (service == null) continue;

            var bookingItem = new BookingItem
            {
                Id = Guid.NewGuid(),
                BookingId = booking.Id,
                ServiceId = item.ServiceId,
                Quantity = item.Quantity,
                UnitPrice = service.Price,
                TotalPrice = service.Price * item.Quantity,
                Notes = item.Notes
            };

            totalAmount += bookingItem.TotalPrice;
            booking.BookingItems.Add(bookingItem);
        }

        booking.TotalAmount = totalAmount;

        // Assign preferred staff if available
        if (dto.PreferredStaffId.HasValue)
        {
            var staff = await _context.Staff.FindAsync(dto.PreferredStaffId.Value);
            if (staff != null && staff.IsAvailable)
            {
                booking.AssignedStaffId = dto.PreferredStaffId.Value;
                booking.Status = BookingStatus.Assigned;
            }
        }

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return await GetBookingByIdAsync(booking.Id, customerId) ?? throw new Exception("Booking creation failed");
    }

    public async Task<BookingResponseDto?> GetBookingByIdAsync(Guid id, Guid userId)
    {
        var booking = await _context.Bookings
            .Include(b => b.BookingItems).ThenInclude(bi => bi.Service)
            .Include(b => b.AssignedStaff).ThenInclude(s => s!.User)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null) return null;

        return MapToResponseDto(booking);
    }

    public async Task<List<BookingResponseDto>> GetUserBookingsAsync(Guid userId)
    {
        var bookings = await _context.Bookings
            .Include(b => b.BookingItems).ThenInclude(bi => bi.Service)
            .Include(b => b.AssignedStaff).ThenInclude(s => s!.User)
            .Where(b => b.CustomerId == userId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return bookings.Select(MapToResponseDto).ToList();
    }

    public async Task<List<BookingResponseDto>> GetAllBookingsAsync()
    {
        var bookings = await _context.Bookings
            .Include(b => b.BookingItems).ThenInclude(bi => bi.Service)
            .Include(b => b.AssignedStaff).ThenInclude(s => s!.User)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return bookings.Select(MapToResponseDto).ToList();
    }

    public async Task<bool> AssignStaffToBookingAsync(Guid bookingId, Guid staffId)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);
        var staff = await _context.Staff.FindAsync(staffId);

        if (booking == null || staff == null || !staff.IsAvailable)
            return false;

        booking.AssignedStaffId = staffId;
        booking.Status = BookingStatus.Assigned;
        booking.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateBookingStatusAsync(Guid bookingId, int status)
    {
        var booking = await _context.Bookings.FindAsync(bookingId);
        if (booking == null) return false;

        booking.Status = (BookingStatus)status;
        booking.UpdatedAt = DateTime.UtcNow;

        if (status == (int)BookingStatus.Completed)
            booking.CompletedAt = DateTime.UtcNow;
        else if (status == (int)BookingStatus.Cancelled)
            booking.CancelledAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<DateTime>> GetAvailableDatesAsync(Guid? staffId)
    {
        var availableDates = new List<DateTime>();
        var startDate = DateTime.Today.AddDays(1);
        var endDate = startDate.AddDays(30);

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Sunday)
            {
                var bookingsCount = await _context.Bookings
                    .Where(b => b.ScheduledDate.Date == date.Date &&
                               (!staffId.HasValue || b.AssignedStaffId == staffId.Value))
                    .CountAsync();

                if (bookingsCount < 10) // Max 10 bookings per day
                    availableDates.Add(date);
            }
        }

        return availableDates;
    }

    private BookingResponseDto MapToResponseDto(Booking booking)
    {
        return new BookingResponseDto
        {
            Id = booking.Id,
            BookingNumber = booking.BookingNumber,
            ScheduledDate = booking.ScheduledDate,
            ScheduledTime = booking.ScheduledTime,
            Address = booking.Address,
            City = booking.City,
            Status = booking.Status,
            TotalAmount = booking.TotalAmount,
            CreatedAt = booking.CreatedAt,
            Items = booking.BookingItems.Select(bi => new BookingItemResponseDto
            {
                Id = bi.Id,
                ServiceName = bi.Service.NameEn,
                Quantity = bi.Quantity,
                UnitPrice = bi.UnitPrice,
                TotalPrice = bi.TotalPrice
            }).ToList(),
            AssignedStaff = booking.AssignedStaff != null ? new StaffInfoDto
            {
                Id = booking.AssignedStaff.Id,
                FirstName = booking.AssignedStaff.User.FirstName,
                LastName = booking.AssignedStaff.User.LastName,
                Rating = booking.AssignedStaff.Rating,
                CompletedJobs = booking.AssignedStaff.CompletedJobs
            } : null
        };
    }

    private string GenerateBookingNumber()
    {
        return $"BK{DateTime.UtcNow:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
    }
}
