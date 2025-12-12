using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalLibrary.Interfaces;
using TerminalLibrary.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace TerminalLibrary.Services
{
    public class AdminService : IAdminService
    {
        private readonly MyContext _context;


        public AdminService(MyContext context)
        {
            _context = context;
        }
        public DataTwo GetRegisteredRoutes()
        {
            return new DataTwo {
                Routes = _context.Routes.ToList()
        };
        }


        public bool CheckRoute(Linja route) { 
        
            if ( _context.Routes.Any(e => e.PointA == route.PointA && e.PointB == route.PointB) || route.PointA == route.PointB){
                return false;
            }
            return true;
        }

        public bool RegisterRoute(Linja route)
        {
            _context.Add(route);
            _context.SaveChanges();
            return Save();
        }

       


        public PaginatedProductViewModel GetMyUnits(int userId, int page, int nisja)
        {
            int pageSize = 10; // numri i elementeve te shfaqur ne faqe

            IQueryable<Unit> unitsQuery = _context.Units.AsQueryable(); ;

            
            switch (nisja)
            {
                case 0:
                    unitsQuery = _context.Units.Include(e => e.Creator).Include(e => e.trips).ThenInclude(e => e.User).Include(e => e.route).Where(e => e.CreatorId == userId && e.Nisja > DateTime.Now); ;
                    break;
                case 1:
                    unitsQuery = _context.Units.Include(e => e.Creator).Include(e => e.route).Where(e => e.CreatorId == userId && e.Nisja < DateTime.Now); ;
                    break;
            }

            
            var totalProducts = unitsQuery.Count();      // merret nr total i njesive
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);    // llogaritet numri i faqeve

            
            page = Math.Max(1, Math.Min(page, totalPages));  // nr i faqeve eshte brenda vlerave

            var units = unitsQuery.Skip((page - 1) * pageSize).Take(pageSize).ToList(); //merren units qe duhen shfaqur per faqen

            
            var viewModel = new PaginatedProductViewModel
            {
                Units = units,
                PageNumber = page,
                TotalPages = totalPages,
            };
            return viewModel;
        }

        public bool DeleteUnit(int id, int userId)
        {
            Unit? unitToDelete = _context.Units.Include(e => e.Creator).Include(e => e.trips).ThenInclude(e => e.User).Include(e => e.route).FirstOrDefault(e => e.UnitId == id);
            if (unitToDelete.Nisja < DateTime.Now || unitToDelete.CreatorId != userId)
            {
                return false;
            }
            String s = $"Na vjen keq tu njoftojme se udhetimi juaj i dates {unitToDelete.Nisja.ToString("dd, MMM yyyy")} i linjes {unitToDelete.route.PointA} - {unitToDelete.route.PointB} me kompanine {unitToDelete.Creator.Name} eshte anulluar. Ju lutem qendroni ne pritje per nje mundesi tjeter. Faleminderit!";
            foreach (var item in unitToDelete.trips)
            {
                Message? message = new Message();
                message.Content = s;
                message.UserId = item.UserId;
                message.CompanyId = userId;
                _context.Add(message);
            }
            List<Trip> trips = _context.Trips.Where(e => e.UnitId == id).ToList();
            _context.RemoveRange(trips);
            _context.Remove(unitToDelete);
            return Save();
        }

        public DataTwo GetUnitAndLinja(int id)
        {
            DataTwo DataTwo = new DataTwo();
            DataTwo.Routes = _context.Routes.ToList();
            Unit? requestedUnit = _context.Units.Include(e => e.AllAssociations).FirstOrDefault(e => e.UnitId == id);
            DataTwo.Unit = requestedUnit;
            return DataTwo;
        }


        public Unit GetUnitById (int id)
        {
            return _context.Units.Include(e => e.route).Include(e => e.Creator).Include(e => e.trips).ThenInclude(e => e.User).FirstOrDefault(e => e.UnitId == id);
        }



        public List<Linja> GetRoutes()
        {
            return _context.Routes.ToList();
        }


        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
