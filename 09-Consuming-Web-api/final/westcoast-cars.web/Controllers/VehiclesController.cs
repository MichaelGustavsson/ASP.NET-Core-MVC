using System.Text.Json;
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
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _httpClient;
    private readonly string _baseUrl;

    public VehiclesController(WestcoastCarsContext context, IConfiguration config, IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
        _baseUrl = config.GetSection("apiSettings:baseUrl").Value;
        _context = context;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    // Denna metod svarar på HttpGet anrop
    [HttpGet("list")]
    public async Task<IActionResult> Index()
    {
        using var client = _httpClient.CreateClient();
        var response = await client.GetAsync($"{_baseUrl}/vehicles");

        if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

        var json = await response.Content.ReadAsStringAsync();

        var vehicles = JsonSerializer.Deserialize<IList<VehicleListViewModel>>(json, _options);

        return View("Index", vehicles);
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        using var client = _httpClient.CreateClient();
        var response = await client.GetAsync($"{_baseUrl}/vehicles/{id}");

        if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

        var json = await response.Content.ReadAsStringAsync();

        var vehicle = JsonSerializer.Deserialize<VehicleDetailViewModel>(json, _options);

        return View("Details", vehicle);
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var vehicle = await _context.Vehicles.FindAsync(id);

        vehicle.IsSold = true;

        _context.Vehicles.Update(vehicle);

        if (await _context.SaveChangesAsync() > 0)
        {
            return RedirectToAction(nameof(Index));
        }

        return View("Errors");
    }

    [HttpGet("edit")]
    public async Task<IActionResult> Edit(int id)
    {
        var found = await _context.Vehicles.FindAsync(id);

        if (found is null) return Content("Hittade inte bilen");

        var manufacturers = await _context.Manufacturers.ToListAsync();
        var fuelTypes = await _context.FuelTypes.ToListAsync();
        var transmissionsTypes = await _context.TransmissionsTypes.ToListAsync();

        var manufacturersList = new List<SelectListItem>();
        var fuelTypesList = new List<SelectListItem>();
        var transmissionsList = new List<SelectListItem>();

        foreach (var make in manufacturers)
        {
            manufacturersList.Add(new SelectListItem { Value = make.Id.ToString(), Text = make.Name });
        }

        foreach (var fueltype in fuelTypes)
        {
            fuelTypesList.Add(new SelectListItem { Value = fueltype.Id.ToString(), Text = fueltype.Name });
        }

        foreach (var transmission in transmissionsTypes)
        {
            transmissionsList.Add(new SelectListItem { Value = transmission.Id.ToString(), Text = transmission.Name });
        }

        var vehicle = new VehicleEditViewModel();

        vehicle.Id = id;
        vehicle.Manufacturer = found.Manufacturer.Id;
        vehicle.Model = found.Model;
        vehicle.ModelYear = found.ModelYear;
        vehicle.FuelType = found.FuelType.Id;
        vehicle.TransmissionsType = found.TransmissionsType.Id;
        vehicle.Mileage = found.Mileage;
        vehicle.Value = found.Value;
        vehicle.Description = found.Description;
        vehicle.Manufacturers = manufacturersList;
        vehicle.FuelTypes = fuelTypesList;
        vehicle.TransmissionsTypes = transmissionsList;

        return View("Edit", vehicle);
    }

    [HttpPost("edit")]
    public async Task<IActionResult> Edit(int id, VehicleEditViewModel vehicle)
    {
        var vehicleToUpdate = await _context.Vehicles.FindAsync(vehicle.Id);

        if (vehicleToUpdate is null) return Content("Hittade inte bilen");

        var selectedMake = await _context.Manufacturers.SingleOrDefaultAsync(c => c.Id == vehicle.Manufacturer);
        var selectedFueltype = await _context.FuelTypes.FindAsync(vehicle.FuelType);
        var selectedTransmission = await _context.TransmissionsTypes.FindAsync(vehicle.TransmissionsType);

        if (selectedMake is not null && selectedFueltype is not null && selectedTransmission is not null)
        {
            vehicleToUpdate.Manufacturer = selectedMake;
            vehicleToUpdate.Model = vehicle.Model;
            vehicleToUpdate.ModelYear = vehicle.ModelYear;
            vehicleToUpdate.FuelType = selectedFueltype;
            vehicleToUpdate.TransmissionsType = selectedTransmission;
            vehicleToUpdate.Mileage = vehicle.Mileage;
            vehicleToUpdate.Value = vehicle.Value;
            vehicleToUpdate.Description = vehicle.Description;

            _context.Vehicles.Update(vehicleToUpdate);

            if (await _context.SaveChangesAsync() > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Errors");
        }

        return View("Errors");
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var manufacturers = await _context.Manufacturers.ToListAsync();
        var fuelTypes = await _context.FuelTypes.ToListAsync();
        var transmissionsTypes = await _context.TransmissionsTypes.ToListAsync();

        var manufacturersList = new List<SelectListItem>();
        var fuelTypesList = new List<SelectListItem>();
        var transmissionsList = new List<SelectListItem>();

        foreach (var make in manufacturers)
        {
            manufacturersList.Add(new SelectListItem { Value = make.Id.ToString(), Text = make.Name });
        }

        foreach (var fueltype in fuelTypes)
        {
            fuelTypesList.Add(new SelectListItem { Value = fueltype.Id.ToString(), Text = fueltype.Name });
        }

        foreach (var transmission in transmissionsTypes)
        {
            transmissionsList.Add(new SelectListItem { Value = transmission.Id.ToString(), Text = transmission.Name });
        }

        var vehicle = new VehiclePostViewModel();
        vehicle.Manufacturers = manufacturersList;
        vehicle.FuelTypes = fuelTypesList;
        vehicle.TransmissionsTypes = transmissionsList;

        return View("Create", vehicle);
    }

    [HttpPost]
    public async Task<IActionResult> Create(VehiclePostViewModel vehicle)
    {
        if (!ModelState.IsValid) return View("Create", vehicle);

        var selectedMake = await _context.Manufacturers.SingleOrDefaultAsync(c => c.Id == vehicle.Manufacturer);
        var selectedFueltype = await _context.FuelTypes.FindAsync(vehicle.FuelType);
        var selectedTransmission = await _context.TransmissionsTypes.FindAsync(vehicle.TransmissionsType);

        if (selectedMake is not null && selectedFueltype is not null && selectedTransmission is not null)
        {
            var vehicleToAdd = new VehicleModel
            {
                RegistrationNumber = vehicle.RegistrationNumber,
                Model = vehicle.Model,
                ModelYear = vehicle.ModelYear,
                Mileage = vehicle.Mileage,
                Manufacturer = selectedMake,
                FuelType = selectedFueltype,
                TransmissionsType = selectedTransmission,
                Value = vehicle.Value,
                Description = vehicle.Description,
                ImageUrl = "no-car.png"
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