using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace TicketBookingApp.Pages.Photo
{
    public class IndexModel : PageModel
    {
        private readonly PhotoRepository _photoRepository;
        private readonly AdventureRepository _adventureRepository;

        public IndexModel(PhotoRepository photoRepository, AdventureRepository adventureRepository)
        {
            _photoRepository = photoRepository ?? throw new ArgumentNullException(nameof(photoRepository));
            _adventureRepository = adventureRepository ?? throw new ArgumentNullException(nameof(adventureRepository));
        }

        public IEnumerable<TicketBookingApp.Models.Photo> Photos { get; private set; }
        public IEnumerable<TicketBookingApp.Models.Adventure> Adventures { get; private set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        [BindProperty]
        public TicketBookingApp.Models.Photo NewPhoto { get; set; }

        public async Task OnGetAsync()
        {
            Photos = await _photoRepository.GetAllPhotos() as IEnumerable<TicketBookingApp.Models.Photo>;
            Adventures = await _adventureRepository.GetAllAdventures();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Upload != null)
                {
                    var filePath = Path.Combine("wwwroot/uploads", Upload.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Upload.CopyToAsync(stream);
                    }

                    NewPhoto.FilePath = filePath;
                    NewPhoto.FileName = Upload.FileName;
                    NewPhoto.UploadedAt = DateTime.UtcNow;

                    await _photoRepository.AddPhoto(NewPhoto);
                    return RedirectToPage("/Photo/Index");
                }
                ModelState.AddModelError(string.Empty, "Please upload a file.");
            }

            return Page();
        }
    }
}
