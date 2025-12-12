using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalLibrary.Models
{
    public class CompanyLogin
    {

        [Required(ErrorMessage = "Email is required")]
        [MinLength(3, ErrorMessage = "Email must be at least 3 characters")]
        [ExistingCompanyEmail]
        public string LEmail { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string LPassword { get; set; }


    }


    public class ExistingCompanyEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult("Email is required!");
            }
            MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
            if (!_context.Companies.Any(e => e.Email == value.ToString()))
            {

                return new ValidationResult("Company not registered");
            }
            else
            {

                return ValidationResult.Success;
            }
        }
    }





}
