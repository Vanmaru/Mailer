using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mailer.Models
{
    public class SendingContext : DbContext
    {
        public DbSet<SendStatus> Sendings { get; set; }

        public SendingContext(DbContextOptions<SendingContext> options = default) : base(options) => Database.EnsureCreated();

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Server=(localdb)\\mssqllocaldb;Database=Sendings;Trusted_Connection=True;");
        //}
    }
}
