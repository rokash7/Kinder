using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kinder.Database
{
    class UsersContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Items> Items { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=Kinder; Trusted_Connection=True;");
        }
    }
}
