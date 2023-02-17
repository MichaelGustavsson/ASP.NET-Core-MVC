using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.api.Data;
using westcoast_cars.api.Entities;
using westcoast_cars.api.ViewModels;

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

        [HttpPost]
        public async Task<IActionResult> Add(VehiclePostViewModel vehicle)
        {
            if (!ModelState.IsValid)
            {
                // return BadRequest("All information är inte med i anropet");
                return ValidationProblem();
            }

            if (await _context.Vehicles.SingleOrDefaultAsync(c => c.RegistrationNumber == vehicle.RegistrationNumber) is not null)
            {
                return BadRequest($"Bilen med regnummer {vehicle.RegistrationNumber} finns redan i systemet");
            }

            // Hämta tillverkaren
            var make = await _context.Manufacturers.SingleOrDefaultAsync(c => c.Name.ToUpper() == vehicle.Make.ToUpper());
            // Kontrollera att vi har den tillverkaren
            if (make is null) return NotFound($"Tyvärr vi kunde inte hitta en tillverkare med namnet {vehicle.Make}");

            // Hämta bränsletyp
            var fueltype = await _context.FuelTypes.SingleOrDefaultAsync(c => c.Name.ToUpper() == vehicle.FuelType.ToUpper());
            // Kontrollera att vi har den bränsletypen
            if (fueltype is null) return NotFound($"Tyvärr vi kunde inte hitta en bränsletyp med namnet {vehicle.FuelType}");

            // Hämta växellåda
            var transmission = await _context.TransmissionTypes.SingleOrDefaultAsync(c => c.Name.ToUpper() == vehicle.Transmission.ToUpper());
            // Kontrollera att vi har den växellådan
            if (transmission is null) return NotFound($"Tyvärr vi kunde inte hitta en tillverkare med namnet {vehicle.Transmission}");

            var vehicleToAdd = new Vehicle
            {
                RegistrationNumber = vehicle.RegistrationNumber,
                Manufacturer = make,
                Model = vehicle.Model,
                ModelYear = vehicle.ModelYear,
                Mileage = vehicle.Mileage,
                TransmissionsType = transmission,
                FuelType = fueltype,
                Value = vehicle.Value,
                IsSold = vehicle.IsSold,
                Description = vehicle.Description,
            };

            try
            {
                await _context.Vehicles.AddAsync(vehicleToAdd);

                if (await _context.SaveChangesAsync() > 0)
                {
                    // return StatusCode(201);
                    return CreatedAtAction(nameof(GetById), new { id = vehicleToAdd.Id },
                    new
                    {
                        Id = vehicleToAdd.Id,
                        RegistrationNumber = vehicleToAdd.RegistrationNumber,
                        Model = vehicleToAdd.Model,
                        ModelYear = vehicleToAdd.ModelYear
                    });
                }

                return StatusCode(500, "Internal Server Error");
            }
            catch (Exception ex)
            {
                // loggning till en databas som hanterar debug information...
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }

        }
    }
}