using iTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using TerminalLibrary.Interfaces;
using TerminalLibrary.Models;

namespace iTerminal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IClientService _client { get; }
        public MyContext _context { get; }

        public HomeController(ILogger<HomeController> logger, IClientService client, MyContext context)
        {
            _logger = logger;
            _client = client;
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Show")]
        public IActionResult Show(Linja searched)
        {
            if (ModelState.IsValid)
            {
                return View("Shfaq", _client.GetSearchedUnit(searched));
            }
            return View("Index");
        }

        [HttpGet("present")]
        public IActionResult Shfaq(List<Unit> AllUnits)
        {
            return View(AllUnits);
        }


        [SessionCheck]
        [HttpGet("rezervo/{id}")]
        public IActionResult Rezervo(int id)
        {
            Unit? requestedUnit = _client.GetUnit(id);
            UserReg? loggedUser = _client.GetLoggedUser((int)HttpContext.Session.GetInt32("UserId"));
            PaginatedProductViewModel data = new PaginatedProductViewModel
            {
                Unit = requestedUnit,
                User = loggedUser
            };
            return View(data);
        }


        [SessionCheck]
        [HttpPost("confirm")]
        public IActionResult Confirm(Trip trip)
        {
            if (_client.ConfirmTrip(trip))
            {
                return RedirectToAction("UpcomingTrips");
            }
            return RedirectToAction("Rezervo", new { id = trip.TripId });
        }


        [SessionCheck]
        [HttpGet("upcoming")]
        public IActionResult UpComingTrips(int filter = 0)
        {
            List<Trip> upcomingTrips = new List<Trip>();
            if (filter==1)
            {
                    return View(_client.GetPastTrips((int)HttpContext.Session.GetInt32("UserId")));
            }
            return View(_client.GetFutureTrips((int)HttpContext.Session.GetInt32("UserId")));
        }


        [SessionCheck]
        [HttpGet("past")]
        public IActionResult PastTrips()
        {
            return RedirectToAction("UpComingTrips", new { filter = 1 });
        }

        [SessionCheck]
        [HttpGet("anullo/{id}")]
        public IActionResult Anullo(int id)
        {
            if (_client.CancelTrip(id))
            {
                return RedirectToAction("UpcomingTrips");
            }
            return View(UpComingTrips);
        }

        [SessionCheck]
        [HttpGet("print/{id}")]
        public IActionResult Print(int id)
        {
            return View(_client.GetTrip(id));
        }

        [SessionCheck]
        [HttpGet("mynotifications")]
        public IActionResult MyNotifications()
        {
            return View(_client.GetAllMessages((int)HttpContext.Session.GetInt32("UserId")));
        }

        [SessionCheck]
        [HttpGet("showmessage/{id}")]
        public IActionResult ShowMessage(int id)
        {

            Message? message = _context.Messages.Include(e => e.Company).FirstOrDefault(e => e.MessageId == id);
            if (message.UserId != HttpContext.Session.GetInt32("UserId"))
            {
                return RedirectToAction("MyNotifications");
            }
            message.Seen = true;
            UserReg? user = _context.Users.Include(e => e.AllMessages).FirstOrDefault(e => e.id == HttpContext.Session.GetInt32("UserId"));
            _context.SaveChanges();
            if (user.AllMessages.Any(e => e.Seen != false))
            {
                HttpContext.Session.Remove("Messages");
            }
            return View(_client.GetMessage(id));
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new TerminalLibrary.Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}



public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }
}