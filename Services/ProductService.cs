using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ProductService : IProductService
    {
        // Injeção de dependência do DbContext
        private readonly PetSuplyHubDbContext _context;

        public ProductService(PetSuplyHubDbContext context)
        {
            _context = context;
        }

        // Método para obter todos os produtos da base
        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Método para criar um novo produto
        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // Método para excluir um produto pelo UUID
        public async Task<bool> DeleteProductAsync(Guid guid)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Uuid == guid);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        // Método para obter um produto pelo UUID
        public async Task<Product> GetProductByIdAsync(Guid uuid)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Uuid == uuid);
        }

        // Método para atualizar um produto pelo UUID 
        public async Task<Product> UpdateProductAsync(Guid guid, Product updateProduct)
        {
            // Verifique se o produto com o UUID fornecido existe
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Uuid == guid);
            if (product == null)
            {
                return null;
            }

            product.Name = updateProduct.Name;
            product.Category = updateProduct.Category;
            product.AnimalSpecie = updateProduct.AnimalSpecie;
            product.Description = updateProduct.Description;
            product.Price = updateProduct.Price;
            product.StockQuantity = updateProduct.StockQuantity;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}