using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using TerminalLibrary.Interfaces;
using TerminalLibrary.Models;

namespace iTerminal.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public IAdminService _admin { get; }
        public IWebHostEnvironment _environment { get; }
        public MyContext _context { get; }

        public AdminController(ILogger<AdminController> logger, IAdminService admin, IWebHostEnvironment environment, MyContext context)
        {
            _logger = logger;
            _admin = admin;
            _environment = environment;
            _context = context;
        }

        [SessionCheck]
        [AdminCheck]
        public IActionResult Index()
        {
            return RedirectToAction("MyUnits");
        }

        [AdminCheck]
        [HttpGet("createunit")]
        public IActionResult CreateUnit()
        {
            return View(_admin.GetRegisteredRoutes());
        }

        [AdminCheck]
        [HttpPost("registerunit")]
        public async Task<IActionResult> RegisterUnit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                if (unit.ImageFile != null && unit.ImageFile.Length > 0)
                {
                    Console.WriteLine("U ekzekutua");
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + unit.ImageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await unit.ImageFile.CopyToAsync(fileStream);
                    }
                    unit.ImageFileName = uniqueFileName;
                    unit.ImageData = System.IO.File.ReadAllBytes(filePath);
                }
                _context.Units.Add(unit);
                _context.SaveChanges();
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreateUnit", _admin.GetRegisteredRoutes());

        }

        [AdminCheck]
        [HttpPost("registerroute")]
        public IActionResult RegisterRoute(Linja route)
        {
            if (ModelState.IsValid)
            {
                if (!_admin.CheckRoute(route))
                {
                    ModelState.AddModelError("PointA", "Vlerat nuk jane te vlefshme");
                    return View("CreateUnit", _admin.GetRegisteredRoutes());
                }
                if (_admin.RegisterRoute(route))
                {
                    ModelState.AddModelError("PointA", "Linja nuk mund te regjistrohet");
                    return View("CreateUnit", _admin.GetRegisteredRoutes());
                }
                return RedirectToAction("CreateUnit");
            }
            return View("CreateUnit", _admin.GetRegisteredRoutes());
        }


        [AdminCheck]
        [HttpGet("myunitspast")]
        public IActionResult MyUnitsPast()
        {

            return RedirectToAction("MyUnits", new { nisja = 1 });
        }

        [AdminCheck]
        [HttpGet("myunits")]
        public IActionResult MyUnits(int page = 1, int nisja = 0, string message = null)
        {
            ViewBag.Message = message;
            return View(_admin.GetMyUnits((int) HttpContext.Session.GetInt32("AdminId"), page, nisja));
        }

        [AdminCheck]
        [HttpGet("destroy/{id}")]
        public IActionResult Delete(int id)
        {
            if (!_admin.DeleteUnit(id, (int)HttpContext.Session.GetInt32("AdminId")))
            {
                return RedirectToAction("MyUnits", new { message = "Fshirja e udhetimit deshtoi!" });
            }

            return RedirectToAction("MyUnits", new { message = "Udhetimi u fshi me sukses!" });
        }


        [AdminCheck]
        [HttpGet("editunit")]
        public IActionResult EditUnit(int id)
        {
            return View(_admin.GetUnitAndLinja(id));
        }


        [AdminCheck]
        [HttpPost("updateunit")]
        public async Task<IActionResult> UpdateUnit(DataTwo data, int id)
        {
            if (ModelState.IsValid)
            {

                Unit unitFromDb = _admin.GetUnitById(id);


                unitFromDb.Name = data.Unit.Name;
                unitFromDb.Price = data.Unit.Price;
                unitFromDb.Seats = data.Unit.Seats;
                unitFromDb.Nisja = data.Unit.Nisja;
                unitFromDb.UpdatedAt = DateTime.Now;
                unitFromDb.RouteId = data.Unit.RouteId;
                if (data.Unit.ImageFile != null && data.Unit.ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + data.Unit.ImageFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await data.Unit.ImageFile.CopyToAsync(fileStream);
                    }

                    unitFromDb.ImageFileName = uniqueFileName;
                    unitFromDb.ImageData = await System.IO.File.ReadAllBytesAsync(filePath);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("MyUnits");
            }

            data.Routes = _admin.GetRoutes();
            return View("EditUnit", data);

        }


        [SessionCheck]
        [HttpGet("unitdetails")]
        public IActionResult UnitDetails(int id)
        {
            Unit? requestedUnit = _admin.GetUnitById(id);
            if (requestedUnit.CreatorId != HttpContext.Session.GetInt32("AdminId"))
            {
                return RedirectToAction("MyUnits");
            }
            return View(requestedUnit);
        }



    }


}











public class AdminCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("AdminId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}