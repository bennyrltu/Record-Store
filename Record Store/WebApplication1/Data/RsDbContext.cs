using Microsoft.EntityFrameworkCore;
using Record_Store.Entity;

namespace Record_Store.Data
{
    public class RsDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Recording> Recordings { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Trusted_Connection=True;");
        }
    }
}
