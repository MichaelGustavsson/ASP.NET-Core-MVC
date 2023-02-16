using Microsoft.AspNetCore.Mvc;

namespace westcoast_cars.web.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("Admin");
        }
    }
}