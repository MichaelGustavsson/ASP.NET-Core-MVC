using Microsoft.AspNetCore.Mvc;
using westcoast_cars.web.Models;

[Route("Vehicles")]
public class VehiclesController : Controller
{

    // Denna metod svarar på HttpGet anrop
    public IActionResult Index()
    {
        var vehicles = new List<VehicleModel>{
            new VehicleModel{Id = 1,Make = "Volvo", Model = "V60", ModelYear = "2019", Mileage = 25000},
            new VehicleModel{Id = 2,Make = "Ford", Model = "Kuga", ModelYear = "2015", Mileage = 89750},
            new VehicleModel{Id = 3,Make = "Kia", Model = "xCeed", ModelYear = "2022", Mileage = 8500}
        };


        return View("Index", vehicles);
    }

    // Denna metod svarar på HttpGet anrop
    // Http://localhost:5169/vehicles/details/6
    [HttpGet("details/{id}")]
    public IActionResult Details(int Id)
    {
        // Vi kommer att hämta en bil med hjälp av inskickat id
        // och vi kommer att hämta detta ifrån en databas...
        var vehicle = new VehicleModel { Id = 3, Make = "Kia", Model = "xCeed", ModelYear = "2022", Mileage = 8500 };

        return View("Details", vehicle);
    }
}