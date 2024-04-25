using Microsoft.EntityFrameworkCore;
using ResourcesAPI.Models.Data;

namespace ResourcesAPI.Services.Data
{
    public class ResourcesDbContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public ResourcesDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("resources");
            modelBuilder.Entity<Resource>().HasIndex(i => new { i.Name, i.Description }).IsUnique();
        }
    }
}
