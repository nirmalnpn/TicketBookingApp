using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBookingApp.Pages.Guide
{
    public class IndexModel : PageModel
    {
        private readonly GuideRepository _guideRepository;
        private readonly AdventureRepository _adventureRepository;

        public IndexModel(GuideRepository guideRepository, AdventureRepository adventureRepository)
        {
            _guideRepository = guideRepository ?? throw new ArgumentNullException(nameof(guideRepository));
            _adventureRepository = adventureRepository ?? throw new ArgumentNullException(nameof(adventureRepository));
        }

        public Adventure Adventure { get; private set; }
        public IEnumerable<TicketBookingApp.Models.Guide> Guides { get; private set; }

        public async Task OnGetAsync(Guid adventureId)
        {
            if (adventureId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Invalid adventure ID.");
                return;
            }

            Adventure = await _adventureRepository.GetAdventureById(adventureId);
            if (Adventure == null)
            {
                ModelState.AddModelError(string.Empty, "Adventure not found.");
                return;
            }

            Guides = await _guideRepository.GetGuidesByAdventureId(adventureId);
        }
    }
}
