﻿namespace TicketBookingApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string Role { get; set; } // User, Admin, etc.
    }
}
