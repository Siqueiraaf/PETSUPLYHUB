using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories.Implementations;
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
        var changes = await _context.SaveChangesAsync();
        if (changes == 0)
        {
            // Log para verificar o que foi adicionado
            Console.WriteLine($"Erro ao adicionar produto: {product.Name} | {product.Price}");
            throw new Exception("Nenhuma alteração foi salva no banco de dados.");
        }
    }
    public async Task<Product?> GetByPublicIdAsync(Guid publicId)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.PublicId == publicId);
    }
    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteProductAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
