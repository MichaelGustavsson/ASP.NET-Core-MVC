using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using westcoast_cars.web.Data;
using westcoast_cars.web.Models;
using westcoast_cars.web.ViewModels.TransmissionTypes;

namespace westcoast_cars.web.Controllers
{
    [Route("[controller]")]
    public class TransmissionsController : Controller
    {
        private readonly WestcoastCarsContext _context;

        public TransmissionsController(WestcoastCarsContext context)
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
            var transmissions = await CreateList();

            var model = new TransmissionTypePostViewModel
            {
                Transmissions = transmissions
            };

            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransmissionTypePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Transmissions = await CreateList();
                return View(model);
            }

            if (await _context.TransmissionsTypes.SingleOrDefaultAsync(c => c.Name.ToUpper() == model.Name.ToUpper()) is not null)
            {
                ModelState.AddModelError("Name", $"Transmissions typ {model.Name} finns redan i systemet.");
                model.Transmissions = await CreateList();
                return View(model);
            }

            var transmission = new TransmissionsTypeModel
            {
                Name = model.Name
            };

            await _context.TransmissionsTypes.AddAsync(transmission);
            if (await _context.SaveChangesAsync() > 0)
            {
                return RedirectToAction(nameof(Create));
            }

            ModelState.AddModelError("Name", "Ett fel inträffade");
            return View();
        }

        private async Task<IList<TransmissionListViewModel>> CreateList()
        {
            var transmissions = await _context.TransmissionsTypes
                .OrderBy(c => c.Name)
                .Select(m => new TransmissionListViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToListAsync();

            return transmissions;
        }
    }
}