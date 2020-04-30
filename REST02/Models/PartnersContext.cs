using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST02.Models
{
    public class PartnersContext : DbContext
    {
        public DbSet<Partner> Partners { get; set; }
        public PartnersContext(DbContextOptions<PartnersContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
