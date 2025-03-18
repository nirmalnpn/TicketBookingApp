using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBookingApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AdventureRepository _adventureRepository;

        public IndexModel(AdventureRepository adventureRepository)
        {
            _adventureRepository = adventureRepository ?? throw new ArgumentNullException(nameof(adventureRepository));
        }

        public IEnumerable<Adventure> Adventures { get; private set; }

        public async Task OnGetAsync()
        {
            Adventures = await _adventureRepository.GetAllAdventures();
        }
    }
}

