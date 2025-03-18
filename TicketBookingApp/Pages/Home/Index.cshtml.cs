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
            _adventureRepository = adventureRepository;
        }

        public IEnumerable<AdventureRepository> Adventures { get; set; }

        public async Task OnGetAsync()
        {
            Adventures = (IEnumerable<AdventureRepository>)await _adventureRepository.GetAllAdventures();
        }
    }
}
