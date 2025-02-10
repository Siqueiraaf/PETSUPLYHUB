using Backend.Data;
using Backend.Models;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
    {
        private readonly PetDbContext _context;

        public ProductRepository(PetDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetByPublicIdAsync(Guid publicId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.PublicId == publicId);
        }

}