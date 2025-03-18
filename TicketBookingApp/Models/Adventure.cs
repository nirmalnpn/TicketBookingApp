using System;

namespace TicketBookingApp.Models
{
    public class Adventure
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public string ImageUrl { get; set; }
    }
}
