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
                .HasIndex(p => p.PublicId)  // Criando um índice no UUID para buscas rápidas
                .IsUnique();  // Garante que o UUID não se repita

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}