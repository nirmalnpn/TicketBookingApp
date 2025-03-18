using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBookingApp.Pages.Photo
{
    public class IndexModel : PageModel
    {
        private readonly PhotoRepository _photoRepository;
        private readonly AdventureRepository _adventureRepository;

        public IndexModel(PhotoRepository photoRepository, AdventureRepository adventureRepository)
        {
            _photoRepository = photoRepository;
            _adventureRepository = adventureRepository;
        }

        public IEnumerable<PhotoRepository> Photos { get; set; }
        public IEnumerable<AdventureRepository> Adventures { get; set; }

        [BindProperty]
        public PhotoRepository NewPhoto { get; set; }

        public async Task OnGetAsync()
        {
            Photos = await _photoRepository.GetAllPhotos();
            Adventures = (IEnumerable<AdventureRepository>)await _adventureRepository.GetAllAdventures();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Save the uploaded file and add it to the database
                var filePath = "/path/to/uploaded/photo"; // Handle file upload logic
                NewPhoto.FilePath = filePath;

                await _photoRepository.AddPhoto(NewPhoto);
                return RedirectToPage("/Photo/Index");
            }

            return Page();
        }
    }
}
