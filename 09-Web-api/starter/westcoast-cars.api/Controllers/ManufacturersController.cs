using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.api.Data;

namespace westcoast_cars.api.Controllers
{
    [ApiController]
    [Route("api/v1/manufacturers")]
    public class ManufacturersController : ControllerBase
    {
        private readonly WestcoastCarsContext _context;
        public ManufacturersController(WestcoastCarsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var result = await _context.Manufacturers.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Manufacturers.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _context.Manufacturers.Where(c => c.Name.ToUpper().StartsWith(name.ToUpper())).ToListAsync();
            return Ok(result);
        }
    }
}