using Microsoft.AspNetCore.Mvc;
using TicketBookingApp.Data;
using TicketBookingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdventureController : ControllerBase
    {
        private readonly AdventureRepository _adventureRepository;

        public AdventureController(AdventureRepository adventureRepository)
        {
            _adventureRepository = adventureRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Adventure>> GetAdventures()
        {
            return await _adventureRepository.GetAllAdventures();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Adventure>> GetAdventureById(Guid id)
        {
            var adventure = await _adventureRepository.GetAdventureById(id);
            if (adventure == null)
            {
                return NotFound();
            }
            return adventure;
        }
    }
}
