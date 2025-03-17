using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class PetDbContext(DbContextOptions<PetDbContext> options) 
        : IdentityDbContext<Users, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Garante que as tabelas do Identity sejam criadas

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); 

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.PublicId)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}
