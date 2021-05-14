using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace net_core_db
{
    public class ApplicationContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }
        public ApplicationContext()
        { 
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=aa");
        }
    }
}
