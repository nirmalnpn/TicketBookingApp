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
            _guideRepository = guideRepository;
            _adventureRepository = adventureRepository;
        }

        public Adventure Adventure { get; set; }
        public IEnumerable<Guide> Guides { get; set; }

        public async Task OnGetAsync(Guid adventureId)
        {
            Adventure = await _adventureRepository.GetAdventureById(adventureId);
            if (Adventure != null)
            {
                Guides = await _guideRepository.GetGuidesByAdventureId(adventureId);
            }
        }
    }
}
