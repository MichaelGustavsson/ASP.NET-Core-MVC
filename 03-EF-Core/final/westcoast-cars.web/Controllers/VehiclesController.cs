using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.web.Data;
using westcoast_cars.web.Models;

[Route("Vehicles")]
public class VehiclesController : Controller
{
    private readonly WestcoastCarsContext _context;
    public VehiclesController(WestcoastCarsContext context)
    {
        _context = context;
    }

    // Denna metod svarar på HttpGet anrop
    public async Task<IActionResult> Index()
    {
        var vehicles = await _context.Vehicles.ToListAsync();

        return View("Index", vehicles);
    }

    // Denna metod svarar på HttpGet anrop
    // Http://localhost:5169/vehicles/details/6
    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int Id)
    {
        var vehicle = await _context.Vehicles.FindAsync(Id);
        return View("Details", vehicle);
    }

    // Denna metod kommer att skapa ett nytt tomt vehicle objekt
    // och skicka detta till en vy som skapar ett formulär.
    [HttpGet("create")]
    public IActionResult Create()
    {
        var vehicle = new VehicleModel();
        return View("Create", vehicle);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(VehicleModel vehicle)
    {
        if (!ModelState.IsValid) return View("Create", vehicle);

        await _context.Vehicles.AddAsync(vehicle);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}