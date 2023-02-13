using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.web.Data;
using westcoast_cars.web.Models;
using westcoast_cars.web.ViewModels.Vehicles;

[Route("Vehicles")]
public class VehiclesController : Controller
{
    private readonly WestcoastCarsContext _context;
    public VehiclesController(WestcoastCarsContext context)
    {
        _context = context;
    }

    // Denna metod svarar på HttpGet anrop
    [HttpGet()]
    public async Task<IActionResult> Index()
    {
        var vehicles = await _context.Vehicles
            .Include(c => c.Manufacturer)
            .Select(v => new VehicleListViewModel
            {
                Id = v.Id,
                Manufacturer = v.Manufacturer.Name,
                Model = v.Model,
                ModelYear = v.ModelYear
            })
            .ToListAsync();

        return View("Index", vehicles);
    }

    // Denna metod svarar på HttpGet anrop
    // Http://localhost:5169/vehicles/details/6
    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int Id)
    {
        var vehicle = await _context.Vehicles
        .Include(c => c.Manufacturer)
        .Select(v => new VehicleDetailsViewModel
        {
            Id = v.Id,
            Manufacturer = v.Manufacturer.Name,
            Model = v.Model,
            ModelYear = v.ModelYear,
            Mileage = v.Mileage
        })
        .SingleOrDefaultAsync(c => c.Id == Id);

        return View("Details", vehicle);
    }

    // Denna metod kommer att skapa ett nytt tomt vehicle objekt
    // och skicka detta till en vy som skapar ett formulär.
    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var manufacturers = await _context.Manufacturers.ToListAsync();
        var fuelTypes = await _context.FuelTypes.ToListAsync();

        var manufacturersList = new List<SelectListItem>();
        var fuelTypesList = new List<SelectListItem>();

        foreach (var make in manufacturers)
        {
            manufacturersList.Add(new SelectListItem { Value = make.Id.ToString(), Text = make.Name });
        }

        foreach (var fueltype in fuelTypes)
        {
            fuelTypesList.Add(new SelectListItem { Value = fueltype.Id.ToString(), Text = fueltype.Name });
        }

        var vehicle = new VehiclePostViewModel();
        vehicle.Manufacturers = manufacturersList;
        vehicle.FuelTypes = fuelTypesList;

        return View("Create", vehicle);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(VehiclePostViewModel vehicle)
    {
        if (!ModelState.IsValid) return View("Create", vehicle);

        var make = await _context.Manufacturers.SingleOrDefaultAsync(c => c.Id == vehicle.Manufacturer);
        var selectedFueltype = await _context.FuelTypes.FindAsync(vehicle.FuelType);

        if (make is not null && selectedFueltype is not null)
        {
            var vehicleToAdd = new VehicleModel
            {
                RegistrationNumber = vehicle.RegistrationNumber,
                Model = vehicle.Model,
                ModelYear = vehicle.ModelYear,
                Mileage = vehicle.Mileage,
                Manufacturer = make,
                FuelType = selectedFueltype
            };

            await _context.Vehicles.AddAsync(vehicleToAdd);

            if (await _context.SaveChangesAsync() > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Errors");
        }

        return View("Errors");
    }
}