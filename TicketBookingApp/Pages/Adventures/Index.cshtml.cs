using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBookingApp.Pages.Adventure
{
    public class IndexModel : PageModel
    {
        private readonly AdventureRepository _adventureRepository;

        public IndexModel(AdventureRepository adventureRepository)
        {
            _adventureRepository = adventureRepository;
        }

        public IEnumerable<Adventure> Adventures { get; set; }

        public async Task OnGetAsync()
        {
            Adventures = await _adventureRepository.GetAllAdventures();
        }
    }
}
