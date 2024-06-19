using Microsoft.EntityFrameworkCore;
using RentingAPI.Models.Data;

namespace RentingAPI.Services.Data
{
    public class RentingDbContext : DbContext
    {
        public DbSet<Rent> Rents { get; set; }
        public RentingDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("renting");
        }
    }
}
