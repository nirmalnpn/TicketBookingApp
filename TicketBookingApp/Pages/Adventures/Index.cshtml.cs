using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using TicketBookingApp.Models; // Add this using directive
using Microsoft.Extensions.Logging; // Add this using directive

namespace TicketBookingApp.Pages.Adventures
{
    public class IndexModel : PageModel
    {
        private readonly AdventureRepository _adventureRepository;
        private readonly ILogger<IndexModel> _logger; // Add logger

        public IndexModel(AdventureRepository adventureRepository, ILogger<IndexModel> logger) // Inject logger
        {
            _adventureRepository = adventureRepository ?? throw new ArgumentNullException(nameof(adventureRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Initialize logger
        }

        public IEnumerable<Adventure> Adventures { get; private set; }

        public async Task OnGetAsync()
        {
            try
            {
                Adventures = await _adventureRepository.GetAllAdventures();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting adventures."); 
            }
        }
    }
}
