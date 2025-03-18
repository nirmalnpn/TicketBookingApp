using Microsoft.AspNetCore.Mvc;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuideController : ControllerBase
    {
        private readonly GuideRepository _guideRepository;

        public GuideController(GuideRepository guideRepository)
        {
            _guideRepository = guideRepository;
        }

        // GET api/guide/{adventureId}
        [HttpGet("{adventureId}")]
        public async Task<ActionResult<IEnumerable<Guide>>> GetGuidesByAdventureId(Guid adventureId)
        {
            var guides = await _guideRepository.GetGuidesByAdventureId(adventureId);
            if (guides == null || !guides.Any())
            {
                return NotFound();
            }
            return Ok(guides);
        }

        // GET api/guide/{id}
        [HttpGet("detail/{id}")]
        public async Task<ActionResult<Guide>> GetGuideById(Guid id)
        {
            var guide = await _guideRepository.GetGuideById(id);
            if (guide == null)
            {
                return NotFound();
            }
            return Ok(guide);
        }

        // POST api/guide
        [HttpPost]
        public async Task<IActionResult> AddGuide([FromBody] Guide guide)
        {
            var guideId = await _guideRepository.AddGuide(guide);
            return Ok(new { message = "Guide added successfully!", guideId });
        }
    }
}
