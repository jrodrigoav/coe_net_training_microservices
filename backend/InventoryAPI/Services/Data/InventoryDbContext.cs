using InventoryAPI.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Services.Data
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public InventoryDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("inventory");
            modelBuilder.Entity<Resource>().HasIndex(r => new { r.Name, r.Type } ).IsUnique();
        }
    }
}
