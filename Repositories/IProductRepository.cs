using Backend.Models;

namespace Backend.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task<Product?> GetByPublicIdAsync(Guid publicId);
        IQueryable<Product> GetAll();
        Task UpdateProductAsync(Product product); // Adicionado
        Task DeleteProductAsync(Product product); // Adicionado
    }
}