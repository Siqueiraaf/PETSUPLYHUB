using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Backend.Data
{
    public class PetSuplyHubDbContext : DbContext
    {
        private readonly DbSettings _dbsettings;

        public PetSuplyHubDbContext(DbContextOptions<PetSuplyHubDbContext> options, IOptions<DbSettings> dbSettings)
            : base(options)
        {
            _dbsettings = dbSettings.Value;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable("Products") // Define the table name
                .HasKey(x => x.Id); // Set the primary key
        }

        public class DbSettings
        {   
            public string ConnectionString { get; set; }
        }
    }
}