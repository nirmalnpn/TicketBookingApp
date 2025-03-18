using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBookingApp.Pages.Booking
{
    public class BookAdventureModel : PageModel
    {
        private readonly AdventureRepository _adventureRepository;
        private readonly BookingRepository _bookingRepository;

        public BookAdventureModel(AdventureRepository adventureRepository, BookingRepository bookingRepository)
        {
            _adventureRepository = adventureRepository;
            _bookingRepository = bookingRepository;
        }

        [BindProperty]
        public BookingModel Booking { get; set; }

        public IEnumerable<AdventureRepository> Adventures { get; set; }

        public async Task OnGetAsync()
        {
            Adventures = (IEnumerable<AdventureRepository>)await _adventureRepository.GetAllAdventures();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Booking.UserId = Guid.NewGuid(); // This should come from the logged-in user
                await _bookingRepository.AddBooking(Booking);
                return RedirectToPage("/Booking/Success");
            }

            return Page();
        }
    }
}
