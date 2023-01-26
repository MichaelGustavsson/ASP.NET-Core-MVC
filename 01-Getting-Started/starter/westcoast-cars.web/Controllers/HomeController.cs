using Microsoft.AspNetCore.Mvc;

namespace westcoast_cars.web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
