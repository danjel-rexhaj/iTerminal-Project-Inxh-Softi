using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalLibrary.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [Required]
        public string Content { get; set; }
        public bool Seen { get; set; } = false;
        public int? CompanyId { get; set; }

        public int? UserId { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public Company? Company { get; set; }
        public UserReg? User { get; set; }
    }
}
