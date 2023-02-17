using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.api.Data;
using westcoast_cars.api.Entities;
using westcoast_cars.api.ViewModels;

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

        [HttpGet("{name}/vehicles")]
        public async Task<IActionResult> ListVehiclesByMake(string name)
        {
            var result = await _context.Manufacturers
                .Where(c => c.Name.ToUpper().StartsWith(name.ToUpper()))
                .Select(m => new
                {
                    Name = m.Name,
                    Vehicles = m.Vehicles.Select(v => new
                    {
                        RegistrationNumber = v.RegistrationNumber,
                        Model = v.Model,
                        ModelYear = v.ModelYear,
                        Mileage = v.Mileage
                    }).ToList()
                }).ToListAsync();


            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(PostViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest("Information saknas!");

            if (await _context.Manufacturers.SingleOrDefaultAsync(c => c.Name.ToUpper() == model.Name.ToUpper()) is not null)
                return BadRequest($"Tillverkaren med namn {model.Name} finns redan i systemet");

            var make = new Manufacturer
            {
                Name = model.Name
            };

            try
            {
                await _context.Manufacturers.AddAsync(make);

                if (await _context.SaveChangesAsync() > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { id = make.Id }, new { Id = make.Id, Name = make.Name });
                }
                return StatusCode(500, "Internal Server Error");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}