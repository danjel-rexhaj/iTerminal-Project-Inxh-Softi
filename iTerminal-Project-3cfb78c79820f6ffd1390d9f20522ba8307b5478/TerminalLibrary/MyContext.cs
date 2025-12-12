using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalLibrary.Models
{
    public class MyContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public MyContext(DbContextOptions<MyContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<UserReg> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Association> Associations { get; set; }
        public DbSet<Linja> Routes { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
