using Microsoft.AspNetCore.Mvc;

public class VehiclesController : Controller
{

    public IActionResult Index()
    {
        // ViewData["vehicle"] = "Ford Mustang MACH-E 2022";
        ViewBag.Vehicle = "Kia Ceed 2017";

        return View("Index");
    }
}