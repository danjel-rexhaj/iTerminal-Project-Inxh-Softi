using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalLibrary.Models
{
    public class Linja
    {
        [Key]
        public int RouteId { get; set; }
        [Required(ErrorMessage = "Vendi i nisjes nuk mund te jete bosh.")]
        public string PointA { get; set; }

        [Required(ErrorMessage = "Destinacioni nuk mund te jete bosh")]
        public string PointB { get; set; }

        public List<Association>? AllAssociations { get; set; }
    }
}
