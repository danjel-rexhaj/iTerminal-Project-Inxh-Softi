using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TerminalLibrary.Interfaces;
using TerminalLibrary.Models;

namespace iTerminal.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _login;

        public LoginController(ILoginService login)
        {
            _login = login;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("company/login")]
        public IActionResult CompanyLogin()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                UserReg? CurrentUser = _login.GetUser(user.LEmail);
                if (CurrentUser == null)
                {
                    ModelState.AddModelError("LEmail", "Invalid Username/Password");
                    return View("Index");
                }
                PasswordHasher<UserLogin> hasher = new PasswordHasher<UserLogin>();
                var result = hasher.VerifyHashedPassword(user, CurrentUser.Password, user.LPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LPassword", "Password invalid");
                    return View("Index");
                }
                if (CurrentUser?.AllMessages?.Any(e => e.Seen == false) == true)
                {
                    HttpContext.Session.SetInt32("Messages", 1);
                }
        
                HttpContext.Session.SetInt32("UserId", CurrentUser.id);
                HttpContext.Session.SetString("UserName", CurrentUser.FirstName);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("company/login")]
        public IActionResult CompanyLogin(CompanyLogin company)
        {
            if (ModelState.IsValid)
            {
                Company? CurrentCompany = _login.GetCompany(company.LEmail);
                if (CurrentCompany == null)
                {
                    ModelState.AddModelError("LEmail", "Invalid Email/Password");
                    return View("CompanyLogin");
                }
                PasswordHasher<CompanyLogin> hasher = new PasswordHasher<CompanyLogin>();
                var result = hasher.VerifyHashedPassword(company, CurrentCompany.Password, company.LPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LPassword", "Password invalid");
                    return View("CompanyLogin");
                }
                HttpContext.Session.SetInt32("UserId", CurrentCompany.CompanyId);
                HttpContext.Session.SetInt32("AdminId", CurrentCompany.CompanyId);
                HttpContext.Session.SetString("UserName", CurrentCompany.Name);

                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View("CompanyLogin");
            }
        }


        [HttpGet("reg")]
        public IActionResult ClientRegister()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(UserReg user)
        {
            if (ModelState.IsValid)
            {
                if (_login.AddUser(user))
                {
                    return View("ClientRegister");
                }
                HttpContext.Session.SetInt32("UserId", user.id);
                HttpContext.Session.SetString("UserName", user.FirstName);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("ClientRegister");
            }
        }

        [HttpGet("company/reg")]
        public IActionResult CompReg()
        {
            return View();
        }


        [HttpPost("company/register")]
        public IActionResult CompanyRegister(Company company)
        {
            if (ModelState.IsValid)
            {
                if (_login.AddCompany(company))
                {
                    return View("CompReg");
                }
                HttpContext.Session.SetInt32("AdminId", company.CompanyId);
                HttpContext.Session.SetInt32("UserId", company.CompanyId);
                HttpContext.Session.SetString("Name", company.Name);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View("CompReg");
            }
        }

        [SessionCheck]
        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }



    }
}
