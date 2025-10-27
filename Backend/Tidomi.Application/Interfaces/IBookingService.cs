using Tidomi.Application.DTOs.Bookings;

namespace Tidomi.Application.Interfaces;

public interface IBookingService
{
    Task<BookingResponseDto> CreateBookingAsync(Guid customerId, CreateBookingDto dto);
    Task<BookingResponseDto?> GetBookingByIdAsync(Guid id, Guid userId);
    Task<List<BookingResponseDto>> GetUserBookingsAsync(Guid userId);
    Task<List<BookingResponseDto>> GetAllBookingsAsync(); // Admin only
    Task<bool> AssignStaffToBookingAsync(Guid bookingId, Guid staffId);
    Task<bool> UpdateBookingStatusAsync(Guid bookingId, int status);
    Task<List<DateTime>> GetAvailableDatesAsync(Guid? staffId);
}
