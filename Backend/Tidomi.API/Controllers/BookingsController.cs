using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tidomi.Application.DTOs.Bookings;
using Tidomi.Application.Interfaces;

namespace Tidomi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<ActionResult<BookingResponseDto>> CreateBooking([FromBody] CreateBookingDto dto)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
            var result = await _bookingService.CreateBookingAsync(userId, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookingResponseDto>> GetBookingById(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
        var result = await _bookingService.GetBookingByIdAsync(id, userId);

        if (result == null)
            return NotFound(new { message = "Booking not found" });

        return Ok(result);
    }

    [HttpGet("my-bookings")]
    public async Task<ActionResult<List<BookingResponseDto>>> GetMyBookings()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
        var result = await _bookingService.GetUserBookingsAsync(userId);
        return Ok(result);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult<List<BookingResponseDto>>> GetAllBookings()
    {
        var result = await _bookingService.GetAllBookingsAsync();
        return Ok(result);
    }

    [HttpPut("{id}/assign-staff")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> AssignStaff(Guid id, [FromBody] AssignStaffDto dto)
    {
        var result = await _bookingService.AssignStaffToBookingAsync(id, dto.StaffId);
        if (!result)
            return BadRequest(new { message = "Failed to assign staff" });

        return Ok(new { message = "Staff assigned successfully" });
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<ActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusDto dto)
    {
        var result = await _bookingService.UpdateBookingStatusAsync(id, dto.Status);
        if (!result)
            return BadRequest(new { message = "Failed to update status" });

        return Ok(new { message = "Status updated successfully" });
    }

    [HttpGet("available-dates")]
    public async Task<ActionResult<List<DateTime>>> GetAvailableDates([FromQuery] Guid? staffId = null)
    {
        var result = await _bookingService.GetAvailableDatesAsync(staffId);
        return Ok(result);
    }
}

public class AssignStaffDto
{
    public Guid StaffId { get; set; }
}

public class UpdateStatusDto
{
    public int Status { get; set; }
}
