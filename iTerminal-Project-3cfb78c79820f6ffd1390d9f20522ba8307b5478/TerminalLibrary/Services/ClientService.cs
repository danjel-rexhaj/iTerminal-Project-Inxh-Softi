using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalLibrary.Interfaces;
using TerminalLibrary.Models;


namespace TerminalLibrary.Services
{
    public class ClientService : IClientService
    {
        public MyContext _context { get; }
        public ClientService(MyContext context)
        {
            _context = context;
        }


        public List<Unit> GetSearchedUnit(Linja searched)
        {
            return _context.Units.Include(e => e.route).Include(e => e.Creator)
                .Where(e => e.route.PointA.ToLower() == searched.PointA.ToLower() && 
            e.route.PointB.ToLower() ==searched.PointB.ToLower() && e.Nisja > DateTime.Now && e.Seats > 0).ToList();
        }

        public Unit GetUnit(int id)
        {
            return _context.Units.Include(e => e.Creator).FirstOrDefault(e => e.UnitId == id);
        }

        public UserReg GetLoggedUser(int userId)
        {
            return _context.Users.Include(e => e.AllMessages).FirstOrDefault(e => e.id == userId);
        }

        public bool ConfirmTrip(Trip trip)
        {
            Unit? unitToUpdate = _context.Units.FirstOrDefault(e => e.UnitId == trip.UnitId);
            unitToUpdate.Seats -= trip.Seats;
            trip.Total = trip.Seats * unitToUpdate.Price;
            _context.Add(trip);
            return Save();
        }

        public List<Trip> GetFutureTrips(int userId)
        {
         return _context.Trips.Include(e => e.Unit).ThenInclude(e => e.route).Include(e => e.Unit.Creator).Include(e => e.User).Where(e => e.UserId == userId && e.Unit.Nisja > DateTime.Now).OrderByDescending(e => e.Unit.Nisja).ToList();
        }

        public List<Trip> GetPastTrips(int userId)
        {
         return _context.Trips.Include(e => e.Unit).ThenInclude(e => e.route).Include(e => e.Unit.Creator).Include(e => e.User).Where(e => e.UserId == userId && e.Unit.Nisja < DateTime.Now).OrderByDescending(e => e.Unit.Nisja).ToList();
        }

        public bool CancelTrip(int id)
        {
            Trip? tripToRemove = _context.Trips.FirstOrDefault(e => e.TripId == id);
            Unit? unitToUpdate = _context.Units.FirstOrDefault(e => e.UnitId == tripToRemove.UnitId);
            unitToUpdate.Seats += tripToRemove.Seats;
            _context.Remove(tripToRemove);
            return Save();
        }

        public Trip GetTrip(int id)
        {
            return _context.Trips.Include(e => e.Unit).ThenInclude(e => e.route).Include(e => e.Unit.Creator).Include(e => e.User).FirstOrDefault(e => e.TripId == id);
        }

        public List<Message>GetAllMessages(int userId)
        {
         return _context.Messages.Include(e => e.Company).Where(e => e.UserId == userId).OrderByDescending(e => e.MessageId).ToList();
        }



        public Message GetMessage(int MessageId)
        {
            return _context.Messages.Include(e => e.Company).FirstOrDefault(e => e.MessageId == MessageId);
        }



        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
