using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalLibrary.Models;

namespace TerminalLibrary.Interfaces
{
    public interface IClientService
    {
        Trip GetTrip(int tripId);
        List<Unit> GetSearchedUnit(Linja searched);
        Unit GetUnit(int UnitId);
        UserReg GetLoggedUser(int userId);
        bool ConfirmTrip(Trip trip);
        List<Trip> GetFutureTrips(int userId);
        List<Trip> GetPastTrips(int userId);
        bool CancelTrip(int id);
        List<Message> GetAllMessages(int userId);
        Message GetMessage(int id);

         
    }
}
