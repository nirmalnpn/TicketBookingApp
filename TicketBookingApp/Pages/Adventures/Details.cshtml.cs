using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System;
using System.Threading.Tasks;

namespace TicketBookingApp.Pages.Adventure
{
    public class DetailsModel : PageModel
    {
        private readonly AdventureRepository _adventureRepository;

        public AdventureRepository Adventure { get; set; }

        public DetailsModel(AdventureRepository adventureRepository)
        {
            _adventureRepository = adventureRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Adventure = await _adventureRepository.GetAdventureById(id);
            if (Adventure == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
