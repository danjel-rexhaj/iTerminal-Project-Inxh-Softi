using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalLibrary.Models
{
    public class Trip
    {
        [Key]
        public int TripId { get; set; }
        [Required(ErrorMessage = "Numri i vendeve nuk mund te jete bosh")]
        [Range(1, int.MaxValue, ErrorMessage = "Vlera duhet te jete me e madhe se 1")]
        public int Seats { get; set; }

        public int? Total { get; set; }
        public int? UnitId { get; set; }

        public int? UserId { get; set; }

        public Unit? Unit { get; set; }
        public UserReg? User { get; set; }
    }


    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }
            return false;
        }
    }
}
