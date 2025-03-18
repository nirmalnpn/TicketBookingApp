using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBookingApp.Models
{
    public class BookingModel : PageModel
    {
        private readonly AdventureRepository _adventureRepository;
        private readonly BookingRepository _bookingRepository;

        // Inject the repositories through the constructor
        public BookingModel(AdventureRepository adventureRepository, BookingRepository bookingRepository)
        {
            _adventureRepository = adventureRepository;
            _bookingRepository = bookingRepository;
        }

        // Property to hold the list of adventures
        public List<Adventure> Adventures { get; set; }

        // Property to hold the selected booking details (for the form)
        [BindProperty]
        public Booking Booking { get; set; }

        // On GET, load all adventures
        public async Task OnGetAsync()
        {
            // Fetch all adventures from the repository
            Adventures = (List<Adventure>)await _adventureRepository.GetAllAdventures();
        }

        // On POST, handle the booking submission
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If the form is not valid, reload the page with validation messages
                Adventures = (List<Adventure>)await _adventureRepository.GetAllAdventures();
                return Page();
            }

            // Set the UserId (this should come from the logged-in user, for example from the JWT token)
            Booking.UserId = Guid.NewGuid(); // Replace with actual user ID logic

            // Add the booking to the database
            var result = await _bookingRepository.AddBooking(Booking);

            if (result > 0)
            {
                // Redirect to a success page or show a success message
                return RedirectToPage("/Success"); // Success page or message
            }

            // If booking failed, you can display an error message
            return Page();
        }
    }
}
