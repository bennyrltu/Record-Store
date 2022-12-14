using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Record_Store.Auth;
using Record_Store.Entity;

namespace Record_Store.Data
{
    public class RsDbContext : IdentityDbContext<StoreRestUser>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Recording> Recordings { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSqlLocalDb;database=RecordStore;Trusted_Connection=True;");
        }
    }
}
