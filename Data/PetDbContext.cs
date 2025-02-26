using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    // Configura o banco de dados e suas tabelas
    public class PetDbContext(DbContextOptions<PetDbContext> options) : DbContext(options) 
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); // Define PublicId como chave primária

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.PublicId)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}