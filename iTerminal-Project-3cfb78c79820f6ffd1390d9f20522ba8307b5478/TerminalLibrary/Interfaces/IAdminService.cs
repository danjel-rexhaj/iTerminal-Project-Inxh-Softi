using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalLibrary.Models;

namespace TerminalLibrary.Interfaces
{
    // Updated formatting (minor change)
    public interface IAdminService
    {
        DataTwo GetRegisteredRoutes();
        
        bool CheckRoute(Linja route);
        
        bool RegisterRoute(Linja route);
        
        PaginatedProductViewModel GetMyUnits(int userId, int page, int nisja);
        
        bool DeleteUnit(int UnitId, int UserId);
        
        DataTwo GetUnitAndLinja(int unitId);
        
        Unit GetUnitById(int UnitId);
        
        List<Linja> GetRoutes();
    }
}

