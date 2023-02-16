using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.web.Data;
using westcoast_cars.web.Models;
using westcoast_cars.web.ViewModels.Manufacturers;

namespace westcoast_cars.web.Controllers
{
    [Route("[controller]")]
    public class ManufacturerController : Controller
    {
        private readonly WestcoastCarsContext _context;

        public ManufacturerController(WestcoastCarsContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var manufacturers = await _context.Manufacturers
                .OrderBy(c => c.Name)
                .Select(m => new ManufacturerListViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToListAsync();

            var model = new ManufacturerPostViewModel
            {
                Manufacturers = manufacturers
            };

            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ManufacturerPostViewModel model)
        {
            if (!ModelState.IsValid) return View();

            if (await _context.Manufacturers.SingleOrDefaultAsync(c => c.Name.ToUpper() == model.Name.ToUpper()) is not null)
            {
                ModelState.AddModelError("Name", $"Tillverkare {model.Name} finns redan i systemet.");
                return View();
            }

            var make = new ManufacturerModel
            {
                Name = model.Name.ToUpper()
            };

            await _context.Manufacturers.AddAsync(make);
            if (await _context.SaveChangesAsync() > 0)
            {
                return RedirectToAction(nameof(Create));
            }

            ModelState.AddModelError("Name", "Ett fel inträffade");
            return View();
        }
    }
}