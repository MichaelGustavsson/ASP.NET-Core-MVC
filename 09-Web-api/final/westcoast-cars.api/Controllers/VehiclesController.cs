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
            var result = await _context.Vehicles
                .Select(v => new
                {
                    Id = v.Id,
                    Name = $"{v.Manufacturer.Name} {v.Model}",
                    ModelYear = v.ModelYear,
                    Mileage = v.Mileage,
                    ImageUrl = v.ImageUrl
                })
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _context.Vehicles
                .Select(v => new
                {
                    Id = v.Id,
                    RegistrationNumber = v.RegistrationNumber,
                    Name = $"{v.Manufacturer.Name} {v.Model}",
                    Make = v.Manufacturer.Name,
                    Model = v.Model,
                    ModelYear = v.ModelYear,
                    Mileage = v.Mileage,
                    Fueltype = v.FuelType.Name,
                    Transmission = v.TransmissionsType.Name,
                    ImageUrl = v.ImageUrl
                })
                .SingleOrDefaultAsync(c => c.Id == id);

            return Ok(result);
        }

        [HttpGet("regno/{regNo}")]
        public async Task<IActionResult> GetByRegNo(string regNo)
        {
            var result = await _context.Vehicles
                .Select(v => new
                {
                    Id = v.Id,
                    RegistrationNumber = v.RegistrationNumber,
                    Name = $"{v.Manufacturer.Name} {v.Model}",
                    Make = v.Manufacturer.Name,
                    Model = v.Model,
                    ModelYear = v.ModelYear,
                    Mileage = v.Mileage,
                    Fueltype = v.FuelType.Name,
                    Transmission = v.TransmissionsType.Name,
                    ImageUrl = v.ImageUrl
                })
                .SingleOrDefaultAsync(c => c.RegistrationNumber == regNo);

            return Ok(result);
        }
    }
}