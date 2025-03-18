using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TicketBookingApp.Pages.Booking
{
    public class BookAdventureModel : PageModel
    {
        private readonly AdventureRepository _adventureRepository;
        private readonly BookingRepository _bookingRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public BookAdventureModel(AdventureRepository adventureRepository, BookingRepository bookingRepository, UserManager<IdentityUser> userManager)
        {
            _adventureRepository = adventureRepository ?? throw new ArgumentNullException(nameof(adventureRepository));
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [BindProperty]
        public TicketBookingApp.Models.Booking Booking { get; set; }

        public IEnumerable<Adventure> Adventures { get; private set; }

        public async Task OnGetAsync()
        {
            Adventures = await _adventureRepository.GetAllAdventures();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Challenge(); // Redirect to login if user is not authenticated
                }

                Booking.UserId = new Guid(user.Id);
                await _bookingRepository.AddBooking(Booking);
                return RedirectToPage("/Booking/Success");
            }

            return Page();
        }
    }
}
