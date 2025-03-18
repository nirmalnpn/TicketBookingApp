namespace TicketBookingApp.Models
{
    public class Guide
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string Experience { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ProfilePicture { get; set; } // URL to guide's profile picture
        public string LanguageSpoken { get; set; }
        public Guid AdventureId { get; set; } // Linking the guide to an adventure
    }
}
