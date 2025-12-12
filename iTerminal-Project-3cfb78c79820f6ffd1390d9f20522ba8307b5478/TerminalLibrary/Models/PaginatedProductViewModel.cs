using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalLibrary.Models
{
    public class PaginatedProductViewModel
    {
        public List<Unit>? Units { get; set; }
        public List<UserReg>? Users { get; set; }
        public int? PageNumber { get; set; }
        public int? TotalPages { get; set; }

        public UserReg? User { get; set; }
        public Unit? Unit { get; set; }
        public Trip? Trip { get; set; }
    }
}
