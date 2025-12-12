using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalLibrary.Interfaces;
using TerminalLibrary.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TerminalLibrary.Services
{
    public class LoginService : ILoginService
    {
        private readonly MyContext _context;

        public LoginService(MyContext context)
        {
            _context = context;
        }

        public UserReg GetUser(string email)
        {
            return _context.Users.Include(e => e.AllMessages).FirstOrDefault(e => e.Email == email);
        }

        public Company GetCompany(string email)
        {
            return _context.Companies.FirstOrDefault(e => e.Email == email);
        }

        public bool AddUser(UserReg user)
        {
            PasswordHasher<UserReg> Hasher = new PasswordHasher<UserReg>();
            user.Password = Hasher.HashPassword(user, user.Password);
            _context.Add(user);
            _context.SaveChanges();
            return Save();
        }

        public bool AddCompany(Company company)
        {
            PasswordHasher<Company> Hasher = new PasswordHasher<Company>();
            company.Password = Hasher.HashPassword(company, company.Password);
            _context.Add(company);
            _context.SaveChanges();
            return Save();
        }


        private bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
