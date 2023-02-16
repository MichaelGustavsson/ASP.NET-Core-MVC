using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.api.Data;

namespace westcoast_cars.api.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly WestcoastCarsContext _context;
        public VehiclesController(WestcoastCarsContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.Vehicles.ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Vehicles
            .FindAsync(id);

            return Ok(result);
        }

        [HttpGet("regno/{regNo}")]
        public async Task<IActionResult> GetByRegNo(string regNo)
        {
            var result = await _context.Vehicles.SingleOrDefaultAsync(c => c.RegistrationNumber == regNo);

            return Ok(result);
        }
    }
}