using ClientsAPI.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.Services.Data
{
    public class ClientsDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public ClientsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("clients");
            modelBuilder.Entity<Client>().HasIndex(c => c.Email).IsUnique();
        }

        public Client? FindByEmail(string email)
        {
            return Clients.SingleOrDefault(c => c.Email == email);
        }
    }
}
