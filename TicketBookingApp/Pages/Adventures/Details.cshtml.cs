using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using TicketBookingApp.Data;
using TicketBookingApp.Models; // Add this using directive

namespace TicketBookingApp.Pages.Adventures
{
    public class DetailsModel : PageModel
    {
        private readonly AdventureRepository _adventureRepository;

        public Adventure Adventure { get; private set; }

        public DetailsModel(AdventureRepository adventureRepository)
        {
            _adventureRepository = adventureRepository ?? throw new ArgumentNullException(nameof(adventureRepository));
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid adventure ID.");
            }

            Adventure = await _adventureRepository.GetAdventureById(id);
            if (Adventure == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
