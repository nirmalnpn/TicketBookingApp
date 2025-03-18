namespace TicketBookingApp.Models
{
    public class Photo
    {
        public Guid Id { get; set; }
        public Guid AdventureId { get; set; }  // AdventureId to relate the photo to an adventure
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
