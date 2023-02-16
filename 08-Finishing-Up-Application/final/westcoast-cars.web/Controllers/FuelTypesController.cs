using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.web.Data;
using westcoast_cars.web.Models;
using westcoast_cars.web.ViewModels.FuelTypes;

namespace westcoast_cars.web.Controllers
{
    [Route("[controller]")]
    public class FuelTypesController : Controller
    {
        private readonly WestcoastCarsContext _context;

        public FuelTypesController(WestcoastCarsContext context)
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
            var fueltTypes = await _context.FuelTypes
                .OrderBy(c => c.Name)
                .Select(m => new FuelTypeListViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToListAsync();

            var model = new FuelTypePostViewModel
            {
                FuelTypes = fueltTypes
            };

            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FuelTypePostViewModel model)
        {
            if (!ModelState.IsValid) return View();

            if (await _context.FuelTypes.SingleOrDefaultAsync(c => c.Name.ToUpper() == model.Name.ToUpper()) is not null)
            {
                ModelState.AddModelError("Name", $"Bränsle type {model.Name} finns redan i systemet.");
                return View();
            }

            var fueltype = new FuelTypeModel
            {
                Name = model.Name
            };

            await _context.FuelTypes.AddAsync(fueltype);
            if (await _context.SaveChangesAsync() > 0)
            {
                return RedirectToAction(nameof(Create));
            }

            ModelState.AddModelError("Name", "Ett fel inträffade");
            return View();
        }

    }
}