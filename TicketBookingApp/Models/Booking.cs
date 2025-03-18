using System;

namespace TicketBookingApp.Models
{
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique ID for the booking
        public Guid UserId { get; set; }               // The user who made the booking
        public Guid AdventureId { get; set; }          // The adventure being booked
        public DateTime BookingDate { get; set; }      // The date of booking
    }
}
