using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalLibrary.Models
{
    public class Association
    {
        [Key]
        public int AssociationId { get; set; }

        public int? UnitId { get; set; }

        public int? RouteId { get; set; }

        public Unit? unit { get; set; }
        public Linja? route { get; set; }
    }
}
