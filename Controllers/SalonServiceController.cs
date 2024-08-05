using AppointmentAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Admin")]
    public class SalonServiceController : Controller
    {
        private readonly HaircutSalonDbContext _context;
        //private SalonService_service salonService=new SalonService_service(_context);

        public SalonServiceController(HaircutSalonDbContext context)
        {
            _context = context;

        }

        // GET: api/SalonServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entities.SalonService>>> GetAll()
        {
            //return (salonService.Get());
            return await _context.SalonService.ToListAsync();
        }

        // GET: api/SalonServices/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Entities.SalonService>> GetServiceById(int id)
        {
            var _service = await _context.SalonService.FindAsync(id);

            if (_service == null)
            {
                return NotFound();
            }

            return _service;
        }

        // PUT: api/SalonServices/1
        [HttpPut("{id}")]
        public async Task<IActionResult> EditSalonService(int id, Entities.SalonService _service)
        {
            if (id != _service.ServiceId)
            {
                return BadRequest();
            }

            _context.Entry(_service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalonServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SalonServices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entities.SalonService>> AddSalonService(Entities.SalonService _service)
        {
            _context.SalonService.Add(_service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalonService", new { id = _service.ServiceId }, _service);
        }

        // DELETE: api/SalonServices/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalonService(int id)
        {
            var service = await _context.SalonService.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.SalonService.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalonServiceExists(int id)
        {
            return _context.SalonService.Any(e => e.ServiceId == id);
        }
    }
}
