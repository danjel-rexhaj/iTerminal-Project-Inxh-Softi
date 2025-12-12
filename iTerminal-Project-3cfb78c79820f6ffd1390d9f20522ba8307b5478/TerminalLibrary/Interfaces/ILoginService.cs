using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalLibrary.Models;

namespace TerminalLibrary.Interfaces
{
    public interface ILoginService
    {
        UserReg GetUser(string email);
        Company GetCompany(string email);
        bool AddUser(UserReg user);
        bool AddCompany(Company company);
    }
}
