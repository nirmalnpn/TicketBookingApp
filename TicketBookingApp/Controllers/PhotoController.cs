using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using TicketBookingApp.Data;
using TicketBookingApp.Models;

namespace TicketBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly PhotoRepository _photoRepository;

        public PhotoController(PhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(IFormFile file, Guid adventureId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Generate a unique filename for the photo
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine("wwwroot", "uploads", fileName);

            // Save the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create the photo object and save to the database
            var photo = new Photo
            {
                Id = Guid.NewGuid(),
                AdventureId = adventureId,
                FileName = fileName,
                FilePath = filePath
            };

            await _photoRepository.AddPhoto(photo);

            return Ok(new { message = "Photo uploaded successfully", photoId = photo.Id });
        }

        [HttpGet("adventure/{adventureId}")]
        public async Task<IActionResult> GetPhotosByAdventureId(Guid adventureId)
        {
            var photos = await _photoRepository.GetPhotosByAdventureId(adventureId);
            return Ok(photos);
        }
    }
}
