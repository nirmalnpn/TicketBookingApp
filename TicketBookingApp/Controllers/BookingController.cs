using Microsoft.AspNetCore.Mvc;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System;
using System.Threading.Tasks;

namespace TicketBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingRepository _bookingRepository;

        public BookingController(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // POST api/booking
        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] Booking booking)
        {
            var bookingId = await _bookingRepository.AddBooking(booking);
            return Ok(new { message = "Booking successful!", bookingId });
        }

        // GET api/booking
        [HttpGet]
        public async Task<IActionResult> GetBookingsByUserId()
        {
            var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var bookings = await _bookingRepository.GetBookingsByUserId(userId);
            return Ok(bookings);
        }
    }
}
