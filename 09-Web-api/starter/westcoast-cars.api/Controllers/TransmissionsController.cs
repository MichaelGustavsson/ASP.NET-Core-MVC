using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.api.Data;

namespace westcoast_cars.api.Controllers
{
    [ApiController]
    [Route("api/v1/transmissions")]
    public class TransmissionsController : ControllerBase
    {
        private readonly WestcoastCarsContext _context;
        public TransmissionsController(WestcoastCarsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.TransmissionTypes.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.TransmissionTypes.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _context.TransmissionTypes.Where(c => c.Name.ToUpper().StartsWith(name.ToUpper())).ToListAsync();
            return Ok(result);
        }
    }
}